import React, { useCallback, useMemo, useState } from 'react';
import { View, Text, ScrollView } from 'react-native';

import { Card, Button, Badge, ProgressBar } from '@/shared/components/ui';
import { formatMoney } from '@/shared/utils';
import { colors } from '@/shared/theme';

import { useGameStore, useCharacter } from '../stores/gameStore';
import { EDUCATION_LIST, hasCompletedEdu } from '../data/education';
import {
  getAvailableDepartments,
  getAllDepartmentsSorted,
  getScoreDescription,
  isFacultyCompatible,
} from '../data/departments';
import { getJobById } from '../data/jobs';

import type { Education, Department } from '../types';

type ExamView = 'default' | 'result' | 'departments';

/** Bölümün kariyer zincirini göster (ilk 3 iş) */
const getCareerChain = (dept: Department): string => {
  const titles = dept.unlockedCareers.slice(0, 4).map((id) => {
    const job = getJobById(id);
    return job?.title ?? id;
  });
  return titles.join(' → ');
};

export const EduList = () => {
  const character = useCharacter();
  const startEdu = useGameStore((s) => s.startEdu);
  const dropOut = useGameStore((s) => s.dropOut);
  const takeExam = useGameStore((s) => s.takeExam);
  const selectDepartment = useGameStore((s) => s.selectDepartment);

  const [examView, setExamView] = useState<ExamView>('default');

  const handleStartEdu = useCallback(
    (edu: Education) => startEdu(edu),
    [startEdu],
  );
  const handleDropOut = useCallback(() => dropOut(), [dropOut]);
  const handleTakeExam = useCallback(() => {
    takeExam();
    setExamView('result');
  }, [takeExam]);
  const handleSelectDept = useCallback(
    (deptId: string) => {
      selectDepartment(deptId);
      setExamView('default');
    },
    [selectDepartment],
  );

  // Tamamlanan ve mevcut eğitimler
  const { completed, availablePostGrad } = useMemo(() => {
    if (!character) return { completed: [], availablePostGrad: [] };

    const done = EDUCATION_LIST.filter((e) =>
      character.education.includes(e.name),
    );

    // Sadece Y.Lisans ve Doktora (üniversite artık sınav akışında)
    const avail = EDUCATION_LIST.filter((e) => {
      if (e.auto) return false;
      if (character.education.includes(e.name)) return false;
      if (character.smarts < e.smartsReq) return false;
      if (character.age < e.minAge) return false;
      if (character.age > e.maxAge) return false;
      if (e.prereq && !hasCompletedEdu(character.education, e.prereq)) return false;
      // Lisansüstü eğitim için üniversite bölümü gerekli
      if ((e.name === 'Yüksek Lisans' || e.name === 'Doktora') && !character.universityDepartment) return false;
      return true;
    });

    return { completed: done, availablePostGrad: avail };
  }, [character?.education, character?.smarts, character?.age]);

  if (!character) return null;

  // Lise diploması var mı
  const hasLise = character.education.includes('Lise');
  // Üniversite mezunu mu
  const hasUni = character.education.some((e) => e.startsWith('Üniversite'));
  // Üniversite öğrencisi mi
  const isUniStudent = character.currentEdu?.name.startsWith('Üniversite') ?? false;
  // Yükseköğretim öğrencisi mi (üniversite, Y.Lisans, Doktora)
  const isHigherEduStudent = character.currentEdu && !character.currentEdu.auto;
  // Sınav alınabilir mi
  const canTakeExam = hasLise && !hasUni && !isUniStudent && character.lastExamAge !== character.age;
  // Sınav sonucu var mı (bu yıl)
  const hasExamResult = character.lastExamScore !== null && character.lastExamAge === character.age;

  // Dropout izin kontrolü
  const canDropOut = (() => {
    if (!character.currentEdu) return false;
    if (character.currentEdu.dropCanAge === null) return false;
    if (character.currentEdu.auto && character.age < character.currentEdu.dropCanAge) return false;
    return true;
  })();

  // Prestij yıldızları
  const renderPrestige = (prestige: number) => {
    const stars = '★'.repeat(prestige) + '☆'.repeat(10 - prestige);
    return stars;
  };

  // Sınav başarı tahmini (Y.Lisans/Doktora için)
  const calcPassChance = (edu: Education) => {
    if (!edu.examRequired) return 100;
    const smartsBonus = Math.max(0, (character.smarts - edu.smartsReq) * 0.8);
    return Math.min(95, Math.round(edu.examPassRate + smartsBonus));
  };

  // ── SINAV SONUÇ EKRANI ────────────────────────────────────────────────
  if (examView === 'result' && hasExamResult) {
    const score = character.lastExamScore!;
    const desc = getScoreDescription(score);
    const canSelectDept = score >= 150;

    return (
      <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
        <Card>
          <View className="items-center gap-sm">
            <Text className="text-2xl">📊</Text>
            <Text className="text-base font-outfit-bold text-text-primary uppercase">
              Sınav Sonucu
            </Text>
            <Text className="text-3xl font-mono text-brand-primary">
              {score} / 500
            </Text>
            <Badge label={desc.percentile} variant={score >= 300 ? 'success' : score >= 150 ? 'warning' : 'danger'} />
            <Text className="text-sm font-outfit text-text-secondary text-center mt-sm">
              {desc.label}
            </Text>
          </View>
        </Card>

        <View className="flex-row gap-sm">
          {canSelectDept && (
            <View className="flex-1">
              <Button
                label="Bölüm Seç →"
                onPress={() => setExamView('departments')}
                variant="primary"
              />
            </View>
          )}
          <View className="flex-1">
            <Button
              label="Seneye Tekrar"
              onPress={() => setExamView('default')}
              variant="secondary"
            />
          </View>
        </View>
      </ScrollView>
    );
  }

  // ── BÖLÜM SEÇİM EKRANI ───────────────────────────────────────────────
  if (examView === 'departments' && hasExamResult) {
    const score = character.lastExamScore!;
    const allDepts = getAllDepartmentsSorted();
    const devletDepts = allDepts.filter((d) => d.type === 'devlet');
    const ozelDepts = allDepts.filter((d) => d.type === 'ozel');

    return (
      <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
        <Card>
          <View className="flex-row items-center justify-between">
            <Text className="text-base font-outfit-bold text-text-primary">
              🎓 Bölüm Seçimi
            </Text>
            <Text className="text-sm font-mono text-brand-primary">
              Puanın: {score}/500
            </Text>
          </View>
        </Card>

        {/* Devlet Üniversiteleri */}
        <Text className="text-base font-outfit-bold text-text-primary">
          🏛️ Devlet Üniversiteleri
        </Text>
        {devletDepts.map((dept) => (
          <DepartmentCard
            key={dept.id}
            dept={dept}
            score={score}
            money={character.money}
            onSelect={handleSelectDept}
            renderPrestige={renderPrestige}
          />
        ))}

        {/* Özel Üniversiteler */}
        <Text className="text-base font-outfit-bold text-text-primary mt-md">
          🏫 Özel Üniversiteler
        </Text>
        {ozelDepts.map((dept) => (
          <DepartmentCard
            key={dept.id}
            dept={dept}
            score={score}
            money={character.money}
            onSelect={handleSelectDept}
            renderPrestige={renderPrestige}
          />
        ))}

        <Button
          label="← Seneye Tekrar Gir"
          onPress={() => setExamView('default')}
          variant="ghost"
        />
      </ScrollView>
    );
  }

  // ── ANA EĞİTİM EKRANI ────────────────────────────────────────────────
  return (
    <ScrollView className="flex-1" contentContainerClassName="p-lg gap-md">
      {/* Devam Eden Eğitim */}
      {character.currentEdu && (
        <Card>
          <View className="flex-row items-center justify-between mb-sm">
            <Text className="text-sm font-outfit-bold text-text-secondary uppercase">
              Devam Eden Eğitim
            </Text>
            <Badge label="Okuyor" variant="info" />
          </View>

          {isUniStudent && character.universityDepartment ? (
            <UniversityProgressCard
              dept={character.universityDepartment}
              yearsLeft={character.eduYearsLeft}
              canDropOut={canDropOut}
              onDropOut={handleDropOut}
              renderPrestige={renderPrestige}
            />
          ) : (
            <>
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
            </>
          )}
        </Card>
      )}

      {/* YKS Sınav Bölümü */}
      {hasLise && !hasUni && !isUniStudent && (
        <ExamSection
          character={character}
          canTakeExam={canTakeExam}
          hasExamResult={hasExamResult}
          onTakeExam={handleTakeExam}
          onViewResult={() => setExamView('result')}
        />
      )}

      {/* Lisansüstü Eğitim Seçenekleri */}
      {availablePostGrad.length > 0 && !isHigherEduStudent && (
        <>
          <Text className="text-base font-outfit-bold text-text-primary">
            Lisansüstü Eğitim
          </Text>
          {availablePostGrad.map((edu) => (
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
          ))}
        </>
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
          {/* Üniversite mezuniyeti ayrıca göster */}
          {character.education
            .filter((e) => e.startsWith('Üniversite'))
            .map((e) => (
              <Card key={e}>
                <View className="flex-row items-center gap-sm">
                  <Text className="text-base">🎓</Text>
                  <Text className="text-sm font-outfit-semibold text-text-primary">
                    {e}
                  </Text>
                  <Badge label="Mezun" variant="success" />
                </View>
                {character.universityDepartment && (
                  <Text className="text-xs font-outfit text-text-tertiary mt-1">
                    YKS: {character.lastExamScore}/500
                  </Text>
                )}
              </Card>
            ))}
        </>
      )}

      {/* Boş durum mesajı */}
      {!character.currentEdu && completed.length === 0 && availablePostGrad.length === 0 && !hasLise && (
        <Card>
          <Text className="text-sm font-outfit text-text-tertiary text-center">
            Zorunlu eğitim otomatik olarak başlayacak.
          </Text>
        </Card>
      )}
    </ScrollView>
  );
};

// ── Sınav Bölümü ──────────────────────────────────────────────────────
const ExamSection = ({
  character,
  canTakeExam,
  hasExamResult,
  onTakeExam,
  onViewResult,
}: {
  character: NonNullable<ReturnType<typeof useCharacter>>;
  canTakeExam: boolean;
  hasExamResult: boolean;
  onTakeExam: () => void;
  onViewResult: () => void;
}) => {
  const estimatedScore = useMemo(() => {
    const base = character.smarts * 3;
    const studyBonus = Math.min(80, character.examStudyCount * 4);
    return Math.min(500, base + studyBonus);
  }, [character.smarts, character.examStudyCount]);

  const estimatedPercent = Math.min(100, Math.round((estimatedScore / 500) * 100));

  return (
    <Card>
      <View className="items-center gap-sm">
        <Text className="text-2xl">📝</Text>
        <Text className="text-base font-outfit-bold text-text-primary uppercase">
          Üniversite Giriş Sınavı (YKS)
        </Text>
      </View>

      <View className="mt-md gap-xs">
        <View className="flex-row justify-between">
          <Text className="text-xs font-outfit text-text-secondary">Zeka</Text>
          <Text className="text-xs font-mono text-text-primary">{character.smarts}</Text>
        </View>
        <View className="flex-row justify-between">
          <Text className="text-xs font-outfit text-text-secondary">Ders Çalışma (Lise)</Text>
          <Text className="text-xs font-mono text-text-primary">{character.examStudyCount} kez</Text>
        </View>
        <View className="flex-row justify-between">
          <Text className="text-xs font-outfit text-text-secondary">Mutluluk</Text>
          <Text className="text-xs font-mono text-text-primary">{character.happiness}</Text>
        </View>
      </View>

      <View className="mt-md">
        <View className="flex-row items-center justify-between mb-1">
          <Text className="text-xs font-outfit text-text-secondary">
            Tahmini Başarı
          </Text>
          <Text className="text-xs font-mono text-text-secondary">
            ~{estimatedScore}/500
          </Text>
        </View>
        <ProgressBar
          value={estimatedPercent}
          color={estimatedPercent >= 60 ? colors.success : estimatedPercent >= 30 ? colors.warning : colors.danger}
        />
      </View>

      <Text className="text-xs font-outfit text-text-tertiary text-center mt-md">
        ⚠️ Sınav yılda 1 kez yapılır. Sonraki yıl tekrar girebilirsin.
      </Text>

      {hasExamResult ? (
        <View className="mt-md gap-sm">
          <Text className="text-sm font-outfit text-brand-primary text-center">
            Bu yıl sınava girdin. Puan: {character.lastExamScore}/500
          </Text>
          <Button label="Sonucu Gör" onPress={onViewResult} variant="primary" />
        </View>
      ) : (
        <View className="mt-md">
          <Button
            label="Sınava Gir"
            onPress={onTakeExam}
            variant={canTakeExam ? 'primary' : 'disabled'}
          />
        </View>
      )}
    </Card>
  );
};

// ── Bölüm Kartı ──────────────────────────────────────────────────────
const DepartmentCard = ({
  dept,
  score,
  money,
  onSelect,
  renderPrestige,
}: {
  dept: Department;
  score: number;
  money: number;
  onSelect: (id: string) => void;
  renderPrestige: (p: number) => string;
}) => {
  const qualified = score >= dept.minScore;
  const affordable = money >= dept.annualCost;
  const gap = dept.minScore - score;

  return (
    <Card>
      <View className="gap-xs">
        <View className="flex-row items-center gap-sm">
          <Text className="text-base">
            {qualified ? '✅' : '❌'}
          </Text>
          <Text className={`text-base font-outfit-semibold ${qualified ? 'text-text-primary' : 'text-text-disabled'}`}>
            {dept.name}
          </Text>
        </View>

        <Text className="text-xs font-outfit text-text-secondary">
          {dept.faculty} · {dept.totalYears} yıl · {dept.type === 'devlet' ? 'Devlet' : 'Özel'}
        </Text>

        <Text className="text-xs font-mono text-text-secondary">
          ₺{dept.annualCost.toLocaleString('tr-TR')}/yıl · Prestij: {renderPrestige(dept.prestige)}
        </Text>

        <View className="flex-row items-center gap-sm">
          <Text className="text-xs font-outfit text-text-tertiary">
            Min: {dept.minScore}
          </Text>
          <Text className={`text-xs font-mono ${qualified ? 'text-success' : 'text-danger'}`}>
            Puanın: {score} {qualified ? '✅' : '❌'}
          </Text>
        </View>

        {!qualified && (
          <Text className="text-xs font-outfit text-danger">
            🔒 {gap} puan eksik
          </Text>
        )}

        {qualified && !affordable && (
          <Text className="text-xs font-outfit text-warning">
            💰 Yeterli paranız yok (₺{dept.annualCost.toLocaleString('tr-TR')} gerekli)
          </Text>
        )}

        {dept.type === 'ozel' && qualified && (
          <Text className="text-xs font-outfit text-warning">
            ⚠️ Yüksek ücret!
          </Text>
        )}

        {qualified && (
          <View className="mt-xs gap-1">
            <Text className="text-xs font-outfit text-text-tertiary">
              {dept.description}
            </Text>
            <Text className="text-xs font-outfit text-brand-primary">
              Kariyer: {getCareerChain(dept)}
            </Text>
          </View>
        )}

        {qualified && affordable && (
          <View className="mt-sm">
            <Button
              label="Yerleş"
              onPress={() => onSelect(dept.id)}
              variant="secondary"
            />
          </View>
        )}
      </View>
    </Card>
  );
};

// ── Üniversite İlerleme Kartı ─────────────────────────────────────────
const UniversityProgressCard = ({
  dept,
  yearsLeft,
  canDropOut,
  onDropOut,
  renderPrestige,
}: {
  dept: Department;
  yearsLeft: number;
  canDropOut: boolean;
  onDropOut: () => void;
  renderPrestige: (p: number) => string;
}) => (
  <View className="gap-xs">
    <Text className="text-lg font-outfit-bold text-text-primary">
      🎓 {dept.name}
    </Text>
    <Text className="text-xs font-outfit text-text-secondary">
      {dept.type === 'devlet' ? 'Devlet Üniversitesi' : 'Özel Üniversite'}
    </Text>
    <Text className="text-sm font-mono text-text-secondary">
      {yearsLeft} yıl kaldı · ₺{dept.annualCost.toLocaleString('tr-TR')}/yıl
    </Text>
    <Text className="text-xs font-outfit text-text-tertiary">
      Prestij: {renderPrestige(dept.prestige)}
    </Text>
    <Text className="text-xs font-outfit text-brand-primary">
      Kariyer: {getCareerChain(dept)}
    </Text>

    {canDropOut && (
      <View className="mt-md">
        <Button label="Okulu Bırak" onPress={onDropOut} variant="danger" />
      </View>
    )}
  </View>
);
