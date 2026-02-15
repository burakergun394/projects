export type Gender = 'M' | 'F';
export type Screen = 'menu' | 'create' | 'game' | 'dead';
export type TabId = 'life' | 'job' | 'edu' | 'actions' | 'relations';
export type LogType = 'birth' | 'good' | 'bad' | 'milestone' | 'death' | 'event';

export interface ZodiacSign {
  name: string;
  emoji: string;
  m: number;
  d: number;
}

export interface Relationship {
  id: string;
  name: string;
  surname: string;
  type: 'spouse' | 'child' | 'friend' | 'parent' | 'sibling';
  age: number;
  closeness: number;
  isAlive: boolean;
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
  relationships: Relationship[];
  isMarried: boolean;
  childCount: number;
  achievements: string[];
  actionCounts: Record<string, number>;
  jobHistory: string[];
  travelCount: number;
  crimeCount: number;
  lowestHealth: number;
  highestHealth: number;
  divorceCount: number;
  marriageYear: number | null;
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
  category: 'entry' | 'skilled' | 'professional' | 'executive';
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

export interface Achievement {
  id: string;
  title: string;
  description: string;
  emoji: string;
  condition: (character: Character, log: LogEntry[]) => boolean;
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
  marry: () => void;
  divorce: () => void;
  haveChild: () => void;
  interactRelation: (relationId: string, type: 'spend_time' | 'argue') => void;
}
