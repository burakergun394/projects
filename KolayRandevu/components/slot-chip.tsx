import { View, Text, Pressable, StyleSheet } from 'react-native';

interface SlotChipProps {
  time: string;
  isOutsideRange?: boolean;
  onRemove: () => void;
}

export const SlotChip = ({ time, isOutsideRange, onRemove }: SlotChipProps) => {
  return (
    <View style={[styles.container, isOutsideRange ? styles.bgOutside : styles.bgNormal]}>
      <Text style={[styles.time, isOutsideRange ? styles.textOutside : styles.textNormal]}>
        {time}
      </Text>
      <Pressable onPress={onRemove} style={styles.removeBtn}>
        <Text
          style={[styles.removeText, isOutsideRange ? styles.removeOutside : styles.removeNormal]}>
          ✕
        </Text>
      </Pressable>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    borderRadius: 12,
    paddingHorizontal: 12,
    paddingVertical: 8,
    gap: 8,
    borderCurve: 'continuous',
  },
  bgNormal: { backgroundColor: '#f9fafb' },
  bgOutside: { backgroundColor: '#fef2f2' },
  time: {
    fontWeight: '600',
    fontSize: 14,
    fontVariant: ['tabular-nums'],
  },
  textNormal: { color: '#111827' },
  textOutside: { color: '#ef4444' },
  removeBtn: { marginLeft: 2 },
  removeText: { fontWeight: '700', fontSize: 12 },
  removeNormal: { color: '#9ca3af' },
  removeOutside: { color: '#f87171' },
});
