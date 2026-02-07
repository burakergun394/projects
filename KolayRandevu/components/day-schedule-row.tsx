import { Switch } from 'react-native';
import { View, Text } from '@/src/tw';
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
    <Card shadow="xs" onPress={onPress} className="p-4 gap-2">
      <View className="flex-row items-center gap-3">
        <View className="flex-1">
          <Text
            className={`font-semibold text-base ${schedule.isActive ? 'text-gray-900' : 'text-gray-400'}`}>
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
        <View className="gap-1">
          {rangesSummary ? (
            <Text className="text-gray-500 text-sm">{rangesSummary}</Text>
          ) : (
            <Text className="text-gray-400 text-sm">{t('schedule.noRanges')}</Text>
          )}
          <Text className="text-orange font-medium text-sm">
            {schedule.slots.length > 0
              ? t('schedule.slotCount', { count: schedule.slots.length })
              : t('schedule.noSlots')}
          </Text>
        </View>
      ) : (
        <Text className="text-gray-400 text-sm">{t('schedule.dayOff')}</Text>
      )}
    </Card>
  );
};
