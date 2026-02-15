import React, { memo } from 'react';
import { View, Text } from 'react-native';
import { MotiView } from 'moti';

import { colors } from '@/shared/theme';

import type { LogEntry, LogType } from '../types';

const DOT_COLORS: Record<LogType, string> = {
  birth: colors.brandPrimary,
  good: colors.success,
  bad: colors.danger,
  milestone: colors.warning,
  death: colors.danger,
  event: colors.textTertiary,
};

interface LogEntryItemProps {
  item: LogEntry;
  index: number;
}

export const LogEntryItem = memo(({ item, index }: LogEntryItemProps) => (
  <MotiView
    from={{ opacity: 0, translateY: 12 }}
    animate={{ opacity: 1, translateY: 0 }}
    transition={{ type: 'timing', duration: 200 }}
  >
    <View className="flex-row items-start px-lg py-sm gap-sm">
      <View className="flex-row items-center gap-xs mt-1">
        <Text className="text-xs font-mono text-text-tertiary w-8 text-right">
          {item.age}
        </Text>
        <View
          className="w-2 h-2 rounded-full"
          style={{ backgroundColor: DOT_COLORS[item.type] }}
        />
      </View>
      <Text className="text-sm font-outfit text-text-primary flex-1 leading-5">
        {item.text}
      </Text>
    </View>
  </MotiView>
));
