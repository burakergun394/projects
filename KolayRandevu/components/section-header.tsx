import { View, Text, Pressable } from '@/src/tw';

interface SectionHeaderProps {
  title: string;
  actionLabel?: string;
  onAction?: () => void;
}

export const SectionHeader = ({ title, actionLabel, onAction }: SectionHeaderProps) => {
  return (
    <View className="flex-row items-center justify-between">
      <Text className="text-gray-900 font-bold text-lg">{title}</Text>
      {actionLabel && onAction && (
        <Pressable onPress={onAction}>
          <Text className="text-orange font-semibold text-sm">{actionLabel}</Text>
        </Pressable>
      )}
    </View>
  );
};
