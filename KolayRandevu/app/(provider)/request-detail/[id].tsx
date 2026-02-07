import { useLocalSearchParams, useRouter } from 'expo-router';
import { Alert, View, Text, ScrollView, Pressable, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { StatusBadge } from '@/components/status-badge';
import { Button } from '@/components/Button';
import { Card } from '@/components/ui/card';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';

export default function RequestDetail() {
  const { id } = useLocalSearchParams<{ id: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const appointments = useStore((s) => s.appointments);
  const updateStatus = useStore((s) => s.updateAppointmentStatus);

  const appointment = appointments.find((a) => a.id === id);

  if (!appointment) {
    return (
      <View style={styles.emptyScreen}>
        <Text style={styles.emptyText}>{t('common.noData')}</Text>
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
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <Pressable onPress={() => router.back()} style={styles.backBtn}>
            <Text style={styles.backText}>{t('common.back')}</Text>
          </Pressable>
          <Text style={styles.headerTitle}>{appointment.customerName}</Text>
        </SafeAreaView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
        <Card shadow="sm" style={styles.detailCard}>
          <View style={styles.statusRow}>
            <Text style={styles.detailLabel}>{t('requests.status')}</Text>
            <StatusBadge status={appointment.status} />
          </View>
          <View style={styles.divider} />
          <DetailRow label={t('requests.customer')} value={appointment.customerName} />
          <DetailRow label={t('requests.service')} value={appointment.service} />
          <DetailRow label={t('requests.date')} value={appointment.date} />
          <DetailRow label={t('requests.time')} value={appointment.time} />
          {appointment.note && <DetailRow label={t('requests.note')} value={appointment.note} />}
        </Card>

        {appointment.status === 'pending' && (
          <View style={styles.actions}>
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
    <View style={detailRowStyles.row}>
      <Text style={detailRowStyles.label}>{label}</Text>
      <Text style={detailRowStyles.value}>{value}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  emptyScreen: {
    flex: 1,
    backgroundColor: '#f9fafb',
    alignItems: 'center',
    justifyContent: 'center',
  },
  emptyText: { color: '#9ca3af' },
  header: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
  backBtn: { paddingVertical: 8 },
  backText: { color: 'rgba(255,255,255,0.7)', fontSize: 14 },
  headerTitle: { color: '#fff', fontSize: 24, fontWeight: '700', marginTop: 8 },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 24, gap: 24 },
  detailCard: { padding: 20, gap: 16 },
  statusRow: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' },
  detailLabel: { color: '#6b7280', fontSize: 14 },
  divider: { height: 1, backgroundColor: '#f3f4f6' },
  actions: { gap: 12 },
});

const detailRowStyles = StyleSheet.create({
  row: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' },
  label: { color: '#6b7280', fontSize: 14 },
  value: { color: '#111827', fontWeight: '500', fontSize: 14 },
});
