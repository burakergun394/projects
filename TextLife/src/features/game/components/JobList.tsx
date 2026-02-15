import React, { useCallback, useMemo } from 'react';
import { View, Text, ScrollView, Pressable } from 'react-native';

import { Card, Button, Badge } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { JOBS } from '../data/jobs';

import type { Job } from '../types';

export const JobList = () => {
  const character = useCharacter();
  const getJob = useGameStore((s) => s.getJob);
  const quitJob = useGameStore((s) => s.quitJob);

  const availableJobs = useMemo(() => {
    if (!character) return [];
    return JOBS.filter((j) => character.smarts >= j.req);
  }, [character?.smarts]);

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

  return (
    <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
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
      ) : (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Şu anda çalışmıyorsun.
          </Text>
        </Card>
      )}

      {/* Açık Pozisyonlar */}
      <Text className="text-base font-outfit-bold text-text-primary">
        Açık Pozisyonlar
      </Text>
      {availableJobs.map((job) => (
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
              variant={character.job ? 'disabled' : 'secondary'}
            />
          </View>
        </Card>
      ))}
    </ScrollView>
  );
};
