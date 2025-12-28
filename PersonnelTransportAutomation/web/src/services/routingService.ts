import { calculateDistance, Coordinates } from '@/utils/geoUtils';
import { Destination } from '@/data/mockDestinations';

/**
 * ROUTING SERVICE
 * Context Level 7: Deterministic North-to-South Sweep & Furthest-First Traversal.
 */

export interface Personnel {
  id: string;
  fullName: string;
  address: string;
  location: Coordinates;
}

export interface RoutingInput {
  personnel: Personnel[];
  destination: Destination; // REQUIRED
  vehicleCapacity?: number;
}

export interface RoutedPersonnel extends Personnel {
  pickupOrder: number;
}

export interface Route {
  id: string;
  name: string;
  destination: Destination;
  personnel: RoutedPersonnel[];
}

export interface RoutingResult {
  totalRoutes: number;
  routes: Route[];
}

/**
 * Generates optimized routes for the given personnel list towards a destination.
 * Strategy: Global Latitude Sort (clustering) + Local Furthest-First Traversal.
 */
export const generateRoutes = (input: RoutingInput): RoutingResult => {
  const { personnel, destination, vehicleCapacity = 10 } = input;

  if (!personnel || personnel.length === 0 || !destination) {
    return { totalRoutes: 0, routes: [] };
  }

  // 1. Validation & Filter
  const validPersonnel = personnel.filter(
    (p) =>
      p.location &&
      typeof p.location.lat === 'number' &&
      typeof p.location.lng === 'number'
  );

  if (validPersonnel.length === 0) return { totalRoutes: 0, routes: [] };

  // 2. Metric Calculation (Angle & Distance)
  // Also find Max Distance for Zone Classification
  let maxDistance = 0;

  const personnelWithMetrics = validPersonnel.map(p => {
    // Calculate polar angle (0-360 degrees) relative to destination
    const dy = p.location.lat - destination.location.lat;
    const dx = p.location.lng - destination.location.lng;
    let angle = Math.atan2(dy, dx) * (180 / Math.PI);
    if (angle < 0) angle += 360;

    const distance = calculateDistance(p.location, destination.location);
    if (distance > maxDistance) maxDistance = distance;

    return { ...p, angle, distanceToDest: distance };
  });

  // 3. Zone Classification
  // Zone 1 (Far): > 66% Max Dist
  // Zone 2 (Mid): 33% - 66% Max Dist
  // Zone 3 (Near): < 33% Max Dist
  
  const farZone: typeof personnelWithMetrics = [];
  const midZone: typeof personnelWithMetrics = [];
  const nearZone: typeof personnelWithMetrics = [];

  const thresholdFar = maxDistance * 0.66;
  const thresholdMid = maxDistance * 0.33;

  personnelWithMetrics.forEach(p => {
    if (p.distanceToDest > thresholdFar) {
        farZone.push(p);
    } else if (p.distanceToDest > thresholdMid) {
        midZone.push(p);
    } else {
        nearZone.push(p);
    }
  });

  const routes: Route[] = [];
  let routeCounter = 1;

  // 4. Per-Zone Routing Function
  const processZone = (zonePersonnel: typeof personnelWithMetrics, zoneName: string) => {
    // A. Sweep Sort (Angle)
    const sortedByAngle = [...zonePersonnel].sort((a, b) => a.angle - b.angle);

    for (let i = 0; i < sortedByAngle.length; i += vehicleCapacity) {
        // B. Chunk
        const chunk = sortedByAngle.slice(i, i + vehicleCapacity);
        
        // C. Sort by Distance (Descending - Furthest First)
        // Strictly start from the edge of the zone/sector and move inwards
        const chunkSortedByDist = chunk.sort((a, b) => b.distanceToDest - a.distanceToDest);

        // D. Optimize
        const optimizedChunk = optimizeRouteChunk(chunkSortedByDist, destination);

        routes.push({
          id: `route-${routeCounter}`,
          name: `Route ${routeCounter} (${zoneName})`, // Labeling for clarity
          destination: destination,
          personnel: optimizedChunk,
        });
        routeCounter++;
    }
  };

  // Process Zones in Order: FAR -> MID -> NEAR
  // This ensures logical layering, though they are independent sets now.
  if (farZone.length > 0) processZone(farZone, 'Far');
  if (midZone.length > 0) processZone(midZone, 'Mid');
  if (nearZone.length > 0) processZone(nearZone, 'Near');

  return {
    totalRoutes: routes.length,
    routes,
  };
};

/**
 * locally optimizes a route chunk using Furthest-First & Nearest Neighbor algorithm.
 * 
 * Logic:
 * 1. Find the person FURTHEST from the destination. This is likely the "start" of the line.
 * 2. From there, find the NEAREST neighbor to continue the chain.
 * This effectively creates a path moving from the outskirts TOWARDS the destination.
 */
const optimizeRouteChunk = (chunk: Personnel[], destination: Destination): RoutedPersonnel[] => {
  if (chunk.length === 0) return [];

  const unvisited = [...chunk];
  const ordered: RoutedPersonnel[] = [];
  
  // 1. Find Start Point: Furthest from Destination
  let furthestIdx = -1;
  let maxDist = -1;

  for (let i = 0; i < unvisited.length; i++) {
    const distToDest = calculateDistance(unvisited[i].location, destination.location);
    if (distToDest > maxDist) {
      maxDist = distToDest;
      furthestIdx = i;
    }
  }

  // Start with furthest
  let current = unvisited[furthestIdx];
  ordered.push({ ...current, pickupOrder: 1 });
  unvisited.splice(furthestIdx, 1);

  let orderCounter = 2;

  // 2. Greedy Traversal (Nearest Neighbor)
  while (unvisited.length > 0) {
    let nearestIdx = -1;
    let minDist = Number.MAX_VALUE;

    for (let i = 0; i < unvisited.length; i++) {
      const dist = calculateDistance(current.location, unvisited[i].location);
      if (dist < minDist) {
        minDist = dist;
        nearestIdx = i;
      }
    }

    // Move nearest to ordered
    const nextPerson = unvisited[nearestIdx];
    ordered.push({ ...nextPerson, pickupOrder: orderCounter });
    
    // Update state
    current = nextPerson;
    unvisited.splice(nearestIdx, 1);
    orderCounter++;
  }

  return ordered;
};
