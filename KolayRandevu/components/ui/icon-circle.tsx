import type { ElementType } from 'react';
import { View, StyleSheet } from 'react-native';
import { colors } from '@/src/theme';

const sizeMap = {
  sm: { container: { width: 40, height: 40 }, icon: 20 },
  md: { container: { width: 48, height: 48 }, icon: 22 },
  lg: { container: { width: 56, height: 56 }, icon: 26 },
  xl: { container: { width: 64, height: 64 }, icon: 28 },
} as const;

const variantMap = {
  navy: { bg: 'rgba(26,35,126,0.08)', color: colors.navy },
  orange: { bg: 'rgba(255,152,0,0.1)', color: colors.orange },
  muted: { bg: 'rgba(26,35,126,0.06)', color: colors.muted },
} as const;

interface IconCircleProps {
  icon: ElementType;
  size?: keyof typeof sizeMap;
  variant?: keyof typeof variantMap;
  strokeWidth?: number;
}

export const IconCircle = ({
  icon: Icon,
  size = 'md',
  variant = 'navy',
  strokeWidth = 1.8,
}: IconCircleProps) => {
  const s = sizeMap[size];
  const v = variantMap[variant];

  return (
    <View style={[styles.base, s.container, { backgroundColor: v.bg }]}>
      <Icon size={s.icon} color={v.color} strokeWidth={strokeWidth} />
    </View>
  );
};

const styles = StyleSheet.create({
  base: {
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    borderCurve: 'continuous',
  },
});
