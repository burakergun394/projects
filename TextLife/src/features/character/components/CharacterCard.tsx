import React from 'react';
import { View, Text } from 'react-native';

import { Card, ProgressBar } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';
import { colors } from '@/shared/theme';

import { getScoreDescription } from '@/features/game/data/departments';
import { EDUCATION_LIST } from '@/features/game/data/education';

import type { Character } from '@/features/game/types';

interface CharacterCardProps {
  character: Character;
}

export const CharacterCard = ({ character }: CharacterCardProps) => {
  const genderEmoji = character.gender === 'M' ? '👦' : '👧';
  const aliveRelations = character.relationships.filter((r) => r.isAlive);
  const spouseCount = character.relationships.filter((r) => r.type === 'spouse').length;
  const { career } = character;

  // En yüksek pozisyon
  const highestPosition =
    career.jobHistory.length > 0
      ? career.jobHistory.reduce((best, h) => (h.finalSalary > best.finalSalary ? h : best)).jobTitle
      : career.currentJob?.title ?? null;

  // Hayat Puanı — basit bir skor hesabı
  const lifeScore = Math.round(
    (character.health + character.happiness + character.smarts + character.looks) / 4 +
      character.education.length * 10 +
      character.achievements.length * 5 +
      Math.min(character.money / 10000, 50) +
      character.childCount * 3 +
      (character.isMarried ? 10 : 0) +
      career.totalWorkYears * 1 +
      (career.isRetired ? 10 : 0),
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
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Eğitim
          </Text>
          <Text className="text-sm font-outfit text-text-primary">
            {character.education.length > 0
              ? character.education[character.education.length - 1]
              : 'Eğitimsiz'}
          </Text>
        </View>
        {character.universityDepartment && (
          <>
            <View className="flex-row justify-between">
              <Text className="text-sm font-outfit text-text-secondary">
                Bölüm
              </Text>
              <Text className="text-sm font-outfit text-text-primary">
                {character.universityDepartment.name} ({character.universityDepartment.type === 'devlet' ? 'Devlet' : 'Özel'})
              </Text>
            </View>
            {character.lastExamScore !== null && (
              <View className="flex-row justify-between">
                <Text className="text-sm font-outfit text-text-secondary">
                  YKS Puanı
                </Text>
                <Text className="text-sm font-mono text-text-primary">
                  {character.lastExamScore}/500 ({getScoreDescription(character.lastExamScore).percentile})
                </Text>
              </View>
            )}
            <View className="flex-row justify-between">
              <Text className="text-sm font-outfit text-text-secondary">
                Prestij
              </Text>
              <Text className="text-sm font-mono text-text-primary">
                {'★'.repeat(character.universityDepartment.prestige)}{'☆'.repeat(10 - character.universityDepartment.prestige)}
              </Text>
            </View>
            {character.education.includes('Yüksek Lisans') && (
              <View className="flex-row justify-between">
                <Text className="text-sm font-outfit text-text-secondary">
                  Ek Eğitim
                </Text>
                <Text className="text-sm font-outfit text-text-primary">
                  Yüksek Lisans{character.education.includes('Doktora') ? ' + Doktora' : ''}
                </Text>
              </View>
            )}
          </>
        )}
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Toplam Eğitim
          </Text>
          <Text className="text-sm font-mono text-text-primary">
            {character.education.reduce((total, eduName) => {
              const edu = EDUCATION_LIST.find((e) => e.name === eduName);
              if (edu) return total + edu.years;
              // Üniversite — bölüm adı formatı
              if (eduName.startsWith('Üniversite') && character.universityDepartment) return total + character.universityDepartment.totalYears;
              return total;
            }, 0)} yıl
          </Text>
        </View>
      </View>

      {/* Kariyer Özeti */}
      <View className="border-t border-border pt-md mt-md gap-xs">
        <Text className="text-sm font-outfit-semibold text-text-primary mb-xs">
          Kariyer
        </Text>
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Toplam Çalışma
          </Text>
          <Text className="text-sm font-mono text-text-primary">
            {career.totalWorkYears} yıl
          </Text>
        </View>
        {highestPosition && (
          <View className="flex-row justify-between">
            <Text className="text-sm font-outfit text-text-secondary">
              En Yüksek Pozisyon
            </Text>
            <Text className="text-sm font-outfit text-text-primary">
              {highestPosition}
            </Text>
          </View>
        )}
        <View className="flex-row justify-between">
          <Text className="text-sm font-outfit text-text-secondary">
            Toplam Kazanç
          </Text>
          <Text className="text-sm font-mono text-text-primary">
            {formatMoney(career.lifetimeEarnings)}
          </Text>
        </View>
        {career.isRetired && career.pension > 0 && (
          <View className="flex-row justify-between">
            <Text className="text-sm font-outfit text-text-secondary">
              Emekli Maaşı
            </Text>
            <Text className="text-sm font-mono text-success">
              ₺{career.pension.toLocaleString('tr-TR')}/ay
            </Text>
          </View>
        )}
        {career.jobHistory.length > 0 && (
          <View className="mt-xs">
            <Text className="text-xs font-outfit text-text-tertiary mb-1">
              İş Geçmişi:
            </Text>
            {career.jobHistory.map((h, i) => (
              <Text key={`${h.jobTitle}-${h.startAge}-${i}`} className="text-xs font-outfit text-text-tertiary">
                {h.jobTitle} ({h.startAge}–{h.endAge} yaş) → {
                  h.endReason === 'promoted' ? 'Terfi' :
                  h.endReason === 'fired' ? 'Çıkarıldı' :
                  h.endReason === 'retired' ? 'Emekli' : 'İstifa'
                }
              </Text>
            ))}
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
