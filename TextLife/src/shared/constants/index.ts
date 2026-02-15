export const APP_CONFIG = {
  name: 'TextLife',
  year: 2026,
  saveSlots: 3,
  version: 1,
} as const;

export const GAME_BALANCE = {
  statMin: 0,
  statMax: 100,
  moneyMin: -100_000,
  baseHealthDecay: 2,
  elderHealthDecay: 5,
  elderAge: 65,
  naturalDeathMinAge: 60,
  naturalDeathBaseChance: 2,
  naturalDeathChancePerYear: 1.5,
  eventChance: 60,
} as const;

export const DEATH_REASONS = [
  'Yaşlılık',
  'Kalp krizi',
  'Kaza',
  'Hastalık',
  'Doğal nedenler',
] as const;

export { SOUND_EVENTS } from './sounds';
export type { SoundEvent } from './sounds';
