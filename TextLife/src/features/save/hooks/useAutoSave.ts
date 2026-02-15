import { useEffect, useRef } from 'react';

import { useGameStore } from '@/features/game/stores/gameStore';

import { useSaveGame } from './useSaveGame';

/** Her yaş ilerlemesinde otomatik kaydet */
export const useAutoSave = () => {
  const { save } = useSaveGame();
  const age = useGameStore((s) => s.character?.age);
  const isAlive = useGameStore((s) => s.character?.isAlive);
  const prevAge = useRef(age);

  useEffect(() => {
    if (age !== undefined && age !== prevAge.current && isAlive) {
      save(0);
      prevAge.current = age;
    }
  }, [age, isAlive, save]);
};
