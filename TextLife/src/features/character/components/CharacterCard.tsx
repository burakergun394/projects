import React from 'react';
import { View, Text } from 'react-native';

import { Card, ProgressBar } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';
import { colors } from '@/shared/theme';

import type { Character } from '@/features/game/types';

interface CharacterCardProps {
  character: Character;
}

export const CharacterCard = ({ character }: CharacterCardProps) => {
  const genderEmoji = character.gender === 'M' ? '👦' : '👧';

  return (
    <Card className="mx-lg">
      <View className="items-center mb-lg">
        <Text className="text-3xl mb-sm">{genderEmoji}</Text>
        <Text className="text-xl font-outfit-bold text-text-primary">
          {character.name} {character.surname}
        </Text>
        <Text className="text-sm font-outfit text-text-secondary">
          {character.city} • {character.zodiac.emoji} {character.zodiac.name}
        </Text>
      </View>

      <View className="gap-sm mb-lg">
        <StatSummary
          label="Sağlık"
          emoji="❤️"
          value={character.health}
          color={colors.statHealth}
        />
        <StatSummary
          label="Mutluluk"
          emoji="😊"
          value={character.happiness}
          color={colors.statHappiness}
        />
        <StatSummary
          label="Zeka"
          emoji="🧠"
          value={character.smarts}
          color={colors.statSmarts}
        />
        <StatSummary
          label="Görünüş"
          emoji="✨"
          value={character.looks}
          color={colors.statLooks}
        />
      </View>

      <View className="border-t border-border pt-md gap-xs">
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Toplam Para
          </Text>
          <Text className="text-sm font-mono-medium text-text-primary">
            {formatMoney(character.money)}
          </Text>
        </View>
        {character.job && (
          <View className="flex-row justify-between">
            <Text className="text-sm font-outfit text-text-secondary">
              Son İş
            </Text>
            <Text className="text-sm font-outfit text-text-primary">
              {character.job.title}
            </Text>
          </View>
        )}
        {character.education.length > 0 && (
          <View className="flex-row justify-between">
            <Text className="text-sm font-outfit text-text-secondary">
              Eğitim
            </Text>
            <Text className="text-sm font-outfit text-text-primary">
              {character.education[character.education.length - 1]}
            </Text>
          </View>
        )}
      </View>
    </Card>
  );
};

const StatSummary = ({
  label,
  emoji,
  value,
  color,
}: {
  label: string;
  emoji: string;
  value: number;
  color: string;
}) => (
  <View className="flex-row items-center gap-sm">
    <Text className="text-xs">{emoji}</Text>
    <Text className="text-xs font-outfit text-text-secondary w-16">
      {label}
    </Text>
    <View className="flex-1">
      <ProgressBar value={value} color={color} />
    </View>
    <Text className="text-xs font-mono text-text-secondary w-8 text-right">
      {value}
    </Text>
  </View>
);
