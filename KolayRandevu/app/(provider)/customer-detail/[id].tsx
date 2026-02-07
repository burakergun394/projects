import { useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, Pressable } from '@/src/tw';
import { AppointmentCard } from '@/components/appointment-card';
import { EmptyState } from '@/components/empty-state';
import { Card } from '@/components/ui/card';
import { useStore } from '@/store/store';

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
      <View className="flex-1 bg-gray-50 items-center justify-center">
        <Text className="text-gray-400">{t('common.noData')}</Text>
      </View>
    );
  }

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <Pressable onPress={() => router.back()} className="py-2">
            <Text className="text-white/70 text-sm">{t('common.back')}</Text>
          </Pressable>
          <View className="flex-row items-center gap-4 mt-2">
            <View className="bg-white/20 rounded-full w-14 h-14 items-center justify-center">
              <Text className="text-white font-bold text-xl">{customer.name.charAt(0)}</Text>
            </View>
            <View className="gap-1">
              <Text className="text-white text-xl font-bold">{customer.name}</Text>
              <Text className="text-white/70 text-sm">{customer.phone}</Text>
            </View>
          </View>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-4 pb-8">
        <Card shadow="sm" className="p-4 flex-row gap-6">
          <View className="items-center">
            <Text className="text-navy text-2xl font-bold tabular-nums">
              {customer.appointmentCount}
            </Text>
            <Text className="text-gray-500 text-xs">{t('customers.visits')}</Text>
          </View>
          <View className="items-center">
            <Text className="text-navy text-2xl font-bold tabular-nums">{customer.lastVisit}</Text>
            <Text className="text-gray-500 text-xs">{t('customers.lastVisit')}</Text>
          </View>
        </Card>

        <Text className="text-gray-900 font-bold text-lg">{t('customers.appointmentHistory')}</Text>

        {customerAppointments.length > 0 ? (
          <View className="gap-3">
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
