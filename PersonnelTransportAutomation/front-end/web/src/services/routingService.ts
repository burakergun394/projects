import { calculateDistance, Coordinates } from '@/utils/geoUtils';

/**
 * ROUTING SERVICE
 * Context Level 7: Deterministic North-to-South Sweep & Nearest Neighbor.
 */

export interface Personnel {
  id: string;
  fullName: string;
  address: string;
  location: Coordinates;
}

export interface RoutingInput {
  personnel: Personnel[];
  vehicleCapacity?: number;
}

export interface RoutedPersonnel extends Personnel {
  pickupOrder: number;
}

export interface Route {
  id: string;
  name: string;
  personnel: RoutedPersonnel[];
}

export interface RoutingResult {
  totalRoutes: number;
  routes: Route[];
}

/**
 * Generates optimized routes for the given personnel list.
 * Strategy: Global Latitude Sort (clustering) + Local Nearest Neighbor (ordering).
 */
export const generateRoutes = (input: RoutingInput): RoutingResult => {
  const { personnel, vehicleCapacity = 10 } = input;

  if (!personnel || personnel.length === 0) {
    return { totalRoutes: 0, routes: [] };
  }

  // 1. Validation & Filter
  // Ensure valid coordinates.
  const validPersonnel = personnel.filter(
    (p) =>
      p.location &&
      typeof p.location.lat === 'number' &&
      typeof p.location.lng === 'number'
  );

  // 2. Global Clustering: Sort by Latitude (North -> South)
  // Deterministic tie-breaking using ID.
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
    const optimizedChunk = optimizeRouteChunk(chunk);

    routes.push({
      id: `route-${routeCounter}`,
      name: `Route ${routeCounter}`,
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
 * locally optimizes a route chunk using Nearest Neighbor algorithm.
 */
const optimizeRouteChunk = (chunk: Personnel[]): RoutedPersonnel[] => {
  if (chunk.length === 0) return [];

  const unvisited = [...chunk];
  const ordered: RoutedPersonnel[] = [];
  
  // Start with the Northern-most person (already first due to global sort)
  // Shift strictly removes the first element.
  let current = unvisited.shift()!;
  ordered.push({ ...current, pickupOrder: 1 });

  let orderCounter = 2;

  while (unvisited.length > 0) {
    // Find nearest neighbor to 'current'
    let nearestIdx = -1;
    let minDist = Number.MAX_VALUE;

    for (let i = 0; i < unvisited.length; i++) {
      const dist = calculateDistance(current.location, unvisited[i].location);
      // Deterministic tie-breaking: prioritize lower index (original lat sort)
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
    unvisited.splice(nearestIdx, 1); // Remove from unvisited
    orderCounter++;
  }

  return ordered;
};
