import React, { useCallback, useMemo } from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Card, Button, Badge } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { EDUCATION_LIST, hasCompletedEdu } from '../data/education';

import type { Education } from '../types';

export const EduList = () => {
  const character = useCharacter();
  const startEdu = useGameStore((s) => s.startEdu);
  const dropOut = useGameStore((s) => s.dropOut);

  const handleStartEdu = useCallback(
    (edu: Education) => {
      startEdu(edu);
    },
    [startEdu],
  );

  const handleDropOut = useCallback(() => {
    dropOut();
  }, [dropOut]);

  const { completed, available } = useMemo(() => {
    if (!character) return { completed: [], available: [] };

    const done = EDUCATION_LIST.filter((e) =>
      character.education.includes(e.name),
    );

    // Sadece yükseköğretim (auto=false) ve uygun koşullar
    const avail = EDUCATION_LIST.filter((e) => {
      if (e.auto) return false;
      if (character.education.includes(e.name)) return false;
      if (character.smarts < e.smartsReq) return false;
      if (character.age < e.minAge) return false;
      if (character.age > e.maxAge) return false;
      if (e.prereq && !hasCompletedEdu(character.education, e.prereq)) return false;
      // Çift üniversite engeli
      if (e.name.startsWith('Üniversite') && character.education.some((ed) => ed.startsWith('Üniversite'))) return false;
      return true;
    });

    return { completed: done, available: avail };
  }, [character?.education, character?.smarts, character?.age]);

  if (!character) return null;

  // Dropout izin kontrolü
  const canDropOut = (() => {
    if (!character.currentEdu) return false;
    if (character.currentEdu.dropCanAge === null) return false;
    if (character.currentEdu.auto && character.age < character.currentEdu.dropCanAge) return false;
    return true;
  })();

  // Sınav başarı tahmini
  const calcPassChance = (edu: Education) => {
    if (!edu.examRequired) return 100;
    const smartsBonus = Math.max(0, (character.smarts - edu.smartsReq) * 0.8);
    return Math.min(95, Math.round(edu.examPassRate + smartsBonus));
  };

  // Yükseköğretim öğrencisi mi
  const isHigherEduStudent = character.currentEdu && !character.currentEdu.auto;

  // Neden boş olduğunu göster
  const getEmptyMessage = () => {
    if (character.age < 18) return 'Zorunlu eğitim otomatik olarak başlayacak.';
    if (!character.education.includes('Lise')) return 'Üniversite için önce lise diploması gerekli.';
    if (completed.length === EDUCATION_LIST.length) return 'Tüm eğitim kademelerini tamamladın! 🎓';
    return 'Mevcut koşullarına uygun eğitim seçeneği yok.';
  };

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
          {canDropOut ? (
            <View className="mt-md">
              <Button label="Okulu Bırak" onPress={handleDropOut} variant="danger" />
            </View>
          ) : (
            <Text className="text-xs font-outfit text-text-tertiary mt-sm">
              📋 Zorunlu eğitim — bırakılamaz
              {character.currentEdu.dropCanAge !== null && character.currentEdu.auto
                ? ` (${character.currentEdu.dropCanAge} yaşından sonra bırakılabilir)`
                : ''}
            </Text>
          )}
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
                <Text className="text-base">✅</Text>
                <Text className="text-sm font-outfit-semibold text-text-primary">
                  {edu.name}
                </Text>
                <Badge label="Tamamlandı" variant="success" />
              </View>
            </Card>
          ))}
        </>
      )}

      {/* Mevcut Eğitim Seçenekleri */}
      <Text className="text-base font-outfit-bold text-text-primary">
        Eğitim Seçenekleri
      </Text>

      {/* Yükseköğretim öğrencisi uyarısı */}
      {isHigherEduStudent && (
        <Card>
          <Text className="text-sm font-outfit text-warning text-center">
            📚 {character.currentEdu!.name} öğrencisisin — yeni eğitim başlatamazsın.
          </Text>
        </Card>
      )}

      {available.length === 0 && !character.currentEdu ? (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            {getEmptyMessage()}
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
                {edu.prereq && (
                  <Text className="text-xs font-outfit text-text-tertiary mt-1">
                    Önkoşul: {edu.prereq} diploması
                  </Text>
                )}
                {edu.examRequired && (
                  <Text className="text-xs font-outfit text-brand-primary mt-1">
                    📝 Giriş sınavı var (başarı ~%{calcPassChance(edu)})
                  </Text>
                )}
              </View>
              <Button
                label={edu.examRequired ? 'Sınava Gir' : 'Başla'}
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
