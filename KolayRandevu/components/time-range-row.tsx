import { View, Text, Pressable } from '@/src/tw';
import type { TimeRange } from '@/store/types';

interface TimeRangeRowProps {
  range: TimeRange;
  onStartPress: () => void;
  onEndPress: () => void;
  onRemove: () => void;
}

export const TimeRangeRow = ({ range, onStartPress, onEndPress, onRemove }: TimeRangeRowProps) => {
  return (
    <View
      className="bg-gray-50 rounded-xl p-3 flex-row items-center gap-3"
      style={{ borderCurve: 'continuous' }}
    >
      <Pressable
        onPress={onStartPress}
        className="bg-white rounded-lg px-4 py-2"
        style={{ borderCurve: 'continuous', boxShadow: '0 1px 2px rgba(0,0,0,0.06)' }}
      >
        <Text className="text-gray-900 font-semibold text-base" style={{ fontVariant: ['tabular-nums'] }}>
          {range.startTime}
        </Text>
      </Pressable>

      <Text className="text-gray-400 font-medium">–</Text>

      <Pressable
        onPress={onEndPress}
        className="bg-white rounded-lg px-4 py-2"
        style={{ borderCurve: 'continuous', boxShadow: '0 1px 2px rgba(0,0,0,0.06)' }}
      >
        <Text className="text-gray-900 font-semibold text-base" style={{ fontVariant: ['tabular-nums'] }}>
          {range.endTime}
        </Text>
      </Pressable>

      <View className="flex-1" />

      <Pressable onPress={onRemove} className="bg-red-50 rounded-lg p-2" style={{ borderCurve: 'continuous' }}>
        <Text className="text-red-500 font-bold text-sm">✕</Text>
      </Pressable>
    </View>
  );
};
