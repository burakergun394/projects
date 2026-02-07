import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView } from '@/src/tw';
import { StatCard } from '@/components/stat-card';
import { AppointmentCard } from '@/components/appointment-card';
import { SectionHeader } from '@/components/section-header';
import { EmptyState } from '@/components/empty-state';
import { LanguageToggle } from '@/components/language-toggle';
import { useStore } from '@/store/store';

export default function Dashboard() {
  const { t } = useTranslation();
  const provider = useStore((s) => s.provider);
  const appointments = useStore((s) => s.appointments);
  const customers = useStore((s) => s.customers);

  const today = new Date().toISOString().split('T')[0];
  const todayAppointments = appointments
    .filter((a) => a.date === today)
    .sort((a, b) => a.time.localeCompare(b.time));
  const pendingCount = appointments.filter((a) => a.status === 'pending').length;
  const weeklyCompleted = appointments.filter((a) => a.status === 'approved').length;

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2 rounded-b-3xl">
        <SafeAreaView edges={['top']}>
          <View className="flex-row items-center justify-between mb-4">
            <View />
            <LanguageToggle />
          </View>
          <Text className="text-white/70 text-sm">{t('dashboard.title')}</Text>
          <Text className="text-white text-2xl font-bold mt-1">
            {t('dashboard.greeting', { name: provider.name.split(' ')[0] })}
          </Text>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-4 pb-8">
        <View className="flex-row gap-3">
          <StatCard label={t('dashboard.todayAppointments')} value={todayAppointments.length} />
          <StatCard label={t('dashboard.pendingRequests')} value={pendingCount} accent />
        </View>
        <View className="flex-row gap-3">
          <StatCard label={t('dashboard.totalCustomers')} value={customers.length} />
          <StatCard label={t('dashboard.weeklyCompleted')} value={weeklyCompleted} />
        </View>

        <SectionHeader title={t('dashboard.upcomingToday')} />

        {todayAppointments.length > 0 ? (
          <View className="gap-3">
            {todayAppointments.map((appointment) => (
              <AppointmentCard key={appointment.id} appointment={appointment} />
            ))}
          </View>
        ) : (
          <EmptyState message={t('dashboard.noAppointments')} />
        )}
      </ScrollView>
    </View>
  );
}
