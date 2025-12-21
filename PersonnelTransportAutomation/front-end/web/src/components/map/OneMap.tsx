'use client';

import React, { useMemo } from 'react';
import { GoogleMap, Marker, Polyline, useJsApiLoader, DirectionsRenderer } from '@react-google-maps/api';
import { Personnel, Route } from '@/services/routingService';
import { Destination } from '@/data/mockDestinations';

/**
 * MAP COMPONENT
 * Context Level 7: Passive visualization.
 * No routing logic allowed here.
 */

const containerStyle = {
  width: '100%',
  height: '100%',
  borderRadius: '8px',
};

const IstanbulCenter = {
  lat: 41.0082,
  lng: 28.9784,
};

interface OneMapProps {
  personnel?: Personnel[];
  routes?: Route[];
  destination?: Destination | null; // NEW PROP
  selectedRouteId?: string | null;
}

const COLORS = ['#1890ff', '#f5222d', '#52c41a', '#fa8c16', '#722ed1', '#eb2f96'];

export default function OneMap({ personnel = [], routes = [], destination, selectedRouteId }: OneMapProps) {
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: process.env.NEXT_PUBLIC_GOOGLE_MAPS_API_KEY || '', // Graceful fallback
  });

  // State for Directions
  const [directionsCache, setDirectionsCache] = React.useState<Record<string, google.maps.DirectionsResult>>({});

  // Fetch Directions when routes change
  // Fetch Directions when routes change (Sequentially to avoid OVER_QUERY_LIMIT)
  // Fetch Directions when routes change (Sequentially to avoid OVER_QUERY_LIMIT)
  React.useEffect(() => {
    if (!isLoaded || !routes || routes.length === 0 || typeof google === 'undefined') return;

    let ignore = false;

    const fetchDirections = async () => {
        const service = new google.maps.DirectionsService();

        for (const route of routes) {
            if (ignore) break;

            // Skip if already cached (Check state via functional update not needed here if we trust stability, but strictly we can't read 'directionsCache' without dep.
            // BETTER: Just fetch. If it's redundant, the cost is API quota, but 'routes' change should imply new data.
            // OR: We can use a ref to track what we've fetched to avoid dep loops.
            
            // Simplest fix: Just run logic.
            
            // Origin: First person
            const origin = route.personnel[0].location;
            // Destination: Target Destination OR Last person
            const dest = destination ? destination.location : route.personnel[route.personnel.length - 1].location;
            
            // Waypoints: All between start and end (excluding last person if dest is set)
            const waypoints = destination 
                ? route.personnel.slice(1).map(p => ({ location: p.location, stopover: true }))
                : route.personnel.slice(1, -1).map(p => ({ location: p.location, stopover: true }));

            try {
                // Wrap in Promise for sequential execution
                const result = await new Promise<google.maps.DirectionsResult>((resolve, reject) => {
                    service.route({
                        origin: origin,
                        destination: dest,
                        waypoints: waypoints,
                        travelMode: google.maps.TravelMode.DRIVING,
                    }, (response, status) => {
                        if (status === google.maps.DirectionsStatus.OK && response) {
                            resolve(response);
                        } else {
                            reject(status);
                        }
                    });
                });
                
                if (!ignore) {
                     setDirectionsCache(prev => ({ ...prev, [route.id]: result }));
                }
                
                // Throttle: Wait 300ms before next request
                await new Promise(r => setTimeout(r, 300));

            } catch (error) {
                console.error(`Directions request failed for ${route.id}:`, error);
                if (error === google.maps.DirectionsStatus.OVER_QUERY_LIMIT) {
                     break; 
                }
            }
        }
    };

    fetchDirections();

    return () => {
        ignore = true;
    };
  }, [routes, isLoaded, destination]); // REMOVED directionsCache


  // Calculate Map Markers
  const mapElements = useMemo(() => {
    if (!isLoaded || typeof google === 'undefined') return [];

    const elements: React.ReactNode[] = [];

    // 1. Destination Marker (Highest priority)
    if (destination) {
        elements.push(
            <Marker
                key={`dest-${destination.id}`}
                position={destination.location}
                label={{
                    text: '★',
                    color: 'white',
                    fontWeight: 'bold',
                    fontSize: '18px'
                }}
                icon={{
                    path: google.maps.SymbolPath.CIRCLE,
                    scale: 12,
                    fillColor: '#000000', // Black
                    fillOpacity: 1,
                    strokeColor: '#ffffff',
                    strokeWeight: 2,
                }}
                title={`Destination: ${destination.name}`}
                zIndex={999} // Always on top
            />
        );
    }

    // 2. Routes
    if (routes.length > 0) {
      routes.forEach((route, index) => {
        const color = COLORS[index % COLORS.length];
        const isSelected = selectedRouteId === route.id;
        const opacity = selectedRouteId && !isSelected ? 0.2 : 1; // Dimmer unselected

        // A. Render Directions Path (Roads)
        const directions = directionsCache[route.id];
        if (directions) {
             elements.push(
                <DirectionsRenderer
                    key={`dir-${route.id}`}
                    directions={directions}
                    options={{
                        suppressMarkers: true, // We use custom markers
                        polylineOptions: {
                            strokeColor: color,
                            strokeOpacity: opacity,
                            strokeWeight: 5,
                        },
                        preserveViewport: true, // Don't auto-zoom on every render
                    }}
                />
             );
        } else {
            // Fallback: Straight Lines (while loading or on error)
            const path = route.personnel.map((p) => p.location);
            if (destination) path.push(destination.location);

            elements.push(
                 <Polyline
                    key={`fallback-${route.id}`}
                    path={path}
                    options={{
                        strokeColor: color,
                        strokeOpacity: opacity * 0.5, // Faint
                        strokeWeight: 2,
                        geodesic: true,
                    }}
                />
            );
        }

        // B. Personnel Markers (Always Render)
        route.personnel.forEach((p) => (
            elements.push(
              <Marker
                key={p.id}
                position={p.location}
                label={{
                  text: p.pickupOrder.toString(),
                  color: 'white',
                  fontWeight: 'bold',
                }}
                icon={{
                    path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z',
                    fillColor: color,
                    fillOpacity: opacity,
                    strokeColor: '#000',
                    strokeWeight: 1,
                    scale: 1,
                    labelOrigin: new google.maps.Point(0, -30),
                }}
                title={`${p.fullName} (#${p.pickupOrder})`}
              />
            )
        ));
      });
    } 
    // 3. Default Personnel (if no routes)
    else {
        personnel.forEach((p) => {
            elements.push(
                <Marker
                    key={p.id}
                    position={p.location}
                    title={p.fullName}
                    icon={{
                       path: google.maps.SymbolPath.CIRCLE,
                       scale: 6,
                       fillColor: '#888888',
                       fillOpacity: 1,
                       strokeWeight: 1,
                       strokeColor: 'white'
                    }}
                />
            );
        });
    }

    return elements;
  }, [personnel, routes, destination, selectedRouteId, isLoaded, directionsCache]);

  if (!isLoaded) return <div className="h-full w-full bg-gray-100 flex items-center justify-center">Loading Map...</div>;

  return (
    <GoogleMap
      mapContainerStyle={containerStyle}
      center={destination?.location || personnel?.[0]?.location || IstanbulCenter}
      zoom={11} // Slightly zoomed out
      options={{
        disableDefaultUI: false,
        zoomControl: true,
        streetViewControl: false,
      }}
    >
      {mapElements}
    </GoogleMap>
  );
}
