import { Alert, View, Text, Pressable, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Check, X } from 'lucide-react-native';
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
    <Card shadow="lg" onPress={onPress} style={styles.card}>
      {/* Main row */}
      <View style={styles.mainRow}>
        <View style={[styles.timeBox, isPending ? styles.timeBoxPending : styles.timeBoxDefault]}>
          <Text
            style={[styles.timeText, isPending ? styles.timeTextPending : styles.timeTextDefault]}>
            {appointment.time}
          </Text>
        </View>
        <View style={styles.info}>
          <Text style={styles.name}>{appointment.customerName}</Text>
          <Text style={styles.service}>{appointment.service}</Text>
        </View>
        {!isPending && <StatusBadge status={appointment.status} />}
      </View>

      {/* Action buttons for pending */}
      {isPending && (
        <View style={styles.actions}>
          <Pressable onPress={handleApprove} style={styles.approveBtn}>
            <Check size={16} color={colors.white} strokeWidth={2.5} />
            <Text style={styles.approveBtnText}>{t('common.approve')}</Text>
          </Pressable>
          <Pressable onPress={handleReject} style={styles.rejectBtn}>
            <X size={16} color="#6b7280" strokeWidth={2.5} />
            <Text style={styles.rejectBtnText}>{t('common.reject')}</Text>
          </Pressable>
        </View>
      )}
    </Card>
  );
};

const styles = StyleSheet.create({
  card: { padding: 16, gap: 12, borderRadius: 12 },
  mainRow: { flexDirection: 'row', alignItems: 'center', gap: 12 },
  timeBox: {
    width: 48,
    height: 48,
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    borderCurve: 'continuous',
  },
  timeBoxPending: { backgroundColor: 'rgba(255,152,0,0.1)' },
  timeBoxDefault: { backgroundColor: 'rgba(26,35,126,0.08)' },
  timeText: { fontWeight: '700', fontSize: 14, fontVariant: ['tabular-nums'] },
  timeTextPending: { color: colors.orange },
  timeTextDefault: { color: colors.navy },
  info: { flex: 1, gap: 2 },
  name: { color: '#111827', fontWeight: '600', fontSize: 16 },
  service: { color: '#9ca3af', fontSize: 14 },
  actions: { flexDirection: 'row', gap: 12, paddingTop: 4 },
  approveBtn: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    borderRadius: 12,
    paddingVertical: 10,
    backgroundColor: colors.orange,
    borderCurve: 'continuous',
  },
  approveBtnText: { color: '#fff', fontWeight: '600', fontSize: 14 },
  rejectBtn: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    borderRadius: 12,
    paddingVertical: 10,
    borderWidth: 1,
    borderColor: colors.cardBorder,
    borderCurve: 'continuous',
  },
  rejectBtnText: { color: '#6b7280', fontWeight: '600', fontSize: 14 },
});
