import { cssInterop } from 'nativewind';
import * as RN from 'react-native';
import { Link as ExpoLink } from 'expo-router';

export const View = cssInterop(RN.View, { className: 'style' });
export const Text = cssInterop(RN.Text, { className: 'style' });
export const Pressable = cssInterop(RN.Pressable, { className: 'style' });
export const ScrollView = cssInterop(RN.ScrollView, {
  className: 'style',
  contentContainerClassName: 'contentContainerStyle',
});
export const TextInput = cssInterop(RN.TextInput, { className: 'style' });
export const TouchableHighlight = cssInterop(RN.TouchableHighlight, { className: 'style' });
export const Link = cssInterop(ExpoLink, { className: 'style' });
export const AnimatedScrollView = cssInterop(RN.Animated.ScrollView, {
  className: 'style',
  contentContainerClassName: 'contentContainerStyle',
});
