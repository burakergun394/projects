import { CalendarX2 } from 'lucide-react-native';
import { View, Text } from '@/src/tw';

interface EmptyStateProps {
  message: string;
}

export const EmptyState = ({ message }: EmptyStateProps) => {
  return (
    <View className="flex-1 items-center justify-center py-16 gap-4">
      <View
        className="rounded-2xl items-center justify-center"
        style={{
          width: 64,
          height: 64,
          borderCurve: 'continuous',
          backgroundColor: 'rgba(26,35,126,0.06)',
        }}
      >
        <CalendarX2 size={28} color="#9CA3AF" strokeWidth={1.5} />
      </View>
      <Text className="text-gray-400 text-base text-center">{message}</Text>
    </View>
  );
};
