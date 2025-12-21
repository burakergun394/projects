'use client';

import React, { useMemo } from 'react';
import { GoogleMap, Marker, Polyline, useJsApiLoader } from '@react-google-maps/api';
import { Personnel, Route } from '@/services/routingService';

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
  selectedRouteId?: string | null;
}

const COLORS = ['#1890ff', '#f5222d', '#52c41a', '#fa8c16', '#722ed1', '#eb2f96'];

export default function OneMap({ personnel = [], routes = [], selectedRouteId }: OneMapProps) {
  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: process.env.NEXT_PUBLIC_GOOGLE_MAPS_API_KEY || '', // Graceful fallback
  });

  // Calculate Map Markers
  const mapElements = useMemo(() => {
    // If we have routes, show colored markers and paths
    if (routes.length > 0) {
      return routes.map((route, index) => {
        const color = COLORS[index % COLORS.length];
        const isSelected = selectedRouteId === route.id;
        const opacity = selectedRouteId && !isSelected ? 0.3 : 1;

        const path = route.personnel.map((p) => p.location);

        return (
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

    // Default: Show unrouted personnel (Gray)
    return personnel.map((p) => (
      <Marker
        key={p.id}
        position={p.location}
        title={p.fullName}
      />
    ));
  }, [personnel, routes, selectedRouteId]);

  if (!isLoaded) return <div className="h-full w-full bg-gray-100 flex items-center justify-center">Loading Map...</div>;

  return (
    <GoogleMap
      mapContainerStyle={containerStyle}
      center={personnel?.[0]?.location || IstanbulCenter}
      zoom={12}
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
