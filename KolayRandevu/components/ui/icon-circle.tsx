import type { ElementType } from 'react';
import { View } from '@/src/tw';
import { colors } from '@/src/theme';

const sizeMap = {
  sm: { className: 'w-10 h-10', icon: 20 },
  md: { className: 'w-12 h-12', icon: 22 },
  lg: { className: 'w-14 h-14', icon: 26 },
  xl: { className: 'w-16 h-16', icon: 28 },
} as const;

const variantMap = {
  navy: { className: 'bg-navy/[0.08]', color: colors.navy },
  orange: { className: 'bg-orange/10', color: colors.orange },
  muted: { className: 'bg-navy/[0.06]', color: colors.muted },
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
    <View
      className={`rounded-xl items-center justify-center border-continuous ${s.className} ${v.className}`}>
      <Icon size={s.icon} color={v.color} strokeWidth={strokeWidth} />
    </View>
  );
};
