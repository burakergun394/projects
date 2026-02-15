import React, { useCallback, useMemo } from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Card, Button, Badge, ProgressBar } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';
import { colors } from '@/shared/theme';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { JOBS, SECTOR_LABELS, SECTOR_ORDER, meetsEducationReq, getJobById } from '../data/jobs';
import { GENERAL_UNI_JOBS } from '../data/departments';

import type { Job, CareerState } from '../types';

const EDUCATION_LABELS: Record<string, string> = {
  none: '—',
  ilkokul: 'İlkokul',
  ortaokul: 'Ortaokul',
  lise: 'Lise',
  universite: 'Üniversite',
  yuksek_lisans: 'Y. Lisans',
  doktora: 'Doktora',
};

const END_REASON_LABELS: Record<string, string> = {
  quit: 'İstifa',
  fired: 'İşten Çıkarıldı',
  promoted: 'Terfi',
  retired: 'Emeklilik',
};

const getPerformanceColor = (score: number): string => {
  if (score < 30) return colors.danger;
  if (score < 65) return colors.warning;
  return colors.success;
};

export const JobList = () => {
  const character = useCharacter();
  const applyForJob = useGameStore((s) => s.applyForJob);
  const quitJob = useGameStore((s) => s.quitJob);
  const retire = useGameStore((s) => s.retire);

  const isHigherEduStudent = character?.currentEdu && !character.currentEdu.auto;

  // İşe alınabilecek pozisyonları grupla (executive hariç)
  const groupedJobs = useMemo(() => {
    if (!character) return {};
    const grouped: Partial<Record<Job['sector'], { job: Job; locked: boolean; lockReason: string | null }[]>> = {};

    for (const job of JOBS) {
      // Executive pozisyonlar listelenmez
      if (job.sector === 'executive') continue;

      const eduOk = meetsEducationReq(character.education, job.minEducation);
      const smartsOk = character.smarts >= job.minSmarts;
      const ageOk = character.age >= job.minAge && character.age <= job.maxAge;

      let locked = false;
      let lockReason: string | null = null;

      if (!eduOk) {
        locked = true;
        lockReason = `${EDUCATION_LABELS[job.minEducation]} diploması gerekli`;
      } else if (!smartsOk) {
        locked = true;
        lockReason = `Zeka ${job.minSmarts}+ gerekli`;
      } else if (!ageOk) {
        locked = true;
        lockReason = character.age < job.minAge
          ? `${job.minAge} yaş gerekli`
          : 'Yaş sınırı aşıldı';
      }

      const sector = job.sector;
      if (!grouped[sector]) grouped[sector] = [];
      grouped[sector]!.push({ job, locked, lockReason });
    }

    return grouped;
  }, [character?.smarts, character?.age, character?.education]);

  const handleApply = useCallback(
    (jobId: string) => applyForJob(jobId),
    [applyForJob],
  );
  const handleQuit = useCallback(() => quitJob(), [quitJob]);
  const handleRetire = useCallback(() => retire(), [retire]);

  if (!character) return null;

  const { career } = character;

  // Emekli
  if (career.isRetired) {
    return (
      <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
        <Card>
          <View className="items-center gap-sm">
            <Text className="text-2xl">🏖️</Text>
            <Text className="text-base font-outfit-bold text-text-primary">
              Emeklisin — tekrar çalışamazsın
            </Text>
            {career.pension > 0 && (
              <Text className="text-sm font-mono text-success">
                Emekli maaşı: ₺{career.pension.toLocaleString('tr-TR')}/ay
              </Text>
            )}
          </View>
        </Card>

        {/* İş Geçmişi */}
        {career.jobHistory.length > 0 && (
          <JobHistorySection history={career.jobHistory} />
        )}
      </ScrollView>
    );
  }

  // 16 yaş altı
  if (character.age < 16) {
    return (
      <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            İş başvurusu yapabilmek için en az 16 yaşında olmalısın.
          </Text>
        </Card>
      </ScrollView>
    );
  }

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

      {/* Mevcut İş Kartı */}
      {career.currentJob ? (
        <CurrentJobCard
          career={career as CareerState & { currentJob: Job }}
          age={character.age}
          onQuit={handleQuit}
          onRetire={character.age >= 60 ? handleRetire : undefined}
        />
      ) : !isHigherEduStudent ? (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Şu anda çalışmıyorsun.
          </Text>
        </Card>
      ) : null}

      {/* İş Geçmişi */}
      {career.jobHistory.length > 0 && (
        <JobHistorySection history={career.jobHistory} />
      )}

      {/* Sektörlere Göre Açık Pozisyonlar */}
      {SECTOR_ORDER.filter((s) => s !== 'executive').map((sector) => {
        const jobs = groupedJobs[sector];
        if (!jobs || jobs.length === 0) return null;

        return (
          <View key={sector} className="gap-sm">
            <Text className="text-base font-outfit-bold text-text-primary">
              {SECTOR_LABELS[sector]}
            </Text>
            {jobs.map(({ job, locked, lockReason }) => (
              <Card key={job.id}>
                <View className="flex-row items-start justify-between">
                  <View className="flex-1 mr-sm">
                    <Text className={`text-base font-outfit-semibold ${locked ? 'text-text-disabled' : 'text-text-primary'}`}>
                      {job.title}
                    </Text>
                    <Text className={`text-sm font-mono ${locked ? 'text-text-disabled' : 'text-text-secondary'}`}>
                      ₺{job.baseSalary.toLocaleString('tr-TR')}/ay
                    </Text>
                    <View className="flex-row gap-sm mt-1">
                      <Text className="text-xs font-outfit text-text-tertiary">
                        Diploma: {EDUCATION_LABELS[job.minEducation]}
                      </Text>
                      {job.minSmarts > 0 && (
                        <Text className="text-xs font-outfit text-text-tertiary">
                          Zeka: {job.minSmarts}+
                        </Text>
                      )}
                    </View>
                    {locked && lockReason && (
                      <Text className="text-xs font-outfit text-danger mt-1">
                        🔒 {lockReason}
                      </Text>
                    )}
                    {!locked && career.currentJob && (
                      <Text className="text-xs font-outfit text-warning mt-1">
                        🔒 Önce mevcut işten ayrıl
                      </Text>
                    )}
                  </View>
                  <Button
                    label="Başvur"
                    onPress={() => handleApply(job.id)}
                    variant={locked || career.currentJob || isHigherEduStudent ? 'disabled' : 'secondary'}
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

// ── Mevcut İş Kartı ──────────────────────────────────────────────────
const CurrentJobCard = ({
  career,
  age,
  onQuit,
  onRetire,
}: {
  career: CareerState & { currentJob: Job };
  age: number;
  onQuit: () => void;
  onRetire?: () => void;
}) => {
  const job = career.currentJob;
  const nextJobId = job.promotionChain.length > 0 ? job.promotionChain[0] : null;
  const nextJob = nextJobId ? getJobById(nextJobId) : null;
  const yearsLeft = Math.max(0, job.experienceYearsForPromo - career.yearsInCurrentJob);
  const atTop = job.promotionChain.length === 0;

  return (
    <Card>
      <View className="flex-row items-center justify-between mb-sm">
        <Text className="text-sm font-outfit-bold text-text-secondary uppercase">
          Mevcut İş
        </Text>
        <Badge label="Çalışıyor" variant="success" />
      </View>

      <Text className="text-lg font-outfit-bold text-text-primary">
        {job.title}
      </Text>
      <Text className="text-xs font-outfit text-text-tertiary mt-1">
        Sektör: {SECTOR_LABELS[job.sector]}
      </Text>
      <Text className="text-sm font-mono text-text-secondary mt-1">
        Maaş: ₺{career.currentSalary.toLocaleString('tr-TR')}/ay
      </Text>
      <Text className="text-xs font-outfit text-text-tertiary mt-1">
        Kıdem: {career.yearsInCurrentJob} yıl
      </Text>

      {/* Performans çubuğu */}
      <View className="mt-md">
        <View className="flex-row items-center justify-between mb-1">
          <Text className="text-xs font-outfit text-text-secondary">
            Performans
          </Text>
          <Text className="text-xs font-mono text-text-secondary">
            {career.performanceScore}/100
          </Text>
        </View>
        <ProgressBar
          value={career.performanceScore}
          color={getPerformanceColor(career.performanceScore)}
        />
      </View>

      {/* Sonraki terfi bilgisi */}
      <View className="mt-md border-t border-border pt-md">
        {atTop ? (
          <Text className="text-xs font-outfit text-brand-primary text-center">
            🏆 Kariyerinin zirvesine ulaştın!
          </Text>
        ) : nextJob ? (
          <View>
            <Text className="text-xs font-outfit text-text-secondary">
              Sonraki terfi: <Text className="font-outfit-semibold text-text-primary">{nextJob.title}</Text>
            </Text>
            <Text className="text-xs font-outfit text-text-tertiary mt-1">
              Gerekli: {yearsLeft > 0 ? `${yearsLeft} yıl daha + ` : ''}performans 65+
              {nextJob.minSmarts > 0 ? ` + zeka ${nextJob.minSmarts}+` : ''}
            </Text>
          </View>
        ) : null}
      </View>

      {/* Butonlar */}
      <View className="flex-row gap-sm mt-md">
        <View className="flex-1">
          <Button label="İstifa Et" onPress={onQuit} variant="danger" />
        </View>
        {onRetire && (
          <View className="flex-1">
            <Button label="Emekli Ol" onPress={onRetire} variant="secondary" />
          </View>
        )}
      </View>
    </Card>
  );
};

// ── İş Geçmişi ──────────────────────────────────────────────────────
const JobHistorySection = ({
  history,
}: {
  history: readonly { jobTitle: string; startAge: number; endAge: number; endReason: string; finalSalary: number }[];
}) => (
  <View className="gap-xs">
    <Text className="text-sm font-outfit-bold text-text-secondary uppercase">
      İş Geçmişi
    </Text>
    {[...history].reverse().map((entry, i) => (
      <View key={`${entry.jobTitle}-${entry.startAge}-${i}`} className="flex-row items-center justify-between py-1">
        <View className="flex-1">
          <Text className="text-sm font-outfit text-text-primary">
            {entry.jobTitle}
          </Text>
          <Text className="text-xs font-mono text-text-tertiary">
            {entry.startAge}–{entry.endAge} yaş
          </Text>
        </View>
        <Badge
          label={END_REASON_LABELS[entry.endReason] ?? entry.endReason}
          variant={
            entry.endReason === 'promoted'
              ? 'success'
              : entry.endReason === 'fired'
                ? 'danger'
                : entry.endReason === 'retired'
                  ? 'info'
                  : 'warning'
          }
        />
      </View>
    ))}
  </View>
);
