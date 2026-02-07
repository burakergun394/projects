import { useTranslation } from 'react-i18next';
import { View, Text, ScrollView, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useRouter } from 'expo-router';
import { DayScheduleRow } from '@/components/day-schedule-row';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';
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
    <View style={styles.screen}>
      <View style={styles.header}>
        <SafeAreaView edges={['top']}>
          <Text style={styles.headerTitle}>{t('schedule.title')}</Text>
          <Text style={styles.headerSub}>{t('schedule.workingHours')}</Text>
        </SafeAreaView>
      </View>

      <ScrollView style={styles.flex1} contentContainerStyle={styles.scrollContent}>
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

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#f9fafb' },
  header: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
  headerTitle: { color: '#fff', fontSize: 24, fontWeight: '700', marginTop: 16 },
  headerSub: { color: 'rgba(255,255,255,0.7)', fontSize: 14, marginTop: 4 },
  flex1: { flex: 1 },
  scrollContent: { paddingHorizontal: 24, paddingVertical: 16, gap: 12, paddingBottom: 32 },
});
