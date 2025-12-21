'use client';

import React, { useMemo } from 'react';
import { GoogleMap, Marker, Polyline, useJsApiLoader } from '@react-google-maps/api';
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
        const opacity = selectedRouteId && !isSelected ? 0.3 : 1;

        const path = route.personnel.map((p) => p.location);

        // Add line to destination if exists (Visual cue)
        if (destination) {
            // Optional: Draw dashed line from last stop to destination
        }

        elements.push(
          <React.Fragment key={route.id}>
            {/* Route Path */}
            <Polyline
              path={path}
              options={{
                strokeColor: color,
                strokeOpacity: opacity,
                strokeWeight: 4,
                geodesic: true,
                icons: [{
                    icon: { path: 2, scale: 3, strokeOpacity: 1, strokeColor: color }, // Arrow
                    offset: '100%',
                }],
              }}
            />
            {/* Personnel Markers */}
            {route.personnel.map((p) => (
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
            ))}
          </React.Fragment>
        );
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
  }, [personnel, routes, destination, selectedRouteId, isLoaded]);

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
