import { Switch } from 'react-native';
import { View, Text, Pressable } from '@/src/tw';
import { useTranslation } from 'react-i18next';
import type { DaySchedule } from '@/store/types';

interface DayScheduleRowProps {
  schedule: DaySchedule;
  onToggle: (isActive: boolean) => void;
  onStartTimePress: () => void;
  onEndTimePress: () => void;
}

export const DayScheduleRow = ({
  schedule,
  onToggle,
  onStartTimePress,
  onEndTimePress,
}: DayScheduleRowProps) => {
  const { t } = useTranslation();

  return (
    <View
      className="bg-white rounded-2xl p-4 flex-row items-center gap-3"
      style={{ borderCurve: 'continuous', boxShadow: '0 1px 2px rgba(0,0,0,0.06)' }}
    >
      <View className="w-24">
        <Text className={`font-semibold text-sm ${schedule.isActive ? 'text-gray-900' : 'text-gray-400'}`}>
          {t(`schedule.${schedule.day}`)}
        </Text>
      </View>
      <Switch
        value={schedule.isActive}
        onValueChange={onToggle}
        trackColor={{ false: '#d1d5db', true: '#FF9800' }}
        thumbColor="white"
      />
      {schedule.isActive ? (
        <View className="flex-1 flex-row items-center gap-2">
          <Pressable onPress={onStartTimePress} className="bg-gray-100 rounded-lg px-3 py-2">
            <Text className="text-gray-900 font-medium text-sm">{schedule.startTime}</Text>
          </Pressable>
          <Text className="text-gray-400">–</Text>
          <Pressable onPress={onEndTimePress} className="bg-gray-100 rounded-lg px-3 py-2">
            <Text className="text-gray-900 font-medium text-sm">{schedule.endTime}</Text>
          </Pressable>
        </View>
      ) : (
        <View className="flex-1">
          <Text className="text-gray-400 text-sm">{t('schedule.dayOff')}</Text>
        </View>
      )}
    </View>
  );
};
