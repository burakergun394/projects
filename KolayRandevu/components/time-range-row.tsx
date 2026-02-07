import { View, Text, Pressable, StyleSheet } from 'react-native';
import { shadows } from '@/src/styles';
import type { TimeRange } from '@/store/types';

interface TimeRangeRowProps {
  range: TimeRange;
  onStartPress: () => void;
  onEndPress: () => void;
  onRemove: () => void;
}

export const TimeRangeRow = ({ range, onStartPress, onEndPress, onRemove }: TimeRangeRowProps) => {
  return (
    <View style={styles.container}>
      <Pressable onPress={onStartPress} style={[styles.timeBtn, shadows.xs]}>
        <Text style={styles.timeText}>{range.startTime}</Text>
      </Pressable>

      <Text style={styles.dash}>–</Text>

      <Pressable onPress={onEndPress} style={[styles.timeBtn, shadows.xs]}>
        <Text style={styles.timeText}>{range.endTime}</Text>
      </Pressable>

      <View style={styles.spacer} />

      <Pressable onPress={onRemove} style={styles.removeBtn}>
        <Text style={styles.removeText}>✕</Text>
      </Pressable>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: '#f9fafb',
    borderRadius: 12,
    padding: 12,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
    borderCurve: 'continuous',
  },
  timeBtn: {
    backgroundColor: '#fff',
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderCurve: 'continuous',
  },
  timeText: {
    color: '#111827',
    fontWeight: '600',
    fontSize: 16,
    fontVariant: ['tabular-nums'],
  },
  dash: {
    color: '#9ca3af',
    fontWeight: '500',
  },
  spacer: { flex: 1 },
  removeBtn: {
    backgroundColor: '#fef2f2',
    borderRadius: 8,
    padding: 8,
    borderCurve: 'continuous',
  },
  removeText: {
    color: '#ef4444',
    fontWeight: '700',
    fontSize: 14,
  },
});
