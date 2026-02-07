import { Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Check, X } from 'lucide-react-native';
import { View, Text, Pressable } from '@/src/tw';
import { StatusBadge } from './status-badge';
import type { Appointment } from '@/store/types';

interface AppointmentCardProps {
  appointment: Appointment;
  onApprove?: (id: string) => void;
  onReject?: (id: string) => void;
  onPress?: () => void;
}

export const AppointmentCard = ({
  appointment,
  onApprove,
  onReject,
  onPress,
}: AppointmentCardProps) => {
  const { t } = useTranslation();
  const isPending = appointment.status === 'pending';

  const handleApprove = () => {
    Alert.alert(t('requests.approveConfirm'), '', [
      { text: t('common.cancel'), style: 'cancel' },
      { text: t('common.approve'), onPress: () => onApprove?.(appointment.id) },
    ]);
  };

  const handleReject = () => {
    Alert.alert(t('requests.rejectConfirm'), '', [
      { text: t('common.cancel'), style: 'cancel' },
      { text: t('common.reject'), style: 'destructive', onPress: () => onReject?.(appointment.id) },
    ]);
  };

  return (
    <Pressable
      onPress={onPress}
      className="bg-white rounded-2xl p-4 gap-3"
      style={{
        borderCurve: 'continuous',
        borderRadius: 12,
        boxShadow: '0 2px 10px rgba(0,0,0,0.07)',
      }}
    >
      {/* Main row */}
      <View className="flex-row items-center gap-3">
        <View
          className="rounded-xl items-center justify-center"
          style={{
            width: 48,
            height: 48,
            borderCurve: 'continuous',
            backgroundColor: isPending ? 'rgba(255,152,0,0.1)' : 'rgba(26,35,126,0.08)',
          }}
        >
          <Text
            className="font-bold text-sm"
            style={{
              color: isPending ? '#FF9800' : '#1A237E',
              fontVariant: ['tabular-nums'],
            }}
          >
            {appointment.time}
          </Text>
        </View>
        <View className="flex-1 gap-0.5">
          <Text className="text-gray-900 font-semibold text-base">
            {appointment.customerName}
          </Text>
          <Text className="text-gray-400 text-sm">{appointment.service}</Text>
        </View>
        {!isPending && <StatusBadge status={appointment.status} />}
      </View>

      {/* Action buttons for pending */}
      {isPending && (
        <View className="flex-row gap-3 pt-1">
          <Pressable
            onPress={handleApprove}
            className="flex-1 flex-row items-center justify-center gap-2 rounded-xl py-2.5"
            style={{
              backgroundColor: '#FF9800',
              borderCurve: 'continuous',
            }}
          >
            <Check size={16} color="#fff" strokeWidth={2.5} />
            <Text className="text-white font-semibold text-sm">{t('common.approve')}</Text>
          </Pressable>
          <Pressable
            onPress={handleReject}
            className="flex-1 flex-row items-center justify-center gap-2 rounded-xl py-2.5"
            style={{
              borderWidth: 1,
              borderColor: '#e5e7eb',
              borderCurve: 'continuous',
            }}
          >
            <X size={16} color="#6b7280" strokeWidth={2.5} />
            <Text className="text-gray-500 font-semibold text-sm">{t('common.reject')}</Text>
          </Pressable>
        </View>
      )}
    </Pressable>
  );
};
