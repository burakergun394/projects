import { View, Text, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { colors } from '@/src/theme';
import type { AppointmentStatus } from '@/store/types';

const statusStyles: Record<AppointmentStatus, { bg: string; text: string }> = {
  pending: { bg: 'rgba(255,152,0,0.2)', text: colors.orange },
  approved: { bg: '#dcfce7', text: '#15803d' },
  rejected: { bg: '#fee2e2', text: '#b91c1c' },
};

export const StatusBadge = ({ status }: { status: AppointmentStatus }) => {
  const { t } = useTranslation();
  const variant = statusStyles[status];

  return (
    <View style={[styles.badge, { backgroundColor: variant.bg }]}>
      <Text style={[styles.text, { color: variant.text }]}>{t(`common.${status}`)}</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  badge: {
    paddingHorizontal: 12,
    paddingVertical: 4,
    borderRadius: 9999,
  },
  text: {
    fontSize: 12,
    fontWeight: '600',
  },
});
