import { useState, useMemo } from 'react';
import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, TextInput } from '@/src/tw';
import { CustomerRow } from '@/components/customer-row';
import { EmptyState } from '@/components/empty-state';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';

export default function Customers() {
  const { t } = useTranslation();
  const router = useRouter();
  const customers = useStore((s) => s.customers);
  const [search, setSearch] = useState('');

  const filtered = useMemo(
    () =>
      search.trim()
        ? customers.filter((c) => c.name.toLowerCase().includes(search.toLowerCase()))
        : customers,
    [customers, search]
  );

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <Text className="text-white text-2xl font-bold mt-4">{t('customers.title')}</Text>
          <Text className="text-white/70 text-sm mt-1">
            {t('customers.totalCustomers')}: {customers.length}
          </Text>
        </SafeAreaView>
      </View>

      <View className="px-6 pt-4">
        <TextInput
          className="bg-white rounded-xl px-4 py-3 text-gray-900"
          placeholder={t('customers.searchPlaceholder')}
          placeholderTextColor={colors.muted}
          value={search}
          onChangeText={setSearch}
        />
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-3 pb-8">
        {filtered.length > 0 ? (
          filtered.map((customer) => (
            <CustomerRow
              key={customer.id}
              customer={customer}
              onPress={() => router.push(`/(provider)/customer-detail/${customer.id}` as never)}
            />
          ))
        ) : (
          <EmptyState message={t('customers.noCustomers')} />
        )}
      </ScrollView>
    </View>
  );
}
