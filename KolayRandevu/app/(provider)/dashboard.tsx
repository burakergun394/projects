import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { CalendarDays, Clock, Users, CheckCircle2 } from 'lucide-react-native';
import { View, Text, ScrollView } from '@/src/tw';
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
    <View className="flex-1 bg-gray-50">
      {/* Gradient header */}
      <View
        className="px-6 pb-7 pt-2"
        style={{
          backgroundColor: colors.navyDeep,
          experimental_backgroundImage: gradients.dashboardHeader,
          borderBottomLeftRadius: 24,
          borderBottomRightRadius: 24,
        }}>
        <SafeAreaView edges={['top']}>
          <View className="flex-row items-center justify-between mb-5">
            <View className="gap-0.5">
              <Text className="text-white/50 text-xs">{t('dashboard.title')}</Text>
              <Text className="text-white text-2xl font-bold">
                {t('dashboard.greeting', { name: provider.name.split(' ')[0] })}
              </Text>
            </View>
            <LanguageToggle />
          </View>

          {/* Business info pill */}
          <View className="flex-row items-center gap-2 self-start rounded-full px-3.5 py-1.5 bg-white/10">
            <View className="w-1.5 h-1.5 rounded-full bg-status-online" />
            <Text className="text-white/70 text-xs">{provider.businessName}</Text>
          </View>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-5 gap-5 pb-8">
        {/* Stats grid */}
        <View className="flex-row gap-3">
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
        <View className="flex-row gap-3">
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
          <View className="gap-3">
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
