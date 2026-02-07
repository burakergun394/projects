import { View, Text } from '@/src/tw';

interface EmptyStateProps {
  message: string;
}

export const EmptyState = ({ message }: EmptyStateProps) => {
  return (
    <View className="flex-1 items-center justify-center py-16">
      <Text className="text-gray-400 text-base text-center">{message}</Text>
    </View>
  );
};
