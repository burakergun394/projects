import { useCallback } from 'react';

import { useGameStore } from '@/features/game/stores/gameStore';

import { storage, SAVE_KEYS } from '../utils/storage';
import { serializeSave } from '../utils/serialization';

export const useSaveGame = () => {
  const save = useCallback((slotId = 0) => {
    const { character, log } = useGameStore.getState();
    if (!character) return false;

    try {
      const data = serializeSave(character, log);
      storage.set(SAVE_KEYS.slot(slotId), data);
      storage.set(SAVE_KEYS.lastSlot, slotId.toString());
      return true;
    } catch {
      return false;
    }
  }, []);

  return { save };
};
