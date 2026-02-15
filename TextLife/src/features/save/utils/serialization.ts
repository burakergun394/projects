import { APP_CONFIG } from '@/shared/constants';

import type { SaveData } from '../types';
import type { Character, LogEntry } from '@/features/game/types';

export const serializeSave = (
  character: Character,
  log: LogEntry[],
): string => {
  const data: SaveData = {
    version: APP_CONFIG.version,
    timestamp: Date.now(),
    character,
    log,
  };
  return JSON.stringify(data);
};

export const deserializeSave = (raw: string): SaveData | null => {
  try {
    const parsed = JSON.parse(raw) as SaveData;
    if (!parsed.character || !parsed.log || !parsed.version) {
      return null;
    }
    return parsed;
  } catch {
    return null;
  }
};
