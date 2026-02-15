import React, { memo, useEffect } from 'react';
import { View } from 'react-native';
import Animated, {
  useSharedValue,
  useAnimatedStyle,
  withTiming,
  Easing,
} from 'react-native-reanimated';

interface ProgressBarProps {
  value: number;
  color: string;
  className?: string;
}

export const ProgressBar = memo(
  ({ value, color, className = '' }: ProgressBarProps) => {
    const width = useSharedValue(0);

    useEffect(() => {
      width.value = withTiming(value, {
        duration: 200,
        easing: Easing.bezierFn(0.16, 1, 0.3, 1),
      });
    }, [value, width]);

    const animatedStyle = useAnimatedStyle(() => ({
      width: `${width.value}%`,
      backgroundColor: color,
    }));

    return (
      <View
        className={`h-3 bg-bg-sunken rounded-full overflow-hidden ${className}`}
      >
        <Animated.View className="h-full rounded-full" style={animatedStyle} />
      </View>
    );
  },
);
