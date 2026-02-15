import { create } from 'zustand';

import { GAME_BALANCE, DEATH_REASONS } from '@/shared/constants';

import { MALE_NAMES, FEMALE_NAMES, SURNAMES, CITIES } from '../data/names';
import { getZodiac } from '../data/zodiac';
import { getEventsForAge } from '../data/events';
import { MARRIAGE_EVENTS, FRIENDSHIP_EVENTS, FAMILY_EVENTS } from '../data/relationships';
import { EDUCATION_LIST, hasCompletedEdu } from '../data/education';
import { ACTIVITIES } from '../data/activities';
import { JOBS, getTaxRate, meetsEducationReq, getJobById } from '../data/jobs';
import { getDepartmentById, GENERAL_UNI_JOBS } from '../data/departments';
import { rand, pick, clamp, pct } from '../utils';

import type {
  Gender,
  Screen,
  TabId,
  Character,
  LogEntry,
  Education,
  Relationship,
  CareerState,
  JobHistoryEntry,
  GameStore,
} from '../types';

let nextRelationId = 1;
const genRelationId = () => `rel-${nextRelationId++}`;

const createInitialCareer = (): CareerState => ({
  currentJob: null,
  currentSalary: 0,
  yearsInCurrentJob: 0,
  totalWorkYears: 0,
  performanceScore: 50,
  jobHistory: [],
  isRetired: false,
  pension: 0,
  lifetimeEarnings: 0,
});

const createInitialCharacter = (gender: Gender): Character => {
  const names = gender === 'M' ? MALE_NAMES : FEMALE_NAMES;
  const name = pick(names);
  const surname = pick(SURNAMES);
  const city = pick(CITIES);
  const birthMonth = rand(1, 12);
  const birthDay = rand(1, 28);
  const zodiac = getZodiac(birthMonth, birthDay);
  const currentYear = new Date().getFullYear();
  const initialHealth = rand(40, 80);

  // Anne ve baba oluştur
  const motherName = pick(FEMALE_NAMES);
  const fatherName = pick(MALE_NAMES);

  const initialRelations: Relationship[] = [
    {
      id: genRelationId(),
      name: fatherName,
      surname,
      type: 'parent',
      age: rand(25, 40),
      closeness: rand(60, 90),
      isAlive: true,
    },
    {
      id: genRelationId(),
      name: motherName,
      surname,
      type: 'parent',
      age: rand(22, 38),
      closeness: rand(65, 95),
      isAlive: true,
    },
  ];

  // %40 kardeş şansı
  if (pct(40)) {
    const sibGender = pct(50) ? 'M' : 'F';
    const sibNames = sibGender === 'M' ? MALE_NAMES : FEMALE_NAMES;
    initialRelations.push({
      id: genRelationId(),
      name: pick(sibNames),
      surname,
      type: 'sibling',
      age: rand(0, 5),
      closeness: rand(50, 80),
      isAlive: true,
    });
  }

  return {
    name,
    surname,
    gender,
    city,
    zodiac,
    birthYear: currentYear,
    age: 0,
    health: initialHealth,
    happiness: rand(50, 90),
    smarts: rand(20, 60),
    looks: rand(30, 80),
    money: 0,
    career: createInitialCareer(),
    education: [],
    currentEdu: null,
    eduYearsLeft: 0,
    isAlive: true,
    deathAge: null,
    deathReason: null,
    relationships: initialRelations,
    isMarried: false,
    childCount: 0,
    achievements: [],
    actionCounts: {},
    travelCount: 0,
    crimeCount: 0,
    lowestHealth: initialHealth,
    highestHealth: initialHealth,
    divorceCount: 0,
    marriageYear: null,
    examStudyCount: 0,
    lastExamScore: null,
    lastExamAge: null,
    universityDepartment: null,
  };
};

const AGE_MILESTONES: Record<number, string> = {
  1: 'İlk yaş günün kutlu olsun! 🎂',
  4: 'Anaokulu çağına geldin!',
  13: 'Artık bir genç oluyorsun!',
  18: 'Reşit oldun! Yetişkin hayatına hoş geldin! 🎓',
  30: '30 yaşına girdin. Hayat hızla geçiyor...',
  40: '40 yaşında bir olgunluk var.',
  50: 'Yarım asır geride kaldı!',
  65: 'Emeklilik yaşına geldin! 🏖️',
};

export const useGameStore = create<GameStore>((set, get) => ({
  screen: 'menu',
  character: null,
  log: [],
  activeTab: 'life',

  setScreen: (screen: Screen) => set({ screen }),

  createCharacter: (gender: Gender) => {
    const character = createInitialCharacter(gender);
    const birthLog: LogEntry = {
      age: 0,
      text: `${character.name} ${character.surname}, ${character.city}'de dünyaya geldi. ${character.zodiac.emoji} ${character.zodiac.name} burcu.`,
      type: 'birth',
    };
    set({
      character,
      log: [birthLog],
      screen: 'game',
      activeTab: 'life',
    });
  },

  ageUp: () => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;

    const newLog: LogEntry[] = [];
    let { health, happiness, smarts, looks, money } = character;
    let {
      age, career, currentEdu, eduYearsLeft, education,
      relationships, isMarried, childCount, achievements,
      actionCounts, travelCount, crimeCount,
      lowestHealth, highestHealth, divorceCount, marriageYear,
    } = character;

    // Kariyer state klonla (mutate edeceğiz)
    let currentJob = career.currentJob;
    let currentSalary = career.currentSalary;
    let yearsInCurrentJob = career.yearsInCurrentJob;
    let totalWorkYears = career.totalWorkYears;
    let performanceScore = career.performanceScore;
    let jobHistory = [...career.jobHistory];
    let isRetired = career.isRetired;
    let pension = career.pension;
    let lifetimeEarnings = career.lifetimeEarnings;

    let isAlive = true;
    let deathAge: number | null = null;
    let deathReason: string | null = null;

    // Yaşı artır
    age += 1;

    // Sağlık azalması
    const healthDecay =
      age >= GAME_BALANCE.elderAge
        ? GAME_BALANCE.elderHealthDecay
        : GAME_BALANCE.baseHealthDecay;
    health -= healthDecay;

    // ── KARİYER İŞLEME ──────────────────────────────────────────────────
    if (currentJob && !isRetired) {
      // Maaş geliri (yıllık) + vergi
      const yearlyIncome = currentSalary * 12;
      const taxRate = getTaxRate(currentSalary);
      const taxDeduction = Math.round(yearlyIncome * taxRate);
      const netIncome = yearlyIncome - taxDeduction;
      money += netIncome;
      lifetimeEarnings += yearlyIncome;

      // Kıdem ve toplam çalışma yılı
      yearsInCurrentJob += 1;
      totalWorkYears += 1;

      // 1. Performans değerlendirmesi (yıllık)
      const perfBase = 50;
      const perfSmarts = Math.min(20, Math.floor(smarts / 5));
      const perfHappiness = Math.min(10, Math.floor(happiness / 10));
      const perfSeniority = Math.min(10, yearsInCurrentJob * 2);
      const perfLuck = rand(-10, 10);
      performanceScore = clamp(perfBase + perfSmarts + perfHappiness + perfSeniority + perfLuck);

      // 65+ yaş çalışma cezası
      if (age >= 65) {
        performanceScore = clamp(performanceScore - 10);
        const stressHealthLoss = rand(2, 5);
        health -= stressHealthLoss;
        newLog.push({
          age,
          text: `Yaşınıza rağmen çalışmaya devam ediyorsunuz. Sağlığınız etkileniyor. (-${stressHealthLoss} sağlık)`,
          type: 'bad',
        });
      }

      // Maaş ve kariyer log
      newLog.push({
        age,
        text: `${currentJob.title} olarak çalışıyorsun. Maaş: ₺${currentSalary.toLocaleString('tr-TR')}/ay (net: ₺${Math.round(netIncome / 12).toLocaleString('tr-TR')}/ay)`,
        type: 'event',
      });

      // 2. Yıllık zam (performans > 40 ise)
      if (performanceScore > 40) {
        const raiseBase = rand(3, 8) + (performanceScore - 50) / 20;
        const raisePercent = clamp(raiseBase, 2, 15);
        const oldSalary = currentSalary;
        currentSalary = Math.round(currentSalary * (1 + raisePercent / 100));
        newLog.push({
          age,
          text: `Maaşına %${Math.round(raisePercent)} zam geldi! Yeni maaş: ₺${currentSalary.toLocaleString('tr-TR')}/ay`,
          type: 'good',
        });
      }

      // 3. Terfi kontrolü
      if (
        currentJob.promotionChain.length > 0 &&
        yearsInCurrentJob >= currentJob.experienceYearsForPromo &&
        performanceScore >= 65
      ) {
        const nextJobId = currentJob.promotionChain[0];
        const nextJob = getJobById(nextJobId);

        if (nextJob && smarts >= nextJob.minSmarts) {
          const promoChance = clamp(
            (performanceScore - 60) * 1.5 + yearsInCurrentJob * 3,
            5,
            60,
          );

          if (pct(promoChance)) {
            // Terfi al!
            const oldTitle = currentJob.title;
            jobHistory = [
              ...jobHistory,
              {
                jobTitle: currentJob.title,
                sector: currentJob.sector,
                startAge: age - yearsInCurrentJob,
                endAge: age,
                endReason: 'promoted' as const,
                finalSalary: currentSalary,
              },
            ];

            currentSalary = Math.max(
              Math.round(currentSalary * 1.20),
              nextJob.baseSalary,
            );
            currentJob = nextJob;
            yearsInCurrentJob = 0;
            happiness = clamp(happiness + 15);

            newLog.push({
              age,
              text: `Terfi aldın! Yeni pozisyon: ${nextJob.title} 🎉 (₺${currentSalary.toLocaleString('tr-TR')}/ay)`,
              type: 'milestone',
            });

            // İlk terfi başarımı
            if (jobHistory.filter((h) => h.endReason === 'promoted').length === 1) {
              newLog.push({
                age,
                text: 'İlk terfini aldın! ⭐',
                type: 'milestone',
              });
            }

            // Üst yönetim başarımı
            if (nextJob.sector === 'executive') {
              newLog.push({
                age,
                text: 'Üst düzey yönetici oldun! 🏆',
                type: 'milestone',
              });
            }
          }
        }
      }

      // 4. İşten çıkarılma riski (performans < 30)
      if (performanceScore < 30 && currentJob) {
        const adjustedFireChance = currentJob.fireChance + (30 - performanceScore);
        if (pct(adjustedFireChance)) {
          // İşten çıkarıldın
          jobHistory = [
            ...jobHistory,
            {
              jobTitle: currentJob.title,
              sector: currentJob.sector,
              startAge: age - yearsInCurrentJob,
              endAge: age,
              endReason: 'fired' as const,
              finalSalary: currentSalary,
            },
          ];

          newLog.push({
            age,
            text: `${currentJob.title} işinden çıkarıldın. 😞`,
            type: 'bad',
          });

          currentJob = null;
          currentSalary = 0;
          yearsInCurrentJob = 0;
          happiness = clamp(happiness - 20);
        }
      }

      // 10 yıl aynı işte çalışma başarımı
      if (yearsInCurrentJob === 10 && currentJob) {
        newLog.push({
          age,
          text: '10 yıllık çalışan oldun! 🏅',
          type: 'milestone',
        });
      }

      // ₺1M toplam kazanç başarımı
      if (lifetimeEarnings >= 1_000_000 && career.lifetimeEarnings < 1_000_000) {
        newLog.push({
          age,
          text: 'Toplam ₺1.000.000 kazandın! 💰',
          type: 'milestone',
        });
      }
    }

    // Emekli maaşı
    if (isRetired && pension > 0) {
      const pensionYearly = pension * 12;
      money += pensionYearly;
      lifetimeEarnings += pensionYearly;
    }

    // Eğitim ilerlemesi
    let { universityDepartment, examStudyCount, lastExamScore, lastExamAge } = character;
    if (currentEdu && eduYearsLeft > 0) {
      eduYearsLeft -= 1;
      smarts += Math.floor(currentEdu.smartsGain / currentEdu.years);

      // Üniversite yıllık ücret (ilk yıl selectDepartment'ta ödendi, sonraki yıllar burada)
      if (universityDepartment && currentEdu.name.startsWith('Üniversite') && eduYearsLeft > 0) {
        money -= universityDepartment.annualCost;
        newLog.push({
          age,
          text: `Üniversite yıllık ücreti ödendi: ₺${universityDepartment.annualCost.toLocaleString('tr-TR')}`,
          type: 'event',
        });
      }

      if (eduYearsLeft === 0) {
        education = [...education, currentEdu.name];
        newLog.push({
          age,
          text: `${currentEdu.name} eğitimini tamamladın! 🎓`,
          type: 'milestone',
        });

        // Üniversite mezuniyet bonusu
        if (universityDepartment && currentEdu.name.startsWith('Üniversite')) {
          happiness = clamp(happiness + 15);
          newLog.push({
            age,
            text: `${universityDepartment.name} bölümünden mezun oldun! Tebrikler! 🎉`,
            type: 'good',
          });
        }

        currentEdu = null;
      } else {
        newLog.push({
          age,
          text: `${currentEdu.name} eğitimine devam ediyorsun. (${eduYearsLeft} yıl kaldı)`,
          type: 'event',
        });
      }
    }

    // 25+ yaş üniversitesiz sosyal baskı
    if (age >= 25 && !education.some((e) => e.startsWith('Üniversite')) && !currentEdu && pct(40)) {
      happiness = clamp(happiness - 3);
      newLog.push({
        age,
        text: "Çevrenden 'Hâlâ okumadın mı?' baskısı hissediyorsun. 😔",
        type: 'bad',
      });
    }

    // Zorunlu eğitim otomatik başlatma
    if (!currentEdu) {
      const autoEdu = EDUCATION_LIST.find(
        (e) =>
          e.auto &&
          age === e.minAge &&
          !education.includes(e.name) &&
          (e.prereq === null || hasCompletedEdu(education, e.prereq)),
      );
      if (autoEdu) {
        currentEdu = autoEdu;
        eduYearsLeft = autoEdu.years;
        newLog.push({
          age,
          text: `${autoEdu.name} eğitimine başladın! 📚`,
          type: 'milestone',
        });
      }
    }

    // İlişkileri yaşlandır
    relationships = relationships.map((r) => ({
      ...r,
      age: r.age + 1,
      closeness: clamp(r.closeness + rand(-5, 5)),
    }));

    // Ebeveyn ölüm kontrolü (60+ yaş sonrası)
    relationships = relationships.map((r) => {
      if (r.type === 'parent' && r.isAlive && r.age >= 60) {
        const parentDeathChance = 2 + (r.age - 60) * 1.5;
        if (pct(parentDeathChance)) {
          newLog.push({
            age,
            text: `${r.name} ${r.surname} vefat etti. Başın sağ olsun. 😢`,
            type: 'bad',
          });
          happiness -= 15;
          return { ...r, isAlive: false };
        }
      }
      return r;
    });

    // İlişki olayları
    if (isMarried && pct(30)) {
      const event = pick(MARRIAGE_EVENTS);
      newLog.push({ age, text: event.t, type: 'event' });
      if (event.fx.health) health += event.fx.health;
      if (event.fx.happiness) happiness += event.fx.happiness;
      if (event.fx.money) money += event.fx.money;
    }

    if (relationships.some((r) => r.type === 'friend' && r.isAlive) && pct(20)) {
      const event = pick(FRIENDSHIP_EVENTS);
      newLog.push({ age, text: event.t, type: 'event' });
      if (event.fx.happiness) happiness += event.fx.happiness;
      if (event.fx.money) money += event.fx.money;
    }

    if (childCount > 0 && pct(25)) {
      const event = pick(FAMILY_EVENTS);
      newLog.push({ age, text: event.t, type: 'event' });
      if (event.fx.health) health += event.fx.health;
      if (event.fx.happiness) happiness += event.fx.happiness;
      if (event.fx.smarts) smarts += event.fx.smarts;
      if (event.fx.money) money += event.fx.money;
    }

    // Arkadaş edinme şansı (18+ yaş, %15)
    if (age >= 18 && pct(15) && relationships.filter((r) => r.type === 'friend' && r.isAlive).length < 5) {
      const friendGender = pct(50) ? 'M' : 'F';
      const friendNames = friendGender === 'M' ? MALE_NAMES : FEMALE_NAMES;
      const friendAge = Math.max(16, age + rand(-5, 5));
      relationships = [
        ...relationships,
        {
          id: genRelationId(),
          name: pick(friendNames),
          surname: pick(SURNAMES),
          type: 'friend',
          age: friendAge,
          closeness: rand(40, 70),
          isAlive: true,
        },
      ];
      newLog.push({ age, text: 'Yeni bir arkadaş edindin!', type: 'good' });
    }

    // Rastgele olaylar (%60 şans)
    if (pct(GAME_BALANCE.eventChance)) {
      const pool = getEventsForAge(age);
      const event = pick(pool);
      newLog.push({ age, text: event.t, type: 'event' });
      if (event.fx.health) health += event.fx.health;
      if (event.fx.happiness) happiness += event.fx.happiness;
      if (event.fx.smarts) smarts += event.fx.smarts;
      if (event.fx.looks) looks += event.fx.looks;
      if (event.fx.money) money += event.fx.money;
    }

    // Yaş dönüm noktaları
    const milestone = AGE_MILESTONES[age];
    if (milestone) {
      newLog.push({ age, text: milestone, type: 'milestone' });
    }

    // İstatistikleri sınırla
    health = clamp(health);
    happiness = clamp(happiness);
    smarts = clamp(smarts);
    looks = clamp(looks);
    money = Math.max(GAME_BALANCE.moneyMin, money);

    // Sağlık takibi
    if (health < lowestHealth) lowestHealth = health;
    if (health > highestHealth) highestHealth = health;

    // Ölüm kontrolü
    if (health <= 0) {
      isAlive = false;
      deathAge = age;
      deathReason = pick(DEATH_REASONS.filter((r) => r !== 'Yaşlılık'));
      newLog.push({
        age,
        text: `${character.name}, ${age} yaşında hayatını kaybetti. Sebep: ${deathReason}`,
        type: 'death',
      });
    } else if (age >= GAME_BALANCE.naturalDeathMinAge) {
      const deathChance =
        GAME_BALANCE.naturalDeathBaseChance +
        (age - GAME_BALANCE.naturalDeathMinAge) *
          GAME_BALANCE.naturalDeathChancePerYear;
      if (pct(deathChance)) {
        isAlive = false;
        deathAge = age;
        deathReason = age >= 80 ? 'Yaşlılık' : pick([...DEATH_REASONS]);
        newLog.push({
          age,
          text: `${character.name}, ${age} yaşında hayatını kaybetti. Sebep: ${deathReason}`,
          type: 'death',
        });
      }
    }

    // Yaşın başında özet log
    if (newLog.length === 0) {
      newLog.push({
        age,
        text: `${age} yaşına girdin.`,
        type: 'event',
      });
    }

    // Kariyer state'i güncelle
    const updatedCareer: CareerState = {
      currentJob,
      currentSalary,
      yearsInCurrentJob,
      totalWorkYears,
      performanceScore,
      jobHistory,
      isRetired,
      pension,
      lifetimeEarnings,
    };

    const updatedCharacter: Character = {
      ...character,
      age,
      health,
      happiness,
      smarts,
      looks,
      money,
      career: updatedCareer,
      education,
      currentEdu,
      eduYearsLeft,
      isAlive,
      deathAge,
      deathReason,
      relationships,
      isMarried,
      childCount,
      achievements,
      actionCounts,
      travelCount,
      crimeCount,
      lowestHealth,
      highestHealth,
      divorceCount,
      marriageYear,
      universityDepartment,
      examStudyCount,
      lastExamScore,
      lastExamAge,
    };

    // Başarım kontrolü
    const { ACHIEVEMENTS } = require('../data/achievements') as {
      ACHIEVEMENTS: ReadonlyArray<{ id: string; title: string; emoji: string; condition: (c: Character, l: LogEntry[]) => boolean }>;
    };
    const allLog = [...log, ...newLog];
    const newAchievements = [...achievements];
    for (const ach of ACHIEVEMENTS) {
      if (!newAchievements.includes(ach.id)) {
        try {
          if (ach.condition(updatedCharacter, allLog)) {
            newAchievements.push(ach.id);
            newLog.push({
              age,
              text: `🏆 Başarım açıldı: ${ach.emoji} ${ach.title}!`,
              type: 'milestone',
            });
          }
        } catch {
          // Başarım kontrolünde hata — atla
        }
      }
    }
    updatedCharacter.achievements = newAchievements;

    set({
      character: updatedCharacter,
      log: [...log, ...newLog],
      screen: isAlive ? 'game' : 'dead',
    });
  },

  applyForJob: (jobId: string) => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;
    if (character.career.isRetired) return;
    if (character.career.currentJob) return; // Önce mevcut işten ayrılmalı

    // Yükseköğretim öğrencisi tam zamanlı çalışamaz
    if (character.currentEdu && !character.currentEdu.auto) return;

    const job = getJobById(jobId);
    if (!job) return;

    // Executive pozisyonlar doğrudan başvuruya kapalı
    if (job.sector === 'executive') return;

    // Yaş kontrolü
    if (character.age < job.minAge || character.age > job.maxAge) return;

    // Eğitim kontrolü
    if (!meetsEducationReq(character.education, job.minEducation)) return;

    // Zeka kontrolü
    if (character.smarts < job.minSmarts) return;

    // Üniversite gerektiren işlerde bölüm kilidi kontrolü
    if (job.minEducation === 'universite') {
      const dept = character.universityDepartment;
      if (!dept) return; // Üniversite mezunu değil

      // İş, bölümün açtığı kariyerler arasında mı VEYA genel üniversite işi mi?
      const isUnlockedByDept = dept.unlockedCareers.includes(job.id);
      const isGeneralJob = GENERAL_UNI_JOBS.has(job.id);
      if (!isUnlockedByDept && !isGeneralJob) return;
    }

    // Mülakat/işe alım şansı
    const smartsBonus = Math.min(30, (character.smarts - job.minSmarts) * 1.5);
    const expBonus = Math.min(20, character.career.totalWorkYears * 2);
    const overqualifiedPenalty =
      job.sector === 'entry' && character.smarts > job.minSmarts + 30 ? -10 : 0;
    const baseChance = 50 + smartsBonus + expBonus + overqualifiedPenalty;
    const hireChance = clamp(baseChance, 20, 95);

    if (!pct(hireChance)) {
      set({
        character: { ...character, happiness: clamp(character.happiness - 3) },
        log: [
          ...log,
          {
            age: character.age,
            text: `${job.title} başvurun reddedildi. 😕`,
            type: 'bad',
          },
        ],
      });
      return;
    }

    // İşe alındı! — Prestij maaş bonusu hesapla
    let startingSalary = job.baseSalary;
    if (character.universityDepartment) {
      let prestige = character.universityDepartment.prestige;
      if (character.education.includes('Yüksek Lisans')) prestige += 1;
      if (character.education.includes('Doktora')) prestige += 2;
      startingSalary = Math.round(job.baseSalary * (1 + prestige / 100));
    }

    const isFirstJob = character.career.jobHistory.length === 0 && !character.career.currentJob;
    const newLog: LogEntry[] = [
      {
        age: character.age,
        text: `${job.title} olarak işe başladın! Maaş: ₺${startingSalary.toLocaleString('tr-TR')}/ay 💼`,
        type: 'good',
      },
    ];

    if (isFirstJob) {
      newLog.push({
        age: character.age,
        text: 'İlk işine başladın! 💼',
        type: 'milestone',
      });
    }

    set({
      character: {
        ...character,
        career: {
          ...character.career,
          currentJob: job,
          currentSalary: startingSalary,
          yearsInCurrentJob: 0,
          performanceScore: 50,
        },
      },
      log: [...log, ...newLog],
    });
  },

  quitJob: () => {
    const { character, log } = get();
    if (!character || !character.isAlive || !character.career.currentJob) return;

    const job = character.career.currentJob;
    const historyEntry: JobHistoryEntry = {
      jobTitle: job.title,
      sector: job.sector,
      startAge: character.age - character.career.yearsInCurrentJob,
      endAge: character.age,
      endReason: 'quit',
      finalSalary: character.career.currentSalary,
    };

    set({
      character: {
        ...character,
        career: {
          ...character.career,
          currentJob: null,
          currentSalary: 0,
          yearsInCurrentJob: 0,
          jobHistory: [...character.career.jobHistory, historyEntry],
        },
        happiness: clamp(character.happiness - 5),
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${job.title} işinden ayrıldın.`,
          type: 'event',
        },
      ],
    });
  },

  retire: () => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;
    if (character.career.isRetired) return;
    if (character.age < 60) return;

    const job = character.career.currentJob;
    const lastSalary = character.career.currentSalary;
    const totalWorkYears = character.career.totalWorkYears;

    // Emekli maaşı hesaplama
    const rawPension = (lastSalary * 0.65 * totalWorkYears) / 25;
    const pensionCap = lastSalary * 0.80;
    const pensionAmount = Math.round(Math.min(rawPension, pensionCap));

    const newJobHistory = job
      ? [
          ...character.career.jobHistory,
          {
            jobTitle: job.title,
            sector: job.sector,
            startAge: character.age - character.career.yearsInCurrentJob,
            endAge: character.age,
            endReason: 'retired' as const,
            finalSalary: lastSalary,
          },
        ]
      : character.career.jobHistory;

    const newLog: LogEntry[] = [
      {
        age: character.age,
        text: `Emekli oldun! 🎉`,
        type: 'milestone',
      },
    ];

    if (pensionAmount > 0) {
      newLog.push({
        age: character.age,
        text: `Emekli maaşın bağlandı: ₺${pensionAmount.toLocaleString('tr-TR')}/ay`,
        type: 'good',
      });
    }

    set({
      character: {
        ...character,
        career: {
          ...character.career,
          currentJob: null,
          currentSalary: 0,
          yearsInCurrentJob: 0,
          isRetired: true,
          pension: pensionAmount,
          jobHistory: newJobHistory,
        },
        happiness: clamp(character.happiness + 10),
      },
      log: [...log, ...newLog],
    });
  },

  startEdu: (edu: Education) => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;
    if (character.currentEdu) return;
    if (edu.auto) return; // Zorunlu eğitim manuel başlatılamaz
    if (character.smarts < edu.smartsReq) return;
    if (character.age < edu.minAge) return;
    if (character.age > edu.maxAge) return;
    if (character.education.includes(edu.name)) return;

    // Ön koşul kontrolü
    if (edu.prereq && !hasCompletedEdu(character.education, edu.prereq)) return;

    // Çift üniversite engeli
    if (edu.name.startsWith('Üniversite') && character.education.some((e) => e.startsWith('Üniversite'))) return;

    // Lisansüstü eğitim için üniversite bölümü gerekli (fakülte uyumluluğu)
    if ((edu.name === 'Yüksek Lisans' || edu.name === 'Doktora') && !character.universityDepartment) return;

    // Giriş sınavı
    if (edu.examRequired) {
      const smartsBonus = Math.max(0, (character.smarts - edu.smartsReq) * 0.8);
      const passChance = Math.min(95, edu.examPassRate + smartsBonus);
      if (!pct(passChance)) {
        set({
          character: { ...character, happiness: clamp(character.happiness - 8) },
          log: [
            ...log,
            {
              age: character.age,
              text: `${edu.name} giriş sınavını geçemedin. 😔`,
              type: 'bad',
            },
          ],
        });
        return;
      }
      // Sınavı geçti
      set((prev) => ({
        log: [
          ...prev.log,
          {
            age: character.age,
            text: `${edu.name} giriş sınavını başarıyla geçtin! 🎉`,
            type: 'good',
          },
        ],
      }));
    }

    set({
      character: {
        ...character,
        currentEdu: edu,
        eduYearsLeft: edu.years,
        money: character.money - edu.cost,
      },
      log: [
        ...get().log,
        {
          age: character.age,
          text: `${edu.name} eğitimine başladın! (${edu.years} yıl, ${edu.cost > 0 ? `₺${edu.cost.toLocaleString('tr-TR')}` : 'Ücretsiz'})`,
          type: 'good',
        },
      ],
    });
  },

  dropOut: () => {
    const { character, log } = get();
    if (!character || !character.isAlive || !character.currentEdu) return;

    const edu = character.currentEdu;

    // Zorunlu eğitim bırakılamaz
    if (edu.dropCanAge === null) return;

    // Yaş sınırı kontrolü
    if (edu.auto && character.age < edu.dropCanAge) return;

    // Üniversite bırakırsa department bilgisini de temizle
    const isUniDropout = character.universityDepartment && edu.name.startsWith('Üniversite');

    set({
      character: {
        ...character,
        currentEdu: null,
        eduYearsLeft: 0,
        happiness: clamp(character.happiness - 10),
        universityDepartment: isUniDropout ? null : character.universityDepartment,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${edu.name} eğitimini yarıda bıraktın.`,
          type: 'bad',
        },
      ],
    });
  },

  takeExam: () => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;

    // Lise diploması gerekli
    if (!character.education.includes('Lise')) return;

    // Zaten üniversite öğrencisi veya mezunu
    if (character.currentEdu && !character.currentEdu.auto) return;
    if (character.education.some((e) => e.startsWith('Üniversite'))) return;

    // Yılda 1 kez sınav
    if (character.lastExamAge === character.age) return;

    // YKS puan hesaplama
    const base = character.smarts * 3;
    const studyBonus = Math.min(80, character.examStudyCount * 4);
    const luck = rand(-30, 30);
    const happinessBonus = character.happiness > 70 ? 10 : 0;
    const stressPenalty = character.happiness < 30 ? 20 : 0;
    const rawScore = base + studyBonus + luck + happinessBonus - stressPenalty;
    const finalScore = clamp(rawScore, 50, 500);

    set({
      character: {
        ...character,
        lastExamScore: finalScore,
        lastExamAge: character.age,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `YKS sınavına girdin! Puanın: ${finalScore}/500 📝`,
          type: finalScore >= 150 ? 'good' : 'bad',
        },
      ],
    });
  },

  selectDepartment: (deptId: string) => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;
    if (!character.lastExamScore || character.lastExamScore < 150) return;

    // Zaten üniversite öğrencisi veya mezunu
    if (character.currentEdu && !character.currentEdu.auto) return;
    if (character.education.some((e) => e.startsWith('Üniversite'))) return;

    const dept = getDepartmentById(deptId);
    if (!dept) return;

    // Puan yeterli mi
    if (character.lastExamScore < dept.minScore) return;

    // İlk yıl ücreti ödeyebilir mi
    if (character.money < dept.annualCost) return;

    // Education nesnesi oluştur
    const uniEdu: Education = {
      name: `Üniversite — ${dept.name}`,
      cost: dept.annualCost,
      smartsReq: 0,
      smartsGain: dept.smartsGain,
      years: dept.totalYears,
      minAge: 18,
      maxAge: 35,
      auto: false,
      prereq: 'Lise',
      examRequired: false,
      examPassRate: 0,
      dropCanAge: 0,
    };

    set({
      character: {
        ...character,
        currentEdu: uniEdu,
        eduYearsLeft: dept.totalYears,
        universityDepartment: dept,
        money: character.money - dept.annualCost,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${dept.name} bölümüne yerleştin! 🎓 ${dept.totalYears} yıllık ${dept.type === 'devlet' ? 'devlet' : 'özel'} üniversite eğitimi başlıyor.`,
          type: 'milestone',
        },
      ],
    });
  },

  doAction: (actionId: string) => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;

    const activity = ACTIVITIES.find((a) => a.id === actionId);
    if (!activity) return;
    if (character.age < activity.minAge) return;
    if (character.money < activity.cost) return;

    let { health, happiness, smarts, looks, money } = character;
    money -= activity.cost;

    const newLog: LogEntry[] = [];
    const actionCounts = { ...character.actionCounts };
    actionCounts[actionId] = (actionCounts[actionId] ?? 0) + 1;

    let { travelCount, crimeCount, examStudyCount } = character;

    // Özel mantık: Yatırım
    if (actionId === 'invest') {
      if (pct(50)) {
        money += activity.cost * 2;
        newLog.push({
          age: character.age,
          text: `Yatırımın ikiye katlandı! +₺${(activity.cost * 2).toLocaleString('tr-TR')} 📈`,
          type: 'good',
        });
      } else {
        newLog.push({
          age: character.age,
          text: `Yatırımın değer kaybetti! -₺${activity.cost.toLocaleString('tr-TR')} 📉`,
          type: 'bad',
        });
      }
    }
    // Özel mantık: Piyango
    else if (actionId === 'lottery') {
      if (pct(1)) {
        const jackpot = 500_000;
        money += jackpot;
        newLog.push({
          age: character.age,
          text: `BÜYÜK İKRAMİYE! ₺${jackpot.toLocaleString('tr-TR')} kazandın! 🎰🎉`,
          type: 'good',
        });
      } else if (pct(10)) {
        const smallWin = 1000;
        money += smallWin;
        newLog.push({
          age: character.age,
          text: `Piyangoda küçük ikramiye kazandın! +₺${smallWin.toLocaleString('tr-TR')}`,
          type: 'good',
        });
      } else {
        newLog.push({
          age: character.age,
          text: 'Piyango bileti aldın ama kazanamadın.',
          type: 'event',
        });
      }
    }
    // Özel mantık: Kumar
    else if (actionId === 'gamble') {
      const roll = rand(1, 100);
      if (roll <= 10) {
        const win = activity.cost * 5;
        money += win;
        newLog.push({
          age: character.age,
          text: `Kumarda büyük vurgun yaptın! +₺${win.toLocaleString('tr-TR')} 🤑`,
          type: 'good',
        });
      } else if (roll <= 40) {
        const win = activity.cost * 2;
        money += win;
        newLog.push({
          age: character.age,
          text: `Kumarda kazandın! +₺${win.toLocaleString('tr-TR')}`,
          type: 'good',
        });
      } else {
        newLog.push({
          age: character.age,
          text: `Kumarda kaybettin! -₺${activity.cost.toLocaleString('tr-TR')} 😔`,
          type: 'bad',
        });
      }
    }
    // Özel mantık: Suç
    else if (actionId === 'crime') {
      if (pct(60)) {
        const stolen = rand(2000, 20000);
        money += stolen;
        crimeCount += 1;
        newLog.push({
          age: character.age,
          text: `Yasadışı işten ₺${stolen.toLocaleString('tr-TR')} kazandın. 🤫`,
          type: 'bad',
        });
      } else {
        money -= rand(5000, 15000);
        happiness -= 20;
        newLog.push({
          age: character.age,
          text: 'Yakalandın! Ceza ödedin ve itibarın zedelendi. 👮',
          type: 'bad',
        });
      }
    }
    // Özel mantık: Seyahat
    else if (actionId === 'travel') {
      travelCount += 1;
      if (activity.fx.health) health += activity.fx.health;
      if (activity.fx.happiness) happiness += activity.fx.happiness;
      if (activity.fx.smarts) smarts += activity.fx.smarts;
      if (activity.fx.looks) looks += activity.fx.looks;

      if (pct(10)) {
        health -= 5;
        newLog.push({
          age: character.age,
          text: '✈️ Seyahatte biraz hastalandın ama eğlenceli geçti!',
          type: 'event',
        });
      } else {
        newLog.push({
          age: character.age,
          text: '✈️ Harika bir seyahat yaptın!',
          type: 'good',
        });
      }
    }
    // Özel mantık: Ders çalış (sınav takibi)
    else if (actionId === 'study') {
      if (activity.fx.smarts) smarts += activity.fx.smarts;
      if (activity.fx.happiness) happiness += activity.fx.happiness;

      // Lise yıllarında sınav çalışma sayısını takip et
      if (character.age >= 14 && character.age <= 18) {
        examStudyCount += 1;
        newLog.push({
          age: character.age,
          text: 'Ders çalıştın. Sınava bir adım daha yakınsın! 📖',
          type: 'good',
        });
      } else {
        newLog.push({
          age: character.age,
          text: `${activity.emoji} ${activity.name} aktivitesini yaptın.`,
          type: 'good',
        });
      }
    }
    // Normal aktivite
    else {
      if (activity.fx.health) health += activity.fx.health;
      if (activity.fx.happiness) happiness += activity.fx.happiness;
      if (activity.fx.smarts) smarts += activity.fx.smarts;
      if (activity.fx.looks) looks += activity.fx.looks;
      if (activity.fx.money) money += activity.fx.money;

      newLog.push({
        age: character.age,
        text: `${activity.emoji} ${activity.name} aktivitesini yaptın.`,
        type: 'good',
      });
    }

    set({
      character: {
        ...character,
        health: clamp(health),
        happiness: clamp(happiness),
        smarts: clamp(smarts),
        looks: clamp(looks),
        money: Math.max(GAME_BALANCE.moneyMin, money),
        actionCounts,
        travelCount,
        crimeCount,
        examStudyCount,
      },
      log: [...log, ...newLog],
    });
  },

  marry: () => {
    const { character, log } = get();
    if (!character || !character.isAlive || character.isMarried || character.age < 20) return;

    const spouseGender = character.gender === 'M' ? 'F' : 'M';
    const spouseNames = spouseGender === 'M' ? MALE_NAMES : FEMALE_NAMES;
    const spouseAge = Math.max(18, character.age + rand(-5, 5));
    const spouse: Relationship = {
      id: genRelationId(),
      name: pick(spouseNames),
      surname: pick(SURNAMES),
      type: 'spouse',
      age: spouseAge,
      closeness: rand(70, 95),
      isAlive: true,
    };

    set({
      character: {
        ...character,
        relationships: [...character.relationships, spouse],
        isMarried: true,
        marriageYear: character.age,
        money: character.money - 15000,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${spouse.name} ${spouse.surname} ile evlendin! 💍 Mutluluklar!`,
          type: 'good',
        },
      ],
    });
  },

  divorce: () => {
    const { character, log } = get();
    if (!character || !character.isAlive || !character.isMarried) return;

    const relationships = character.relationships.map((r) =>
      r.type === 'spouse' && r.isAlive ? { ...r, type: 'friend' as const, closeness: 20 } : r,
    );

    set({
      character: {
        ...character,
        relationships,
        isMarried: false,
        marriageYear: null,
        divorceCount: character.divorceCount + 1,
        happiness: clamp(character.happiness - 20),
        money: character.money - 20000,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: 'Boşandın. Zor bir süreç oldu. 💔',
          type: 'bad',
        },
      ],
    });
  },

  haveChild: () => {
    const { character, log } = get();
    if (!character || !character.isAlive || !character.isMarried || character.age < 22) return;

    const childGender = pct(50) ? 'M' : 'F';
    const childNames = childGender === 'M' ? MALE_NAMES : FEMALE_NAMES;
    const child: Relationship = {
      id: genRelationId(),
      name: pick(childNames),
      surname: character.surname,
      type: 'child',
      age: 0,
      closeness: rand(80, 100),
      isAlive: true,
    };

    set({
      character: {
        ...character,
        relationships: [...character.relationships, child],
        childCount: character.childCount + 1,
        happiness: clamp(character.happiness + 15),
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${child.name} adında ${childGender === 'M' ? 'bir oğlun' : 'bir kızın'} dünyaya geldi! 👶`,
          type: 'good',
        },
      ],
    });
  },

  interactRelation: (relationId: string, type: 'spend_time' | 'argue') => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;

    const relationships = character.relationships.map((r) => {
      if (r.id !== relationId || !r.isAlive) return r;

      if (type === 'spend_time') {
        return { ...r, closeness: clamp(r.closeness + rand(5, 15)) };
      } else {
        return { ...r, closeness: clamp(r.closeness - rand(10, 25)) };
      }
    });

    const relation = character.relationships.find((r) => r.id === relationId);
    if (!relation) return;

    const logText =
      type === 'spend_time'
        ? `${relation.name} ile güzel vakit geçirdin.`
        : `${relation.name} ile tartıştın.`;

    set({
      character: {
        ...character,
        relationships,
        happiness: clamp(
          character.happiness + (type === 'spend_time' ? 5 : -8),
        ),
      },
      log: [
        ...log,
        {
          age: character.age,
          text: logText,
          type: type === 'spend_time' ? 'good' : 'bad',
        },
      ],
    });
  },

  newGame: () => {
    set({
      screen: 'menu',
      character: null,
      log: [],
      activeTab: 'life',
    });
  },

  setActiveTab: (tab: TabId) => set({ activeTab: tab }),
}));

// İzole yeniden render için seçiciler
export const useCharacter = () => useGameStore((s) => s.character);
export const useLog = () => useGameStore((s) => s.log);
export const useScreen = () => useGameStore((s) => s.screen);
export const useActiveTab = () => useGameStore((s) => s.activeTab);
