import { useLocalSearchParams, useRouter } from 'expo-router';
import { Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, Pressable } from '@/src/tw';
import { StatusBadge } from '@/components/status-badge';
import { Button } from '@/components/Button';
import { useStore } from '@/store/store';

export default function RequestDetail() {
  const { id } = useLocalSearchParams<{ id: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const appointments = useStore((s) => s.appointments);
  const updateStatus = useStore((s) => s.updateAppointmentStatus);

  const appointment = appointments.find((a) => a.id === id);

  if (!appointment) {
    return (
      <View className="flex-1 bg-gray-50 items-center justify-center">
        <Text className="text-gray-400">{t('common.noData')}</Text>
      </View>
    );
  }

  const handleApprove = () => {
    Alert.alert(t('requests.approveConfirm'), '', [
      { text: t('common.cancel'), style: 'cancel' },
      {
        text: t('common.approve'),
        onPress: () => {
          updateStatus(appointment.id, 'approved');
          router.back();
        },
      },
    ]);
  };

  const handleReject = () => {
    Alert.alert(t('requests.rejectConfirm'), '', [
      { text: t('common.cancel'), style: 'cancel' },
      {
        text: t('common.reject'),
        style: 'destructive',
        onPress: () => {
          updateStatus(appointment.id, 'rejected');
          router.back();
        },
      },
    ]);
  };

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <Pressable onPress={() => router.back()} className="py-2">
            <Text className="text-white/70 text-sm">{t('common.back')}</Text>
          </Pressable>
          <Text className="text-white text-2xl font-bold mt-2">
            {appointment.customerName}
          </Text>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-6 gap-6">
        <View
          className="bg-white rounded-2xl p-5 gap-4"
          style={{ borderCurve: 'continuous', boxShadow: '0 1px 3px rgba(0,0,0,0.08)' }}
        >
          <View className="flex-row justify-between items-center">
            <Text className="text-gray-500 text-sm">{t('requests.status')}</Text>
            <StatusBadge status={appointment.status} />
          </View>
          <View className="h-px bg-gray-100" />
          <DetailRow label={t('requests.customer')} value={appointment.customerName} />
          <DetailRow label={t('requests.service')} value={appointment.service} />
          <DetailRow label={t('requests.date')} value={appointment.date} />
          <DetailRow label={t('requests.time')} value={appointment.time} />
          {appointment.note && (
            <DetailRow label={t('requests.note')} value={appointment.note} />
          )}
        </View>

        {appointment.status === 'pending' && (
          <View className="gap-3">
            <Button title={t('common.approve')} variant="primary" onPress={handleApprove} />
            <Button title={t('common.reject')} variant="danger" onPress={handleReject} />
          </View>
        )}
      </ScrollView>
    </View>
  );
}

function DetailRow({ label, value }: { label: string; value: string }) {
  return (
    <View className="flex-row justify-between items-center">
      <Text className="text-gray-500 text-sm">{label}</Text>
      <Text className="text-gray-900 font-medium text-sm">{value}</Text>
    </View>
  );
}
