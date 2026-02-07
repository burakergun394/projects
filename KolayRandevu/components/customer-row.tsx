import { View, Text, Pressable } from '@/src/tw';
import { useTranslation } from 'react-i18next';
import type { Customer } from '@/store/types';

interface CustomerRowProps {
  customer: Customer;
  onPress?: () => void;
}

export const CustomerRow = ({ customer, onPress }: CustomerRowProps) => {
  const { t } = useTranslation();

  return (
    <Pressable
      onPress={onPress}
      className="bg-white rounded-2xl p-4 flex-row items-center gap-3"
      style={{ borderCurve: 'continuous', boxShadow: '0 1px 3px rgba(0,0,0,0.08)' }}
    >
      <View className="bg-navy rounded-full w-11 h-11 items-center justify-center">
        <Text className="text-white font-bold text-base">
          {customer.name.charAt(0)}
        </Text>
      </View>
      <View className="flex-1 gap-0.5">
        <Text className="text-gray-900 font-semibold text-base">{customer.name}</Text>
        <Text className="text-gray-500 text-sm">
          {customer.appointmentCount} {t('customers.visits')}
        </Text>
      </View>
      <Text className="text-gray-400 text-xs">{customer.lastVisit}</Text>
    </Pressable>
  );
};
