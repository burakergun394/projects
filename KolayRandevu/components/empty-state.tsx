import { CalendarX2 } from 'lucide-react-native';
import { View, Text } from '@/src/tw';
import { IconCircle } from '@/components/ui/icon-circle';

interface EmptyStateProps {
  message: string;
}

export const EmptyState = ({ message }: EmptyStateProps) => {
  return (
    <View className="flex-1 items-center justify-center py-16 gap-4">
      <IconCircle icon={CalendarX2} size="xl" variant="muted" strokeWidth={1.5} />
      <Text className="text-gray-400 text-base text-center">{message}</Text>
    </View>
  );
};
