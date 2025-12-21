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

  // 2. Global Clustering: Sort by Latitude (North -> South)
  // This ensures we group people who are geographically close in a 'band'.
  // We prioritize North-South sweep as a simple clustering heuristic.
  const sortedPersonnel = [...validPersonnel].sort((a, b) => {
    const latDiff = b.location.lat - a.location.lat; // Descending
    if (latDiff !== 0) return latDiff;
    return a.id.localeCompare(b.id); // Tie-breaker
  });

  // 3. Chunking & Local Optimization
  const routes: Route[] = [];
  let routeCounter = 1;

  for (let i = 0; i < sortedPersonnel.length; i += vehicleCapacity) {
    const chunk = sortedPersonnel.slice(i, i + vehicleCapacity);
    // Pass destination to optimization
    const optimizedChunk = optimizeRouteChunk(chunk, destination);

    routes.push({
      id: `route-${routeCounter}`,
      name: `Route ${routeCounter}`,
      destination: destination,
      personnel: optimizedChunk,
    });
    routeCounter++;
  }

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
