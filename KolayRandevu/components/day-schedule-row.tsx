import { Switch, View, Text, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Card } from '@/components/ui/card';
import { colors } from '@/src/theme';
import type { DaySchedule } from '@/store/types';

interface DayScheduleRowProps {
  schedule: DaySchedule;
  onToggle: (isActive: boolean) => void;
  onPress: () => void;
}

export const DayScheduleRow = ({ schedule, onToggle, onPress }: DayScheduleRowProps) => {
  const { t } = useTranslation();

  const rangesSummary = schedule.ranges.map((r) => `${r.startTime}–${r.endTime}`).join(', ');

  return (
    <Card shadow="xs" onPress={onPress} style={styles.card}>
      <View style={styles.topRow}>
        <View style={styles.flex1}>
          <Text style={[styles.dayName, !schedule.isActive && styles.dayNameInactive]}>
            {t(`schedule.${schedule.day}`)}
          </Text>
        </View>
        <Switch
          value={schedule.isActive}
          onValueChange={onToggle}
          trackColor={{ false: colors.chevron, true: colors.orange }}
          thumbColor={colors.white}
        />
      </View>

      {schedule.isActive ? (
        <View style={styles.details}>
          {rangesSummary ? (
            <Text style={styles.rangeText}>{rangesSummary}</Text>
          ) : (
            <Text style={styles.noRangeText}>{t('schedule.noRanges')}</Text>
          )}
          <Text style={styles.slotText}>
            {schedule.slots.length > 0
              ? t('schedule.slotCount', { count: schedule.slots.length })
              : t('schedule.noSlots')}
          </Text>
        </View>
      ) : (
        <Text style={styles.noRangeText}>{t('schedule.dayOff')}</Text>
      )}
    </Card>
  );
};

const styles = StyleSheet.create({
  card: { padding: 16, gap: 8 },
  topRow: { flexDirection: 'row', alignItems: 'center', gap: 12 },
  flex1: { flex: 1 },
  dayName: { fontWeight: '600', fontSize: 16, color: '#111827' },
  dayNameInactive: { color: '#9ca3af' },
  details: { gap: 4 },
  rangeText: { color: '#6b7280', fontSize: 14 },
  noRangeText: { color: '#9ca3af', fontSize: 14 },
  slotText: { color: colors.orange, fontWeight: '500', fontSize: 14 },
});
