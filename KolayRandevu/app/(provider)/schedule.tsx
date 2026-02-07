import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useRouter } from 'expo-router';
import { View, Text, ScrollView } from '@/src/tw';
import { DayScheduleRow } from '@/components/day-schedule-row';
import { useStore } from '@/store/store';
import type { DayOfWeek } from '@/store/types';

export default function Schedule() {
  const { t } = useTranslation();
  const router = useRouter();
  const schedule = useStore((s) => s.schedule);
  const toggleDay = useStore((s) => s.toggleDay);

  const handleDayPress = (day: DayOfWeek) => {
    router.push(`/(provider)/day-detail/${day}` as never);
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
        {schedule.map((day) => (
          <DayScheduleRow
            key={day.day}
            schedule={day}
            onToggle={(isActive) => toggleDay(day.day, isActive)}
            onPress={() => handleDayPress(day.day)}
          />
        ))}
      </ScrollView>
    </View>
  );
}
