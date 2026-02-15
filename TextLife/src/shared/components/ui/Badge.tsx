import React from 'react';
import { View, Text } from 'react-native';

type BadgeVariant = 'success' | 'warning' | 'danger' | 'info';

interface BadgeProps {
  label: string;
  variant?: BadgeVariant;
  className?: string;
}

const VARIANT_BG: Record<BadgeVariant, string> = {
  success: 'bg-success-subtle',
  warning: 'bg-warning-subtle',
  danger: 'bg-danger-subtle',
  info: 'bg-brand-subtle',
};

const VARIANT_TEXT: Record<BadgeVariant, string> = {
  success: 'text-success-on',
  warning: 'text-warning-on',
  danger: 'text-danger-on',
  info: 'text-brand-primary',
};

export const Badge = ({ label, variant = 'info', className = '' }: BadgeProps) => (
  <View className={`px-sm py-1 rounded-full ${VARIANT_BG[variant]} ${className}`}>
    <Text className={`text-xs font-outfit-medium ${VARIANT_TEXT[variant]}`}>
      {label}
    </Text>
  </View>
);
