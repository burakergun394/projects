import type { ReactNode } from 'react';
import { View, Pressable } from '@/src/tw';

const shadowClasses = {
  xs: 'shadow-xs',
  sm: 'shadow-sm',
  md: 'shadow-md',
  lg: 'shadow-lg',
  xl: 'shadow-xl',
} as const;

interface CardProps {
  children: ReactNode;
  shadow?: keyof typeof shadowClasses;
  onPress?: () => void;
  className?: string;
}

export const Card = ({ children, shadow = 'sm', onPress, className = '' }: CardProps) => {
  const base = `bg-white rounded-2xl border-continuous ${shadowClasses[shadow]} ${className}`;

  if (onPress) {
    return (
      <Pressable onPress={onPress} className={base}>
        {children}
      </Pressable>
    );
  }

  return <View className={base}>{children}</View>;
};
