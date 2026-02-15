import { create } from 'zustand';

import { GAME_BALANCE, DEATH_REASONS } from '@/shared/constants';

import { MALE_NAMES, FEMALE_NAMES, SURNAMES, CITIES } from '../data/names';
import { getZodiac } from '../data/zodiac';
import { getEventsForAge } from '../data/events';
import { MARRIAGE_EVENTS, FRIENDSHIP_EVENTS, FAMILY_EVENTS } from '../data/relationships';
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
  Relationship,
  GameStore,
} from '../types';

let nextRelationId = 1;
const genRelationId = () => `rel-${nextRelationId++}`;

const createInitialCharacter = (gender: Gender): Character => {
  const names = gender === 'M' ? MALE_NAMES : FEMALE_NAMES;
  const name = pick(names);
  const surname = pick(SURNAMES);
  const city = pick(CITIES);
  const birthMonth = rand(1, 12);
  const birthDay = rand(1, 28);
  const zodiac = getZodiac(birthMonth, birthDay);
  const currentYear = new Date().getFullYear();

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
    relationships: initialRelations,
    isMarried: false,
    childCount: 0,
    achievements: [],
    actionCounts: {},
    jobHistory: [],
    travelCount: 0,
    crimeCount: 0,
    lowestHealth: rand(40, 80),
    highestHealth: rand(40, 80),
    divorceCount: 0,
    marriageYear: null,
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
    let {
      age, job, currentEdu, eduYearsLeft, education,
      relationships, isMarried, childCount, achievements,
      actionCounts, jobHistory, travelCount, crimeCount,
      lowestHealth, highestHealth, divorceCount, marriageYear,
    } = character;
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
      relationships = [
        ...relationships,
        {
          id: genRelationId(),
          name: pick(friendNames),
          surname: pick(SURNAMES),
          type: 'friend',
          age: age + rand(-5, 5),
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

    const updatedCharacter: Character = {
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
      relationships,
      isMarried,
      childCount,
      achievements,
      actionCounts,
      jobHistory,
      travelCount,
      crimeCount,
      lowestHealth,
      highestHealth,
      divorceCount,
      marriageYear,
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

  getJob: (job: Job) => {
    const { character, log } = get();
    if (!character) return;

    const jobHistory = character.jobHistory.includes(job.title)
      ? character.jobHistory
      : [...character.jobHistory, job.title];

    set({
      character: { ...character, job, jobHistory },
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
    const actionCounts = { ...character.actionCounts };
    actionCounts[actionId] = (actionCounts[actionId] ?? 0) + 1;

    let { travelCount, crimeCount } = character;

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
        // Büyük kazanç
        const win = activity.cost * 5;
        money += win;
        newLog.push({
          age: character.age,
          text: `Kumarda büyük vurgun yaptın! +₺${win.toLocaleString('tr-TR')} 🤑`,
          type: 'good',
        });
      } else if (roll <= 40) {
        // Küçük kazanç
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

      // Küçük sağlık riski (%10)
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
      },
      log: [...log, ...newLog],
    });
  },

  marry: () => {
    const { character, log } = get();
    if (!character || character.isMarried || character.age < 20) return;

    const spouseGender = character.gender === 'M' ? 'F' : 'M';
    const spouseNames = spouseGender === 'M' ? MALE_NAMES : FEMALE_NAMES;
    const spouse: Relationship = {
      id: genRelationId(),
      name: pick(spouseNames),
      surname: pick(SURNAMES),
      type: 'spouse',
      age: character.age + rand(-5, 5),
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
    if (!character || !character.isMarried) return;

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
    if (!character || !character.isMarried || character.age < 22) return;

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
    if (!character) return;

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
