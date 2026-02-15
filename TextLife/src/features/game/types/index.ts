export type Gender = 'M' | 'F';
export type Screen = 'menu' | 'create' | 'game' | 'dead';
export type TabId = 'life' | 'job' | 'edu' | 'actions';
export type LogType = 'birth' | 'good' | 'bad' | 'milestone' | 'death' | 'event';

export interface ZodiacSign {
  name: string;
  emoji: string;
  m: number;
  d: number;
}

export interface Character {
  name: string;
  surname: string;
  gender: Gender;
  city: string;
  zodiac: ZodiacSign;
  birthYear: number;
  age: number;
  health: number;
  happiness: number;
  smarts: number;
  looks: number;
  money: number;
  job: Job | null;
  education: string[];
  currentEdu: Education | null;
  eduYearsLeft: number;
  isAlive: boolean;
  deathAge: number | null;
  deathReason: string | null;
}

export interface LogEntry {
  age: number;
  text: string;
  type: LogType;
}

export interface Job {
  title: string;
  salary: number;
  req: number;
}

export interface Education {
  name: string;
  cost: number;
  smartsReq: number;
  smartsGain: number;
  years: number;
  minAge: number;
}

export interface GameEvent {
  t: string;
  fx: {
    health?: number;
    happiness?: number;
    smarts?: number;
    looks?: number;
    money?: number;
  };
}

export interface Activity {
  id: string;
  name: string;
  emoji: string;
  cost: number;
  fx: {
    health?: number;
    happiness?: number;
    smarts?: number;
    looks?: number;
    money?: number;
  };
  description: string;
}

export interface GameStore {
  // State
  screen: Screen;
  character: Character | null;
  log: LogEntry[];
  activeTab: TabId;

  // Actions
  setScreen: (screen: Screen) => void;
  createCharacter: (gender: Gender) => void;
  ageUp: () => void;
  getJob: (job: Job) => void;
  quitJob: () => void;
  startEdu: (edu: Education) => void;
  doAction: (actionId: string) => void;
  newGame: () => void;
  setActiveTab: (tab: TabId) => void;
}
