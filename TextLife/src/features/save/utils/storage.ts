import { createMMKV } from 'react-native-mmkv';

export const storage = createMMKV({ id: 'textlife-save' });

export const SAVE_KEYS = {
  slot: (id: number) => `save-slot-${id}`,
  lastSlot: 'last-save-slot',
} as const;
