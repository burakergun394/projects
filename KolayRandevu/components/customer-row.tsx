import { View, Text } from '@/src/tw';
import { useTranslation } from 'react-i18next';
import { Card } from '@/components/ui/card';
import type { Customer } from '@/store/types';

interface CustomerRowProps {
  customer: Customer;
  onPress?: () => void;
}

export const CustomerRow = ({ customer, onPress }: CustomerRowProps) => {
  const { t } = useTranslation();

  return (
    <Card shadow="sm" onPress={onPress} className="p-4 flex-row items-center gap-3">
      <View className="bg-navy rounded-full w-11 h-11 items-center justify-center">
        <Text className="text-white font-bold text-base">{customer.name.charAt(0)}</Text>
      </View>
      <View className="flex-1 gap-0.5">
        <Text className="text-gray-900 font-semibold text-base">{customer.name}</Text>
        <Text className="text-gray-500 text-sm">
          {customer.appointmentCount} {t('customers.visits')}
        </Text>
      </View>
      <Text className="text-gray-400 text-xs">{customer.lastVisit}</Text>
    </Card>
  );
};
