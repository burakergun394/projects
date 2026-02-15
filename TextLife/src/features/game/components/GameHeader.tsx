import React from 'react';
import { View, Text } from 'react-native';

import { Badge } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';
import { colors } from '@/shared/theme';

import { useCharacter } from '../stores/gameStore';

export const GameHeader = () => {
  const character = useCharacter();
  if (!character) return null;

  const genderEmoji = character.gender === 'M' ? '👦' : '👧';
  const moneyVariant =
    character.money >= 0 ? ('success' as const) : ('danger' as const);

  const currentJob = character.career.currentJob;

  return (
    <View className="bg-bg-elevated px-lg py-md border-b border-border">
      <View className="flex-row items-center justify-between">
        <View className="flex-row items-center gap-sm flex-1">
          <Text className="text-xl">{genderEmoji}</Text>
          <View className="flex-1">
            <Text
              className="text-lg font-outfit-bold text-text-primary"
              numberOfLines={1}
            >
              {character.name} {character.surname}
            </Text>
            <View className="flex-row items-center gap-xs mt-1">
              <Text className="text-xs font-mono text-text-secondary">
                {character.age} yaş
              </Text>
              <Text className="text-xs text-text-tertiary">•</Text>
              <Text className="text-xs font-outfit text-text-secondary">
                {character.city}
              </Text>
              <Text className="text-xs text-text-tertiary">•</Text>
              <Text className="text-xs">
                {character.zodiac.emoji} {character.zodiac.name}
              </Text>
            </View>
          </View>
        </View>
        <View className="items-end gap-xs">
          <Badge
            label={formatMoney(character.money)}
            variant={moneyVariant}
          />
          {character.currentEdu && (
            <Text
              className="text-xs font-outfit text-brand-primary"
              numberOfLines={1}
            >
              📚 {character.universityDepartment
                ? `Üniversite — ${character.universityDepartment.name} (${character.eduYearsLeft}y)`
                : `${character.currentEdu.name} (${character.eduYearsLeft}y)`}
            </Text>
          )}
          {currentJob && (
            <Text
              className="text-xs font-outfit text-text-secondary"
              numberOfLines={1}
            >
              {currentJob.title}
            </Text>
          )}
          {character.career.isRetired && (
            <Text
              className="text-xs font-outfit text-success"
              numberOfLines={1}
            >
              🏖️ Emekli
            </Text>
          )}
        </View>
      </View>
    </View>
  );
};
