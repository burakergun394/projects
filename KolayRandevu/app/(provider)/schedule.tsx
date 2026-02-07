import { useState } from 'react';
import { Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, ScrollView } from '@/src/tw';
import { DayScheduleRow } from '@/components/day-schedule-row';
import { Button } from '@/components/Button';
import { useStore } from '@/store/store';
import type { DaySchedule as DayScheduleType } from '@/store/types';

export default function Schedule() {
  const { t } = useTranslation();
  const storeSchedule = useStore((s) => s.schedule);
  const setSchedule = useStore((s) => s.setSchedule);
  const [localSchedule, setLocalSchedule] = useState<DayScheduleType[]>(storeSchedule);

  const updateDay = (index: number, updates: Partial<DayScheduleType>) => {
    setLocalSchedule((prev) =>
      prev.map((day, i) => (i === index ? { ...day, ...updates } : day))
    );
  };

  const cycleTime = (current: string, isStart: boolean): string => {
    const [h, m] = current.split(':').map(Number);
    let minutes = h * 60 + m + 30;
    if (isStart && minutes >= 23 * 60) minutes = 6 * 60;
    if (!isStart && minutes >= 23 * 60 + 30) minutes = 7 * 60;
    const newH = Math.floor(minutes / 60) % 24;
    const newM = minutes % 60;
    return `${String(newH).padStart(2, '0')}:${String(newM).padStart(2, '0')}`;
  };

  const handleSave = () => {
    setSchedule(localSchedule);
    Alert.alert(t('schedule.saved'));
  };

  return (
    <View className="flex-1 bg-gray-50">
      <View className="bg-navy px-6 pb-6 pt-2">
        <SafeAreaView edges={['top']}>
          <Text className="text-white text-2xl font-bold mt-4">{t('schedule.title')}</Text>
          <Text className="text-white/70 text-sm mt-1">{t('schedule.workingHours')}</Text>
        </SafeAreaView>
      </View>

      <ScrollView className="flex-1" contentContainerClassName="px-6 py-4 gap-3 pb-8">
        {localSchedule.map((day, index) => (
          <DayScheduleRow
            key={day.day}
            schedule={day}
            onToggle={(isActive) => updateDay(index, { isActive })}
            onStartTimePress={() =>
              updateDay(index, { startTime: cycleTime(day.startTime, true) })
            }
            onEndTimePress={() =>
              updateDay(index, { endTime: cycleTime(day.endTime, false) })
            }
          />
        ))}

        <View className="mt-4">
          <Button title={t('common.save')} onPress={handleSave} />
        </View>
      </ScrollView>
    </View>
  );
}
