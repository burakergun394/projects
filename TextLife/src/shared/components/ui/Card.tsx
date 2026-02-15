import React from 'react';
import { View } from 'react-native';

import type { ReactNode } from 'react';

interface CardProps {
  children: ReactNode;
  className?: string;
}

export const Card = ({ children, className = '' }: CardProps) => (
  <View
    className={`bg-bg-elevated rounded-md border border-border p-lg shadow-sm ${className}`}
  >
    {children}
  </View>
);
