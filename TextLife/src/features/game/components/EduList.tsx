import React, { useCallback, useMemo } from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Card, Button, Badge } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { EDUCATION_LIST } from '../data/education';

import type { Education } from '../types';

export const EduList = () => {
  const character = useCharacter();
  const startEdu = useGameStore((s) => s.startEdu);

  const handleStartEdu = useCallback(
    (edu: Education) => {
      startEdu(edu);
    },
    [startEdu],
  );

  const { completed, available } = useMemo(() => {
    if (!character) return { completed: [], available: [] };

    const done = EDUCATION_LIST.filter((e) =>
      character.education.includes(e.name),
    );
    const avail = EDUCATION_LIST.filter(
      (e) =>
        !character.education.includes(e.name) &&
        character.smarts >= e.smartsReq &&
        character.age >= e.minAge,
    );
    return { completed: done, available: avail };
  }, [character?.education, character?.smarts, character?.age]);

  if (!character) return null;

  return (
    <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
      {/* Devam Eden Eğitim */}
      {character.currentEdu && (
        <Card>
          <View className="flex-row items-center justify-between mb-sm">
            <Text className="text-base font-outfit-bold text-text-primary">
              Devam Eden Eğitim
            </Text>
            <Badge label="Okuyor" variant="info" />
          </View>
          <Text className="text-lg font-outfit-semibold text-text-primary">
            {character.currentEdu.name}
          </Text>
          <Text className="text-sm font-mono text-text-secondary mt-1">
            {character.eduYearsLeft} yıl kaldı
          </Text>
        </Card>
      )}

      {/* Tamamlanan Eğitimler */}
      {completed.length > 0 && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            Tamamlanan
          </Text>
          {completed.map((edu) => (
            <Card key={edu.name}>
              <View className="flex-row items-center gap-sm">
                <Text className="text-base">🎓</Text>
                <Text className="text-sm font-outfit-semibold text-text-primary">
                  {edu.name}
                </Text>
                <Badge label="Tamamlandı" variant="success" />
              </View>
            </Card>
          ))}
        </>
      )}

      {/* Mevcut Eğitimler */}
      <Text className="text-base font-outfit-bold text-text-primary">
        Mevcut Eğitimler
      </Text>
      {available.length === 0 && !character.currentEdu ? (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Şu anda başvurabileceğin bir eğitim yok.
          </Text>
        </Card>
      ) : (
        available.map((edu) => (
          <Card key={edu.name}>
            <View className="flex-row items-center justify-between">
              <View className="flex-1">
                <Text className="text-base font-outfit-semibold text-text-primary">
                  {edu.name}
                </Text>
                <Text className="text-xs font-outfit text-text-secondary">
                  {edu.years} yıl •{' '}
                  {edu.cost > 0 ? formatMoney(edu.cost) : 'Ücretsiz'} • Zeka:{' '}
                  {edu.smartsReq}+
                </Text>
                <Text className="text-xs font-outfit text-success mt-1">
                  +{edu.smartsGain} zeka puanı
                </Text>
              </View>
              <Button
                label="Başla"
                onPress={() => handleStartEdu(edu)}
                variant={character.currentEdu ? 'disabled' : 'secondary'}
              />
            </View>
          </Card>
        ))
      )}
    </ScrollView>
  );
};
