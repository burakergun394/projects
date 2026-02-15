import React, { useCallback, useMemo } from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Card, Button, Badge } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { JOBS, JOB_CATEGORY_LABELS } from '../data/jobs';

import type { Job } from '../types';

type JobCategory = Job['category'];

const CATEGORY_ORDER: readonly JobCategory[] = [
  'entry',
  'skilled',
  'professional',
  'executive',
] as const;

export const JobList = () => {
  const character = useCharacter();
  const getJob = useGameStore((s) => s.getJob);
  const quitJob = useGameStore((s) => s.quitJob);

  const hasLise = character?.education.includes('Lise') ?? false;
  const hasUni = character?.education.some((e) => e.startsWith('Üniversite')) ?? false;
  const isHigherEduStudent = character?.currentEdu && !character.currentEdu.auto;

  const groupedJobs = useMemo(() => {
    if (!character) return {};
    const available = JOBS.filter((j) => {
      if (character.smarts < j.req) return false;
      // Diploma gereksinimleri
      if (j.req >= 40 && !hasUni) return false;
      if (j.req >= 20 && !hasLise) return false;
      return true;
    });
    const grouped: Partial<Record<JobCategory, Job[]>> = {};
    for (const job of available) {
      if (!grouped[job.category]) {
        grouped[job.category] = [];
      }
      grouped[job.category]!.push(job);
    }
    return grouped;
  }, [character?.smarts, hasLise, hasUni]);

  const handleGetJob = useCallback(
    (job: Job) => {
      getJob(job);
    },
    [getJob],
  );

  const handleQuitJob = useCallback(() => {
    quitJob();
  }, [quitJob]);

  if (!character) return null;

  if (character.age < 18) {
    return (
      <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            İş başvurusu yapabilmek için 18 yaşını doldurmalısın.
          </Text>
        </Card>
      </ScrollView>
    );
  }

  const hasAnyJobs = CATEGORY_ORDER.some((c) => (groupedJobs[c]?.length ?? 0) > 0);

  return (
    <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
      {/* Öğrenci uyarısı */}
      {isHigherEduStudent && (
        <Card>
          <Text className="text-sm font-outfit text-warning text-center">
            📚 {character.currentEdu!.name} öğrencisisin — tam zamanlı çalışamazsın.
          </Text>
        </Card>
      )}

      {/* Mevcut İş */}
      {character.job ? (
        <Card>
          <View className="flex-row items-center justify-between mb-sm">
            <Text className="text-base font-outfit-bold text-text-primary">
              Mevcut İş
            </Text>
            <Badge label="Çalışıyor" variant="success" />
          </View>
          <Text className="text-lg font-outfit-semibold text-text-primary">
            {character.job.title}
          </Text>
          <Text className="text-sm font-mono text-text-secondary mt-1">
            Maaş: {formatMoney(character.job.salary)}/yıl
          </Text>
          <View className="mt-md">
            <Button label="İşten Ayrıl" onPress={handleQuitJob} variant="danger" />
          </View>
        </Card>
      ) : !isHigherEduStudent ? (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Şu anda çalışmıyorsun.
          </Text>
        </Card>
      ) : null}

      {/* Kategorilere Göre Açık Pozisyonlar */}
      {!hasAnyJobs && !character.job && (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Eğitim seviyene uygun açık pozisyon yok.
          </Text>
        </Card>
      )}

      {CATEGORY_ORDER.map((category) => {
        const jobs = groupedJobs[category];
        if (!jobs || jobs.length === 0) return null;
        return (
          <View key={category} className="gap-sm">
            <Text className="text-base font-outfit-bold text-text-primary">
              {JOB_CATEGORY_LABELS[category]}
            </Text>
            {jobs.map((job) => (
              <Card key={job.title}>
                <View className="flex-row items-center justify-between">
                  <View className="flex-1">
                    <Text className="text-base font-outfit-semibold text-text-primary">
                      {job.title}
                    </Text>
                    <Text className="text-sm font-mono text-text-secondary">
                      {formatMoney(job.salary)}/yıl
                    </Text>
                    {job.req > 0 && (
                      <Text className="text-xs font-outfit text-text-tertiary">
                        Zeka gereksinimi: {job.req}+
                      </Text>
                    )}
                  </View>
                  <Button
                    label="Başvur"
                    onPress={() => handleGetJob(job)}
                    variant={character.job || isHigherEduStudent ? 'disabled' : 'secondary'}
                  />
                </View>
              </Card>
            ))}
          </View>
        );
      })}
    </ScrollView>
  );
};
