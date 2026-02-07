import { forwardRef } from 'react';
import { View, Pressable, Text, StyleSheet } from 'react-native';
import { colors } from '@/src/theme';

type ButtonProps = {
  title?: string;
  variant?: 'primary' | 'secondary' | 'danger';
  onPress?: () => void;
};

const variantBg = {
  primary: colors.navy,
  secondary: colors.orange,
  danger: '#dc2626',
};

export const Button = forwardRef<View, ButtonProps>(
  ({ title, variant = 'primary', onPress }, ref) => {
    return (
      <Pressable
        ref={ref}
        onPress={onPress}
        style={[styles.button, { backgroundColor: variantBg[variant] }]}>
        <Text style={styles.text}>{title}</Text>
      </Pressable>
    );
  }
);

Button.displayName = 'Button';

const styles = StyleSheet.create({
  button: {
    alignItems: 'center',
    borderRadius: 24,
    flexDirection: 'row',
    justifyContent: 'center',
    marginHorizontal: 16,
    padding: 16,
  },
  text: {
    color: '#fff',
    fontSize: 16,
    fontWeight: '600',
    textAlign: 'center',
  },
});
