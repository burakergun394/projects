import React, { memo } from 'react';
import { View, Text } from 'react-native';

import { ProgressBar } from '@/shared/components/ui';
import { colors } from '@/shared/theme';

import { useCharacter } from '../stores/gameStore';

interface StatRowProps {
  label: string;
  emoji: string;
  value: number;
  color: string;
}

const StatRow = memo(({ label, emoji, value, color }: StatRowProps) => (
  <View className="flex-row items-center gap-sm">
    <Text className="text-xs w-6 text-center">{emoji}</Text>
    <Text className="text-xs font-outfit w-16 text-text-secondary">
      {label}
    </Text>
    <View className="flex-1">
      <ProgressBar value={value} color={color} />
    </View>
    <Text className="text-xs font-mono-medium text-text-secondary w-8 text-right">
      {value}
    </Text>
  </View>
));

export const StatBars = memo(() => {
  const character = useCharacter();
  if (!character) return null;

  return (
    <View className="bg-bg-elevated px-lg py-md gap-sm border-t border-border">
      <StatRow
        label="Sağlık"
        emoji="❤️"
        value={character.health}
        color={colors.statHealth}
      />
      <StatRow
        label="Mutluluk"
        emoji="😊"
        value={character.happiness}
        color={colors.statHappiness}
      />
      <StatRow
        label="Zeka"
        emoji="🧠"
        value={character.smarts}
        color={colors.statSmarts}
      />
      <StatRow
        label="Görünüş"
        emoji="✨"
        value={character.looks}
        color={colors.statLooks}
      />
    </View>
  );
});
