import { useCallback } from 'react';

import type { SoundEvent } from '@/shared/constants/sounds';

/**
 * Ses efekti hook iskeleti.
 * Ses dosyaları eklendiğinde expo-av ile bağlanacak.
 */
export const useSoundEffect = () => {
  const play = useCallback((_event: SoundEvent) => {
    // TODO: expo-av ile ses çalma implementasyonu
  }, []);

  return { play };
};
