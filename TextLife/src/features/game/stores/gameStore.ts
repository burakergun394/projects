import { create } from 'zustand';

import { GAME_BALANCE, DEATH_REASONS } from '@/shared/constants';

import { MALE_NAMES, FEMALE_NAMES, SURNAMES, CITIES } from '../data/names';
import { getZodiac } from '../data/zodiac';
import { getEventsForAge } from '../data/events';
import { ACTIVITIES } from '../data/activities';
import { rand, pick, clamp, pct } from '../utils';

import type {
  Gender,
  Screen,
  TabId,
  Character,
  LogEntry,
  Job,
  Education,
  GameStore,
} from '../types';

const createInitialCharacter = (gender: Gender): Character => {
  const names = gender === 'M' ? MALE_NAMES : FEMALE_NAMES;
  const name = pick(names);
  const surname = pick(SURNAMES);
  const city = pick(CITIES);
  const birthMonth = rand(1, 12);
  const birthDay = rand(1, 28);
  const zodiac = getZodiac(birthMonth, birthDay);
  const currentYear = new Date().getFullYear();

  return {
    name,
    surname,
    gender,
    city,
    zodiac,
    birthYear: currentYear,
    age: 0,
    health: rand(40, 80),
    happiness: rand(50, 90),
    smarts: rand(20, 60),
    looks: rand(30, 80),
    money: 0,
    job: null,
    education: [],
    currentEdu: null,
    eduYearsLeft: 0,
    isAlive: true,
    deathAge: null,
    deathReason: null,
  };
};

const AGE_MILESTONES: Record<number, string> = {
  1: 'İlk yaş günün kutlu olsun! 🎂',
  4: 'Anaokulu çağına geldin!',
  6: 'İlkokula başladın! 📝',
  10: 'Çift haneli yaşlara hoş geldin!',
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
    let { age, job, currentEdu, eduYearsLeft, education } = character;
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

    // Maaş geliri
    if (job) {
      money += job.salary;
      newLog.push({
        age,
        text: `${job.title} olarak çalışıyorsun. Maaş: ₺${job.salary.toLocaleString('tr-TR')}`,
        type: 'event',
      });
    }

    // Eğitim ilerlemesi
    if (currentEdu && eduYearsLeft > 0) {
      eduYearsLeft -= 1;
      smarts += Math.floor(currentEdu.smartsGain / currentEdu.years);

      if (eduYearsLeft === 0) {
        education = [...education, currentEdu.name];
        newLog.push({
          age,
          text: `${currentEdu.name} eğitimini tamamladın! 🎓`,
          type: 'milestone',
        });
        currentEdu = null;
      } else {
        newLog.push({
          age,
          text: `${currentEdu.name} eğitimine devam ediyorsun. (${eduYearsLeft} yıl kaldı)`,
          type: 'event',
        });
      }
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

    set({
      character: {
        ...character,
        age,
        health,
        happiness,
        smarts,
        looks,
        money,
        job,
        education,
        currentEdu,
        eduYearsLeft,
        isAlive,
        deathAge,
        deathReason,
      },
      log: [...log, ...newLog],
      screen: isAlive ? 'game' : 'dead',
    });
  },

  getJob: (job: Job) => {
    const { character, log } = get();
    if (!character) return;

    set({
      character: { ...character, job },
      log: [
        ...log,
        {
          age: character.age,
          text: `${job.title} olarak işe başladın! Maaş: ₺${job.salary.toLocaleString('tr-TR')}`,
          type: 'good',
        },
      ],
    });
  },

  quitJob: () => {
    const { character, log } = get();
    if (!character || !character.job) return;

    const oldJob = character.job.title;
    set({
      character: { ...character, job: null },
      log: [
        ...log,
        {
          age: character.age,
          text: `${oldJob} işinden ayrıldın.`,
          type: 'event',
        },
      ],
    });
  },

  startEdu: (edu: Education) => {
    const { character, log } = get();
    if (!character) return;
    if (character.currentEdu) return;
    if (character.smarts < edu.smartsReq) return;
    if (character.age < edu.minAge) return;
    if (character.education.includes(edu.name)) return;

    set({
      character: {
        ...character,
        currentEdu: edu,
        eduYearsLeft: edu.years,
        money: character.money - edu.cost,
      },
      log: [
        ...log,
        {
          age: character.age,
          text: `${edu.name} eğitimine başladın! (${edu.years} yıl, ${edu.cost > 0 ? `₺${edu.cost.toLocaleString('tr-TR')}` : 'Ücretsiz'})`,
          type: 'good',
        },
      ],
    });
  },

  doAction: (actionId: string) => {
    const { character, log } = get();
    if (!character || !character.isAlive) return;

    const activity = ACTIVITIES.find((a) => a.id === actionId);
    if (!activity) return;
    if (character.money < activity.cost) return;

    let { health, happiness, smarts, looks, money } = character;
    money -= activity.cost;

    const newLog: LogEntry[] = [];

    // Özel mantık: Yatırım (%50 ikiye katla, %50 kaybet)
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
    // Özel mantık: Piyango (%1 büyük ikramiye)
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
      },
      log: [...log, ...newLog],
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
