import type { ElementType } from 'react';
import { View, Text } from '@/src/tw';
import { Card } from '@/components/ui/card';
import { IconCircle } from '@/components/ui/icon-circle';

interface StatCardProps {
  label: string;
  value: number | string;
  accent?: boolean;
  icon?: ElementType;
}

export const StatCard = ({ label, value, accent = false, icon: Icon }: StatCardProps) => {
  return (
    <Card shadow="md" className="flex-1 p-4 gap-3">
      <View className="flex-row items-center justify-between">
        {Icon && (
          <IconCircle icon={Icon} size="sm" variant={accent ? 'orange' : 'navy'} strokeWidth={2} />
        )}
        <Text className="text-navy font-bold text-3xl tabular-nums">{value}</Text>
      </View>
      <Text className="text-gray-500 text-xs">{label}</Text>
    </Card>
  );
};
