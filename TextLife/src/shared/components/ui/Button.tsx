import React, { useCallback } from 'react';
import { Pressable, Text } from 'react-native';
import Animated, {
  useSharedValue,
  useAnimatedStyle,
  withTiming,
  Easing,
} from 'react-native-reanimated';

const AnimatedPressable = Animated.createAnimatedComponent(Pressable);

type ButtonVariant = 'primary' | 'secondary' | 'ghost' | 'danger' | 'disabled';

interface ButtonProps {
  label: string;
  onPress: () => void;
  variant?: ButtonVariant;
  className?: string;
}

const VARIANT_STYLES: Record<ButtonVariant, string> = {
  primary: 'bg-brand-primary',
  secondary: 'bg-brand-subtle',
  ghost: 'bg-transparent',
  danger: 'bg-danger',
  disabled: 'bg-bg-sunken',
};

const VARIANT_TEXT: Record<ButtonVariant, string> = {
  primary: 'text-text-inverse font-outfit-semibold',
  secondary: 'text-brand-primary font-outfit-semibold',
  ghost: 'text-text-secondary font-outfit-semibold',
  danger: 'text-text-inverse font-outfit-semibold',
  disabled: 'text-text-disabled font-outfit',
};

export const Button = ({
  label,
  onPress,
  variant = 'primary',
  className = '',
}: ButtonProps) => {
  const scale = useSharedValue(1);

  const animatedStyle = useAnimatedStyle(() => ({
    transform: [{ scale: scale.value }],
  }));

  const handlePressIn = useCallback(() => {
    if (variant === 'disabled') return;
    scale.value = withTiming(0.97, {
      duration: 120,
      easing: Easing.out(Easing.ease),
    });
  }, [variant, scale]);

  const handlePressOut = useCallback(() => {
    scale.value = withTiming(1, {
      duration: 120,
      easing: Easing.out(Easing.ease),
    });
  }, [scale]);

  return (
    <AnimatedPressable
      onPress={variant === 'disabled' ? undefined : onPress}
      onPressIn={handlePressIn}
      onPressOut={handlePressOut}
      style={animatedStyle}
      className={`rounded-md px-lg py-md items-center justify-center ${VARIANT_STYLES[variant]} ${className}`}
      disabled={variant === 'disabled'}
    >
      <Text className={`text-sm ${VARIANT_TEXT[variant]}`}>{label}</Text>
    </AnimatedPressable>
  );
};
