/**
 * GEOGRAPHIC UTILITIES
 * Context Level 7: Deterministic and immutable calculations.
 */

export interface Coordinates {
  lat: number;
  lng: number;
}

/**
 * Calculates the distance between two coordinates using the Haversine formula.
 * Returns distance in kilometers.
 * 
 * @param from - Start coordinates
 * @param to - End coordinates
 * @returns Distance in km
 */
export const calculateDistance = (from: Coordinates, to: Coordinates): number => {
  const R = 6371; // Earth's radius in km
  const dLat = toRad(to.lat - from.lat);
  const dLon = toRad(to.lng - from.lng);
  
  const lat1 = toRad(from.lat);
  const lat2 = toRad(to.lat);

  const a =
    Math.sin(dLat / 2) * Math.sin(dLat / 2) +
    Math.sin(dLon / 2) * Math.sin(dLon / 2) * Math.cos(lat1) * Math.cos(lat2);
  const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
  
  return R * c;
};

/**
 * Helper to convert degrees to radians.
 */
const toRad = (value: number): number => {
  return (value * Math.PI) / 180;
};
