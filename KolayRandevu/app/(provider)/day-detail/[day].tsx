import { useState } from 'react';
import { Alert } from 'react-native';
import { useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView, Pressable } from '@/src/tw';
import { TimeRangeRow } from '@/components/time-range-row';
import { SlotChip } from '@/components/slot-chip';
import { SectionHeader } from '@/components/section-header';
import { EmptyState } from '@/components/empty-state';
import { Button } from '@/components/Button';
import { useStore } from '@/store/store';
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
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <View className="flex-row items-center mt-4 gap-3">
            <Pressable onPress={() => router.back()} className="py-1">
              <Text className="text-white text-base">{t('common.back')}</Text>
            </Pressable>
            <View className="flex-1" />
          </View>
          <Text className="text-white text-2xl font-bold mt-2">{t(`schedule.${day}`)}</Text>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-6 pb-8">
        {/* Working Hours Section */}
        <View className="gap-3">
          <SectionHeader
            title={t('schedule.ranges')}
            actionLabel={t('schedule.addRange')}
            onAction={handleAddRange}
          />

          {daySchedule.ranges.length === 0 ? (
            <EmptyState message={t('schedule.noRanges')} />
          ) : (
            <View className="gap-2">
              {daySchedule.ranges.map((range) => (
                <TimeRangeRow
                  key={range.id}
                  range={range}
                  onStartPress={() =>
                    updateTimeRange(dayKey, range.id, { startTime: cycleTime(range.startTime, true) })
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
        <View className="gap-3">
          <SectionHeader
            title={t('schedule.slots')}
            actionLabel={t('schedule.addSlot')}
            onAction={() => setShowSlotPicker(!showSlotPicker)}
          />

          {showSlotPicker && (
            <View
              className="bg-white rounded-2xl p-4 gap-2"
              style={{ borderCurve: 'continuous', boxShadow: '0 1px 3px rgba(0,0,0,0.08)' }}
            >
              <ScrollView className="max-h-48" contentContainerClassName="flex-row flex-wrap gap-2">
                {availableSlots.map((time) => {
                  const outside = !isTimeInRanges(time, daySchedule.ranges);
                  return (
                    <Pressable
                      key={time}
                      onPress={() => handleAddSlot(time)}
                      className={`rounded-lg px-3 py-2 ${outside ? 'bg-red-50' : 'bg-navy/10'}`}
                      style={{ borderCurve: 'continuous' }}
                    >
                      <Text
                        className={`text-sm font-medium ${outside ? 'text-red-400' : 'text-navy'}`}
                        style={{ fontVariant: ['tabular-nums'] }}
                      >
                        {time}
                      </Text>
                    </Pressable>
                  );
                })}
              </ScrollView>
            </View>
          )}

          {daySchedule.slots.length === 0 ? (
            <EmptyState message={t('schedule.noSlots')} />
          ) : (
            <View className="flex-row flex-wrap gap-2">
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
