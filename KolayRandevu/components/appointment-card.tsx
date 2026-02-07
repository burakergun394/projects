import { View, Text, Pressable } from '@/src/tw';
import { StatusBadge } from './status-badge';
import type { Appointment } from '@/store/types';

interface AppointmentCardProps {
  appointment: Appointment;
  onPress?: () => void;
}

export const AppointmentCard = ({ appointment, onPress }: AppointmentCardProps) => {
  return (
    <Pressable
      onPress={onPress}
      className="bg-white rounded-2xl p-4 flex-row items-center gap-3"
      style={{ borderCurve: 'continuous', boxShadow: '0 1px 3px rgba(0,0,0,0.08)' }}
    >
      <View className="bg-navy/10 rounded-xl w-12 h-12 items-center justify-center">
        <Text className="text-navy font-bold text-sm">{appointment.time}</Text>
      </View>
      <View className="flex-1 gap-1">
        <Text className="text-gray-900 font-semibold text-base">
          {appointment.customerName}
        </Text>
        <Text className="text-gray-500 text-sm">{appointment.service}</Text>
      </View>
      <StatusBadge status={appointment.status} />
    </Pressable>
  );
};
