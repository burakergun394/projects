export const SOUND_EVENTS = {
  ageUp: 'age-up.mp3',
  goodEvent: 'positive.mp3',
  badEvent: 'negative.mp3',
  death: 'death.mp3',
  achievement: 'achievement.mp3',
  buttonPress: 'tap.mp3',
} as const;

export type SoundEvent = keyof typeof SOUND_EVENTS;
