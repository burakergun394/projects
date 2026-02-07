import type { ElementType } from 'react';
import { View, Text } from '@/src/tw';

interface StatCardProps {
  label: string;
  value: number | string;
  accent?: boolean;
  icon?: ElementType;
}

export const StatCard = ({ label, value, accent = false, icon: Icon }: StatCardProps) => {
  return (
    <View
      className="flex-1 bg-white rounded-2xl p-4 gap-3"
      style={{
        borderCurve: 'continuous',
        boxShadow: '0 2px 8px rgba(0,0,0,0.06)',
      }}
    >
      <View className="flex-row items-center justify-between">
        {Icon && (
          <View
            className="rounded-xl items-center justify-center"
            style={{
              width: 40,
              height: 40,
              borderCurve: 'continuous',
              backgroundColor: accent ? 'rgba(255,152,0,0.1)' : 'rgba(26,35,126,0.08)',
            }}
          >
            <Icon size={20} color={accent ? '#FF9800' : '#1A237E'} strokeWidth={2} />
          </View>
        )}
        <Text
          className="text-navy font-bold"
          style={{ fontSize: 28, fontVariant: ['tabular-nums'] }}
        >
          {value}
        </Text>
      </View>
      <Text className="text-gray-500 text-xs">{label}</Text>
    </View>
  );
};
