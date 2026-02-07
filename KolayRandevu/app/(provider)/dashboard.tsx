import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { CalendarDays, Clock, Users, CheckCircle2 } from 'lucide-react-native';
import { StatCard } from '@/components/stat-card';
import { AppointmentCard } from '@/components/appointment-card';
import { SectionHeader } from '@/components/section-header';
import { EmptyState } from '@/components/empty-state';
import { LanguageToggle } from '@/components/language-toggle';
import { useStore } from '@/store/store';
import { colors, gradients } from '@/src/theme';

export default function Dashboard() {
  const { t } = useTranslation();
  const provider = useStore((s) => s.provider);
  const appointments = useStore((s) => s.appointments);
  const customers = useStore((s) => s.customers);
  const updateAppointmentStatus = useStore((s) => s.updateAppointmentStatus);

  const today = new Date().toISOString().split('T')[0];
  const todayAppointments = appointments
    .filter((a) => a.date === today)
    .sort((a, b) => a.time.localeCompare(b.time));
  const pendingCount = appointments.filter((a) => a.status === 'pending').length;
  const weeklyCompleted = appointments.filter((a) => a.status === 'approved').length;

  const handleApprove = (id: string) => {
    updateAppointmentStatus(id, 'approved');
  };

  const handleReject = (id: string) => {
    updateAppointmentStatus(id, 'rejected');
  };

  return (
    <View style={styles.screen}>
      {/* Gradient header */}
      <View
        style={{
          paddingHorizontal: 24,
          paddingBottom: 28,
          paddingTop: 8,
          backgroundColor: colors.navyDeep,
          experimental_backgroundImage: gradients.dashboardHeader,
          borderBottomLeftRadius: 24,
          borderBottomRightRadius: 24,
        }}>
        <SafeAreaView edges={['top']}>
          <View style={styles.headerRow}>
            <View style={styles.headerTextGroup}>
              <Text style={styles.headerLabel}>{t('dashboard.title')}</Text>
              <Text style={styles.headerGreeting}>
                {t('dashboard.greeting', { name: provider.name.split(' ')[0] })}
              </Text>
            </View>
            <LanguageToggle />
          </View>

          {/* Business info pill */}
          <View style={styles.pill}>
            <View style={styles.pillDot} />
            <Text style={styles.pillText}>{provider.businessName}</Text>
          </View>
        </SafeAreaView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
        {/* Stats grid */}
        <View style={styles.statsRow}>
          <StatCard
            label={t('dashboard.todayAppointments')}
            value={todayAppointments.length}
            icon={CalendarDays}
          />
          <StatCard
            label={t('dashboard.pendingRequests')}
            value={pendingCount}
            icon={Clock}
            accent
          />
        </View>
        <View style={styles.statsRow}>
          <StatCard label={t('dashboard.totalCustomers')} value={customers.length} icon={Users} />
          <StatCard
            label={t('dashboard.weeklyCompleted')}
            value={weeklyCompleted}
            icon={CheckCircle2}
          />
        </View>

        {/* Today's appointments */}
        <SectionHeader title={t('dashboard.upcomingToday')} />

        {todayAppointments.length > 0 ? (
          <View style={styles.appointmentList}>
            {todayAppointments.map((appointment) => (
              <AppointmentCard
                key={appointment.id}
                appointment={appointment}
                onApprove={handleApprove}
                onReject={handleReject}
              />
            ))}
          </View>
        ) : (
          <EmptyState message={t('dashboard.noAppointments')} />
        )}
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 20, gap: 20, paddingBottom: 32 },
  headerRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginBottom: 20,
  },
  headerTextGroup: { gap: 2 },
  headerLabel: { color: 'rgba(255,255,255,0.5)', fontSize: 12 },
  headerGreeting: { color: '#fff', fontSize: 24, fontWeight: '700' },
  pill: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    alignSelf: 'flex-start',
    borderRadius: 9999,
    paddingHorizontal: 14,
    paddingVertical: 6,
    backgroundColor: 'rgba(255,255,255,0.1)',
  },
  pillDot: {
    width: 6,
    height: 6,
    borderRadius: 9999,
    backgroundColor: colors.statusOnline,
  },
  pillText: { color: 'rgba(255,255,255,0.7)', fontSize: 12 },
  statsRow: { flexDirection: 'row', gap: 12 },
  appointmentList: { gap: 12 },
});
