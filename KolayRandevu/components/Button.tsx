import { forwardRef } from 'react';
import { View } from 'react-native';
import { Pressable, Text } from '@/src/tw';

type ButtonProps = {
  title?: string;
  variant?: 'primary' | 'secondary' | 'danger';
  className?: string;
  onPress?: () => void;
};

const variantClasses = {
  primary: 'bg-navy',
  secondary: 'bg-orange',
  danger: 'bg-red-600',
} as const;

export const Button = forwardRef<View, ButtonProps>(
  ({ title, variant = 'primary', className = '', onPress }, ref) => {
    return (
      <Pressable
        ref={ref}
        onPress={onPress}
        className={`items-center rounded-3xl flex-row justify-center mx-4 p-4 ${variantClasses[variant]} ${className}`}>
        <Text className="text-white text-base font-semibold text-center">{title}</Text>
      </Pressable>
    );
  }
);

Button.displayName = 'Button';
