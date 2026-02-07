import { cssInterop } from 'nativewind';
import { View } from 'react-native';
import RNAnimated from 'react-native-reanimated';

const AnimatedView = RNAnimated.createAnimatedComponent(View);
const CSSAnimatedView = cssInterop(AnimatedView, { className: 'style' });

export const Animated = {
  ...RNAnimated,
  View: CSSAnimatedView,
};
