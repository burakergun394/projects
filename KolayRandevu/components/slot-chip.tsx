import { View, Text, Pressable } from '@/src/tw';

interface SlotChipProps {
  time: string;
  isOutsideRange?: boolean;
  onRemove: () => void;
}

export const SlotChip = ({ time, isOutsideRange, onRemove }: SlotChipProps) => {
  return (
    <View
      className={`flex-row items-center rounded-xl px-3 py-2 gap-2 ${
        isOutsideRange ? 'bg-red-50' : 'bg-gray-50'
      }`}
      style={{ borderCurve: 'continuous' }}
    >
      <Text
        className={`font-semibold text-sm ${isOutsideRange ? 'text-red-500' : 'text-gray-900'}`}
        style={{ fontVariant: ['tabular-nums'] }}
      >
        {time}
      </Text>
      <Pressable onPress={onRemove} className="ml-0.5">
        <Text className={`font-bold text-xs ${isOutsideRange ? 'text-red-400' : 'text-gray-400'}`}>
          ✕
        </Text>
      </Pressable>
    </View>
  );
};
