import type { Character, LogEntry } from '@/features/game/types';

export interface SaveData {
  version: number;
  timestamp: number;
  character: Character;
  log: LogEntry[];
}

export interface SaveSlot {
  id: number;
  data: SaveData | null;
  isEmpty: boolean;
}
