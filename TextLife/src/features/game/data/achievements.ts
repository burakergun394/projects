import type { Achievement } from '../types';

export const ACHIEVEMENTS: readonly Achievement[] = [
  {
    id: 'first_job',
    title: 'İlk Maaş',
    description: 'İlk işine başla',
    emoji: '💼',
    condition: (character) => character.jobHistory.length > 0,
  },
  {
    id: 'millionaire',
    title: 'Milyoner',
    description: '₺1.000.000 biriktir',
    emoji: '💰',
    condition: (character) => character.money >= 1_000_000,
  },
  {
    id: 'scholar',
    title: 'Akademisyen',
    description: 'Doktora tamamla',
    emoji: '🎓',
    condition: (character) => character.education.includes('Doktora'),
  },
  {
    id: 'centenarian',
    title: 'Yüzyıllık Çınar',
    description: '100 yaşına ulaş',
    emoji: '🎂',
    condition: (character) => character.age >= 100,
  },
  {
    id: 'heartbreaker',
    title: 'Kalp Kıran',
    description: '3 kez boşan',
    emoji: '💔',
    condition: (character) => character.divorceCount >= 3,
  },
  {
    id: 'athlete',
    title: 'Sporcu',
    description: 'Sağlık 95+ ulaş',
    emoji: '💪',
    condition: (character) => character.health >= 95,
  },
  {
    id: 'genius',
    title: 'Dahi',
    description: 'Zeka 95+ ulaş',
    emoji: '🧠',
    condition: (character) => character.smarts >= 95,
  },
  {
    id: 'lucky',
    title: 'Şanslı',
    description: 'Piyango kazan',
    emoji: '🍀',
    condition: (_character, log) =>
      log.some((l) => l.text.includes('BÜYÜK İKRAMİYE')),
  },
  {
    id: 'ceo',
    title: 'Patron',
    description: 'Müdür ol',
    emoji: '👔',
    condition: (character) => character.job?.title === 'Müdür',
  },
  {
    id: 'family_person',
    title: 'Aile İnsanı',
    description: '3+ çocuk sahibi ol',
    emoji: '👨‍👩‍👧‍👦',
    condition: (character) => character.childCount >= 3,
  },
  {
    id: 'traveler',
    title: 'Gezgin',
    description: '5 seyahat et',
    emoji: '✈️',
    condition: (character) => character.travelCount >= 5,
  },
  {
    id: 'criminal',
    title: 'Suç Baronu',
    description: '5 suç işle',
    emoji: '🤫',
    condition: (character) => character.crimeCount >= 5,
  },
  {
    id: 'healthy',
    title: 'Sağlık Gurusu',
    description: '50 kez spor yap',
    emoji: '🏋️',
    condition: (character) => (character.actionCounts['gym'] ?? 0) >= 50,
  },
  {
    id: 'poor',
    title: 'Beş Parasız',
    description: '₺-50.000 borca düş',
    emoji: '📉',
    condition: (character) => character.money <= -50_000,
  },
  {
    id: 'long_marriage',
    title: 'Ömürlük',
    description: '25 yıl evli kal',
    emoji: '💍',
    condition: (character) =>
      character.isMarried &&
      character.marriageYear !== null &&
      character.age - character.marriageYear >= 25,
  },
  {
    id: 'dropout',
    title: 'Okul Terk',
    description: 'Hiç eğitim almadan 30 yaşına gel',
    emoji: '🚫',
    condition: (character) =>
      character.age >= 30 && character.education.length === 0,
  },
  {
    id: 'comeback',
    title: 'Geri Dönüş',
    description: 'Sağlık 10 altına düş sonra 80+ çık',
    emoji: '🔥',
    condition: (character) =>
      character.lowestHealth < 10 && character.health >= 80,
  },
  {
    id: 'peaceful',
    title: 'Huzurlu Son',
    description: 'Tüm statlar 70+ iken öl',
    emoji: '🕊️',
    condition: (character) =>
      !character.isAlive &&
      character.happiness >= 70 &&
      character.smarts >= 70 &&
      character.looks >= 70,
  },
  {
    id: 'young_death',
    title: 'Erken Veda',
    description: '30 yaş altında öl',
    emoji: '😢',
    condition: (character) =>
      !character.isAlive && (character.deathAge ?? 999) < 30,
  },
  {
    id: 'jack_of_all',
    title: 'Her İşin Adamı',
    description: '5 farklı işte çalış',
    emoji: '🔄',
    condition: (character) => character.jobHistory.length >= 5,
  },
] as const;
