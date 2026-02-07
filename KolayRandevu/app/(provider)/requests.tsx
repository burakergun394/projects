import { useState, useMemo } from 'react';
import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, Pressable, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { AppointmentCard } from '@/components/appointment-card';
import { EmptyState } from '@/components/empty-state';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';
import type { AppointmentStatus } from '@/store/types';

type Filter = 'all' | AppointmentStatus;

export default function Requests() {
  const { t } = useTranslation();
  const router = useRouter();
  const appointments = useStore((s) => s.appointments);
  const [filter, setFilter] = useState<Filter>('all');

  const filters: { key: Filter; label: string }[] = [
    { key: 'all', label: t('common.all') },
    { key: 'pending', label: t('common.pending') },
    { key: 'approved', label: t('common.approved') },
    { key: 'rejected', label: t('common.rejected') },
  ];

  const filtered = useMemo(
    () => (filter === 'all' ? appointments : appointments.filter((a) => a.status === filter)),
    [appointments, filter]
  );

  return (
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <Text style={styles.headerTitle}>{t('requests.title')}</Text>
        </SafeAreaView>
      </View>

      <View style={styles.filterContainer}>
        <ScrollView horizontal showsHorizontalScrollIndicator={false}>
          <View style={styles.filterRow}>
            {filters.map((f) => (
              <Pressable
                key={f.key}
                onPress={() => setFilter(f.key)}
                style={[
                  styles.filterChip,
                  filter === f.key ? styles.filterActive : styles.filterInactive,
                ]}>
                <Text
                  style={[
                    styles.filterText,
                    filter === f.key ? styles.filterTextActive : styles.filterTextInactive,
                  ]}>
                  {f.label}
                </Text>
              </Pressable>
            ))}
          </View>
        </ScrollView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
        {filtered.length > 0 ? (
          filtered.map((appointment) => (
            <AppointmentCard
              key={appointment.id}
              appointment={appointment}
              onPress={() => router.push(`/(provider)/request-detail/${appointment.id}` as never)}
            />
          ))
        ) : (
          <EmptyState message={t('requests.noRequests')} />
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
  filterContainer: { paddingHorizontal: 24, paddingTop: 16 },
  filterRow: { flexDirection: 'row', gap: 8 },
  filterChip: { paddingHorizontal: 16, paddingVertical: 8, borderRadius: 9999 },
  filterActive: { backgroundColor: colors.navy },
  filterInactive: { backgroundColor: '#fff' },
  filterText: { fontSize: 14, fontWeight: '600' },
  filterTextActive: { color: '#fff' },
  filterTextInactive: { color: '#4b5563' },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 16, gap: 12, paddingBottom: 32 },
});
