import { useState, useMemo } from 'react';
import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, TextInput, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
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
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <Text style={styles.headerTitle}>{t('customers.title')}</Text>
          <Text style={styles.headerSub}>
            {t('customers.totalCustomers')}: {customers.length}
          </Text>
        </SafeAreaView>
      </View>

      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder={t('customers.searchPlaceholder')}
          placeholderTextColor={colors.muted}
          value={search}
          onChangeText={setSearch}
        />
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
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

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  header: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
  headerTitle: { color: '#fff', fontSize: 24, fontWeight: '700', marginTop: 16 },
  headerSub: { color: 'rgba(255,255,255,0.7)', fontSize: 14, marginTop: 4 },
  searchContainer: { paddingHorizontal: 24, paddingTop: 16 },
  searchInput: {
    backgroundColor: '#fff',
    borderRadius: 12,
    paddingHorizontal: 16,
    paddingVertical: 12,
    color: '#111827',
  },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 16, gap: 12, paddingBottom: 32 },
});
