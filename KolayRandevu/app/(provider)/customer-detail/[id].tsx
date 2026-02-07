import { useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, Pressable, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { AppointmentCard } from '@/components/appointment-card';
import { EmptyState } from '@/components/empty-state';
import { Card } from '@/components/ui/card';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';

export default function CustomerDetail() {
  const { id } = useLocalSearchParams<{ id: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const customers = useStore((s) => s.customers);
  const appointments = useStore((s) => s.appointments);

  const customer = customers.find((c) => c.id === id);
  const customerAppointments = appointments
    .filter((a) => a.customerId === id)
    .sort((a, b) => b.date.localeCompare(a.date));

  if (!customer) {
    return (
      <View style={styles.emptyScreen}>
        <Text style={styles.emptyText}>{t('common.noData')}</Text>
      </View>
    );
  }

  return (
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <Pressable onPress={() => router.back()} style={styles.backBtn}>
            <Text style={styles.backText}>{t('common.back')}</Text>
          </Pressable>
          <View style={styles.profileRow}>
            <View style={styles.avatar}>
              <Text style={styles.avatarText}>{customer.name.charAt(0)}</Text>
            </View>
            <View style={styles.profileInfo}>
              <Text style={styles.profileName}>{customer.name}</Text>
              <Text style={styles.profilePhone}>{customer.phone}</Text>
            </View>
          </View>
        </SafeAreaView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
        <Card shadow="sm" style={styles.statsCard}>
          <View style={styles.statItem}>
            <Text style={styles.statValue}>{customer.appointmentCount}</Text>
            <Text style={styles.statLabel}>{t('customers.visits')}</Text>
          </View>
          <View style={styles.statItem}>
            <Text style={styles.statValue}>{customer.lastVisit}</Text>
            <Text style={styles.statLabel}>{t('customers.lastVisit')}</Text>
          </View>
        </Card>

        <Text style={styles.sectionTitle}>{t('customers.appointmentHistory')}</Text>

        {customerAppointments.length > 0 ? (
          <View style={styles.appointmentList}>
            {customerAppointments.map((appointment) => (
              <AppointmentCard key={appointment.id} appointment={appointment} />
            ))}
          </View>
        ) : (
          <EmptyState message={t('common.noData')} />
        )}
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  emptyScreen: {
    flex: 1,
    backgroundColor: '#f9fafb',
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyText: { color: '#9ca3af' },
  header: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
  backBtn: { paddingVertical: 8 },
  backText: { color: 'rgba(255,255,255,0.7)', fontSize: 14 },
  profileRow: { flexDirection: 'row', alignItems: 'center', gap: 16, marginTop: 8 },
  avatar: {
    backgroundColor: 'rgba(255,255,255,0.2)',
    borderRadius: 9999,
    width: 56,
    height: 56,
    alignItems: 'center',
    justifyContent: 'center',
  },
  avatarText: { color: '#fff', fontWeight: '700', fontSize: 20 },
  profileInfo: { gap: 4 },
  profileName: { color: '#fff', fontSize: 20, fontWeight: '700' },
  profilePhone: { color: 'rgba(255,255,255,0.7)', fontSize: 14 },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 16, gap: 16, paddingBottom: 32 },
  statsCard: { padding: 16, flexDirection: 'row', gap: 24 },
  statItem: { alignItems: 'center' },
  statValue: {
    color: colors.navy,
    fontSize: 24,
    fontWeight: '700',
    fontVariant: ['tabular-nums'],
  },
  statLabel: { color: '#6b7280', fontSize: 12 },
  sectionTitle: { color: '#111827', fontWeight: '700', fontSize: 18 },
  appointmentList: { gap: 12 },
});
