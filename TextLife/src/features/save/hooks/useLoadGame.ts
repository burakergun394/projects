import { useCallback } from 'react';

import { useGameStore } from '@/features/game/stores/gameStore';

import { storage, SAVE_KEYS } from '../utils/storage';
import { deserializeSave } from '../utils/serialization';

export const useLoadGame = () => {
  const hasSave = useCallback((slotId = 0): boolean => {
    try {
      return storage.contains(SAVE_KEYS.slot(slotId));
    } catch {
      return false;
    }
  }, []);

  const load = useCallback((slotId = 0): boolean => {
    try {
      const raw = storage.getString(SAVE_KEYS.slot(slotId));
      if (!raw) return false;

      const data = deserializeSave(raw);
      if (!data) return false;

      useGameStore.setState({
        character: data.character,
        log: data.log,
        screen: 'game',
        activeTab: 'life',
      });
      return true;
    } catch {
      return false;
    }
  }, []);

  const deleteSave = useCallback((slotId = 0) => {
    try {
      storage.remove(SAVE_KEYS.slot(slotId));
    } catch {
      // Silme hatası — sessizce geç
    }
  }, []);

  return { hasSave, load, deleteSave };
};
