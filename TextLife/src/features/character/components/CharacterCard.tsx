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
  const aliveRelations = character.relationships.filter((r) => r.isAlive);
  const spouseCount = character.relationships.filter((r) => r.type === 'spouse').length;

  // Hayat Puanı — basit bir skor hesabı
  const lifeScore = Math.round(
    (character.health + character.happiness + character.smarts + character.looks) / 4 +
      character.education.length * 10 +
      character.achievements.length * 5 +
      Math.min(character.money / 10000, 50) +
      character.childCount * 3 +
      (character.isMarried ? 10 : 0),
  );

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

      {/* Statlar */}
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

      {/* Hayat Özeti */}
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
        {character.jobHistory.length > 0 && (
          <View className="flex-row justify-between">
            <Text className="text-sm font-outfit text-text-secondary">
              Çalıştığı İşler
            </Text>
            <Text className="text-sm font-mono text-text-primary">
              {character.jobHistory.length}
            </Text>
          </View>
        )}
      </View>

      {/* İlişkiler Özeti */}
      <View className="border-t border-border pt-md mt-md gap-xs">
        <Text className="text-sm font-outfit-semibold text-text-primary mb-xs">
          İlişkiler
        </Text>
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Evlilik
          </Text>
          <Text className="text-sm font-outfit text-text-primary">
            {spouseCount > 0 ? `${spouseCount} kez` : 'Hiç evlenmedi'}
          </Text>
        </View>
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Çocuklar
          </Text>
          <Text className="text-sm font-mono text-text-primary">
            {character.childCount}
          </Text>
        </View>
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Hayatta Kalan Yakınlar
          </Text>
          <Text className="text-sm font-mono text-text-primary">
            {aliveRelations.length}
          </Text>
        </View>
      </View>

      {/* Başarılar */}
      {character.achievements.length > 0 && (
        <View className="border-t border-border pt-md mt-md">
          <Text className="text-sm font-outfit-semibold text-text-primary mb-sm">
            Başarılar ({character.achievements.length})
          </Text>
          <View className="flex-row flex-wrap gap-xs">
            {character.achievements.map((id) => (
              <View
                key={id}
                className="bg-brand-subtle px-sm py-1 rounded-sm"
              >
                <Text className="text-xs font-outfit-medium text-brand-primary">
                  {id}
                </Text>
              </View>
            ))}
          </View>
        </View>
      )}

      {/* Hayat Puanı */}
      <View className="border-t border-border pt-md mt-md items-center">
        <Text className="text-xs font-outfit text-text-tertiary">
          Hayat Puanı
        </Text>
        <Text className="text-3xl font-outfit-extrabold text-brand-primary">
          {lifeScore}
        </Text>
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
