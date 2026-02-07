import { useState, useMemo } from 'react';
import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, Pressable } from '@/src/tw';
import { AppointmentCard } from '@/components/appointment-card';
import { EmptyState } from '@/components/empty-state';
import { useStore } from '@/store/store';
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
    () =>
      filter === 'all'
        ? appointments
        : appointments.filter((a) => a.status === filter),
    [appointments, filter]
  );

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <Text className="text-white text-2xl font-bold mt-4">{t('requests.title')}</Text>
        </SafeAreaView>
      </View>

      <View className="px-6 pt-4">
        <ScrollView horizontal showsHorizontalScrollIndicator={false}>
          <View className="flex-row gap-2">
            {filters.map((f) => (
              <Pressable
                key={f.key}
                onPress={() => setFilter(f.key)}
                className={`px-4 py-2 rounded-full ${
                  filter === f.key ? 'bg-navy' : 'bg-white'
                }`}
              >
                <Text
                  className={`text-sm font-semibold ${
                    filter === f.key ? 'text-white' : 'text-gray-600'
                  }`}
                >
                  {f.label}
                </Text>
              </Pressable>
            ))}
          </View>
        </ScrollView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-3 pb-8">
        {filtered.length > 0 ? (
          filtered.map((appointment) => (
            <AppointmentCard
              key={appointment.id}
              appointment={appointment}
              onPress={() =>
                router.push(`/(provider)/request-detail/${appointment.id}`)
              }
            />
          ))
        ) : (
          <EmptyState message={t('requests.noRequests')} />
        )}
      </ScrollView>
    </View>
  );
}
