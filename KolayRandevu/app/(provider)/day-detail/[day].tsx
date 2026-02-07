import { useState } from 'react';
import { useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, Pressable, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { TimeRangeRow } from '@/components/time-range-row';
import { SlotChip } from '@/components/slot-chip';
import { SectionHeader } from '@/components/section-header';
import { EmptyState } from '@/components/empty-state';
import { Card } from '@/components/ui/card';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';
import type { DayOfWeek, TimeRange } from '@/store/types';

const cycleTime = (current: string, forward: boolean): string => {
  const [h, m] = current.split(':').map(Number);
  let minutes = h * 60 + m + (forward ? 30 : -30);
  if (minutes < 0) minutes = 23 * 60 + 30;
  if (minutes >= 24 * 60) minutes = 0;
  const newH = Math.floor(minutes / 60) % 24;
  const newM = minutes % 60;
  return `${String(newH).padStart(2, '0')}:${String(newM).padStart(2, '0')}`;
};

const generateId = () => `r${Date.now()}-${Math.random().toString(36).slice(2, 6)}`;

const SLOT_TIMES = Array.from({ length: 48 }, (_, i) => {
  const h = Math.floor(i / 2);
  const m = i % 2 === 0 ? '00' : '30';
  return `${String(h).padStart(2, '0')}:${m}`;
});

const isTimeInRanges = (time: string, ranges: TimeRange[]): boolean => {
  return ranges.some((r) => time >= r.startTime && time < r.endTime);
};

export default function DayDetail() {
  const { day } = useLocalSearchParams<{ day: string }>();
  const { t } = useTranslation();
  const router = useRouter();

  const schedule = useStore((s) => s.schedule);
  const addTimeRange = useStore((s) => s.addTimeRange);
  const removeTimeRange = useStore((s) => s.removeTimeRange);
  const updateTimeRange = useStore((s) => s.updateTimeRange);
  const addSlot = useStore((s) => s.addSlot);
  const removeSlot = useStore((s) => s.removeSlot);

  const daySchedule = schedule.find((d) => d.day === day);
  const [showSlotPicker, setShowSlotPicker] = useState(false);

  if (!daySchedule) return null;

  const dayKey = day as DayOfWeek;

  const handleAddRange = () => {
    const lastRange = daySchedule.ranges[daySchedule.ranges.length - 1];
    const newStart = lastRange ? lastRange.endTime : '09:00';
    const newEndH = parseInt(newStart.split(':')[0], 10) + 3;
    const newEnd =
      newEndH >= 24 ? '23:30' : `${String(newEndH).padStart(2, '0')}:${newStart.split(':')[1]}`;

    addTimeRange(dayKey, { id: generateId(), startTime: newStart, endTime: newEnd });
  };

  const handleAddSlot = (time: string) => {
    if (!daySchedule.slots.includes(time)) {
      addSlot(dayKey, time);
    }
    setShowSlotPicker(false);
  };

  const availableSlots = SLOT_TIMES.filter((time) => !daySchedule.slots.includes(time));

  return (
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <View style={styles.navRow}>
            <Pressable onPress={() => router.back()} style={styles.backBtn}>
              <Text style={styles.backText}>{t('common.back')}</Text>
            </Pressable>
            <View style={styles.flex1} />
          </View>
          <Text style={styles.headerTitle}>{t(`schedule.${day}`)}</Text>
        </SafeAreaView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
        {/* Working Hours Section */}
        <View style={styles.section}>
          <SectionHeader
            title={t('schedule.ranges')}
            actionLabel={t('schedule.addRange')}
            onAction={handleAddRange}
          />

          {daySchedule.ranges.length === 0 ? (
            <EmptyState message={t('schedule.noRanges')} />
          ) : (
            <View style={styles.rangeList}>
              {daySchedule.ranges.map((range) => (
                <TimeRangeRow
                  key={range.id}
                  range={range}
                  onStartPress={() =>
                    updateTimeRange(dayKey, range.id, {
                      startTime: cycleTime(range.startTime, true),
                    })
                  }
                  onEndPress={() =>
                    updateTimeRange(dayKey, range.id, { endTime: cycleTime(range.endTime, true) })
                  }
                  onRemove={() => removeTimeRange(dayKey, range.id)}
                />
              ))}
            </View>
          )}
        </View>

        {/* Appointment Slots Section */}
        <View style={styles.section}>
          <SectionHeader
            title={t('schedule.slots')}
            actionLabel={t('schedule.addSlot')}
            onAction={() => setShowSlotPicker(!showSlotPicker)}
          />

          {showSlotPicker && (
            <Card shadow="sm" style={styles.pickerCard}>
              <ScrollView style={styles.pickerScroll} contentContainerStyle={styles.pickerGrid}>
                {availableSlots.map((time) => {
                  const outside = !isTimeInRanges(time, daySchedule.ranges);
                  return (
                    <Pressable
                      key={time}
                      onPress={() => handleAddSlot(time)}
                      style={[
                        styles.pickerChip,
                        outside ? styles.pickerChipOutside : styles.pickerChipNormal,
                      ]}>
                      <Text
                        style={[
                          styles.pickerChipText,
                          outside ? styles.pickerChipTextOutside : styles.pickerChipTextNormal,
                        ]}>
                        {time}
                      </Text>
                    </Pressable>
                  );
                })}
              </ScrollView>
            </Card>
          )}

          {daySchedule.slots.length === 0 ? (
            <EmptyState message={t('schedule.noSlots')} />
          ) : (
            <View style={styles.slotGrid}>
              {daySchedule.slots.map((time) => (
                <SlotChip
                  key={time}
                  time={time}
                  isOutsideRange={!isTimeInRanges(time, daySchedule.ranges)}
                  onRemove={() => removeSlot(dayKey, time)}
                />
              ))}
            </View>
          )}
        </View>
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  header: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
  navRow: { flexDirection: 'row', alignItems: 'center', marginTop: 16, gap: 12 },
  backBtn: { paddingVertical: 4 },
  backText: { color: '#fff', fontSize: 16 },
  headerTitle: { color: '#fff', fontSize: 24, fontWeight: '700', marginTop: 8 },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 16, gap: 24, paddingBottom: 32 },
  section: { gap: 12 },
  rangeList: { gap: 8 },
  pickerCard: { padding: 16, gap: 8 },
  pickerScroll: { maxHeight: 192 },
  pickerGrid: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
  pickerChip: {
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 8,
    borderCurve: 'continuous',
  },
  pickerChipNormal: { backgroundColor: 'rgba(26,35,126,0.1)' },
  pickerChipOutside: { backgroundColor: '#fef2f2' },
  pickerChipText: { fontSize: 14, fontWeight: '500', fontVariant: ['tabular-nums'] },
  pickerChipTextNormal: { color: colors.navy },
  pickerChipTextOutside: { color: '#f87171' },
  slotGrid: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
});
