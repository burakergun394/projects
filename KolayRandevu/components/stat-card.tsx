import { View, Text } from '@/src/tw';

interface StatCardProps {
  label: string;
  value: number | string;
  accent?: boolean;
}

export const StatCard = ({ label, value, accent = false }: StatCardProps) => {
  return (
    <View
      className={`flex-1 rounded-2xl p-4 ${accent ? 'bg-orange' : 'bg-white'}`}
      style={{ borderCurve: 'continuous' }}
    >
      <Text
        className={`text-2xl font-bold ${accent ? 'text-white' : 'text-navy'}`}
        style={{ fontVariant: ['tabular-nums'] }}
      >
        {value}
      </Text>
      <Text className={`text-xs mt-1 ${accent ? 'text-white/80' : 'text-gray-500'}`}>
        {label}
      </Text>
    </View>
  );
};
