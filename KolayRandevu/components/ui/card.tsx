import type { ReactNode } from 'react';
import { View, Pressable, StyleSheet, type ViewStyle } from 'react-native';
import { shadows } from '@/src/styles';

const shadowStyles = {
  xs: shadows.xs,
  sm: shadows.sm,
  md: shadows.md,
  lg: shadows.lg,
  xl: shadows.xl,
} as const;

interface CardProps {
  children: ReactNode;
  shadow?: keyof typeof shadowStyles;
  onPress?: () => void;
  style?: ViewStyle;
}

export const Card = ({ children, shadow = 'sm', onPress, style }: CardProps) => {
  const combined = [styles.base, shadowStyles[shadow], style];

  if (onPress) {
    return (
      <Pressable onPress={onPress} style={combined}>
        {children}
      </Pressable>
    );
  }

  return <View style={combined}>{children}</View>;
};

const styles = StyleSheet.create({
  base: {
    backgroundColor: '#fff',
    borderRadius: 16,
    borderCurve: 'continuous',
  },
});
