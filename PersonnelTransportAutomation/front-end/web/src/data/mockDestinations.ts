import { Coordinates } from "@/utils/geoUtils";

/**
 * MOCK DESTINATIONS
 * Context Level 7: Static target locations.
 */

export interface Destination {
  id: string;
  name: string;
  location: Coordinates;
}

export const MOCK_DESTINATIONS: Destination[] = [
  { 
      id: 'dest-1', 
      name: 'Main HQ (Maslak)', 
      location: { lat: 41.1100, lng: 29.0200 } // Near Maslak
  },
  { 
      id: 'dest-2', 
      name: 'City Center Office (Sisli)', 
      location: { lat: 41.0600, lng: 28.9870 } // Near Mecidiyekoy
  },
  { 
      id: 'dest-3', 
      name: 'Distribution Warehouse (Gebze)', 
      location: { lat: 40.8000, lng: 29.4300 } // Far East
  },
];
