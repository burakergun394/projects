import { Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Check, X } from 'lucide-react-native';
import { View, Text, Pressable } from '@/src/tw';
import { StatusBadge } from './status-badge';
import { Card } from '@/components/ui/card';
import { colors } from '@/src/theme';
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
    <Card shadow="lg" onPress={onPress} className="p-4 gap-3 rounded-xl">
      {/* Main row */}
      <View className="flex-row items-center gap-3">
        <View
          className={`w-12 h-12 rounded-xl items-center justify-center border-continuous ${
            isPending ? 'bg-orange/10' : 'bg-navy/[0.08]'
          }`}>
          <Text
            className={`font-bold text-sm tabular-nums ${isPending ? 'text-orange' : 'text-navy'}`}>
            {appointment.time}
          </Text>
        </View>
        <View className="flex-1 gap-0.5">
          <Text className="text-gray-900 font-semibold text-base">{appointment.customerName}</Text>
          <Text className="text-gray-400 text-sm">{appointment.service}</Text>
        </View>
        {!isPending && <StatusBadge status={appointment.status} />}
      </View>

      {/* Action buttons for pending */}
      {isPending && (
        <View className="flex-row gap-3 pt-1">
          <Pressable
            onPress={handleApprove}
            className="flex-1 flex-row items-center justify-center gap-2 rounded-xl py-2.5 bg-orange border-continuous">
            <Check size={16} color={colors.white} strokeWidth={2.5} />
            <Text className="text-white font-semibold text-sm">{t('common.approve')}</Text>
          </Pressable>
          <Pressable
            onPress={handleReject}
            className="flex-1 flex-row items-center justify-center gap-2 rounded-xl py-2.5 border border-card-border border-continuous">
            <X size={16} color="#6b7280" strokeWidth={2.5} />
            <Text className="text-gray-500 font-semibold text-sm">{t('common.reject')}</Text>
          </Pressable>
        </View>
      )}
    </Card>
  );
};
