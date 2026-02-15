export type Gender = 'M' | 'F';
export type Screen = 'menu' | 'create' | 'game' | 'dead';
export type TabId = 'life' | 'job' | 'edu' | 'actions' | 'relations';
export type LogType = 'birth' | 'good' | 'bad' | 'milestone' | 'death' | 'event';
export type EducationLevel = 'none' | 'ilkokul' | 'ortaokul' | 'lise' | 'universite' | 'yuksek_lisans' | 'doktora';

export type Faculty =
  | 'Tıp' | 'Hukuk' | 'Mühendislik' | 'İşletme'
  | 'Fen-Edebiyat' | 'Eğitim' | 'İletişim'
  | 'Mimarlık' | 'Eczacılık' | 'Güzel Sanatlar'
  | 'Sağlık Bilimleri' | 'Havacılık';

export interface Department {
  id: string;
  name: string;
  faculty: Faculty;
  minScore: number;
  type: 'devlet' | 'ozel';
  annualCost: number;
  totalYears: number;
  smartsGain: number;
  prestige: number;
  unlockedCareers: string[];
  description: string;
}

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

export interface Job {
  id: string;
  title: string;
  sector: 'entry' | 'trade' | 'public' | 'private' | 'professional' | 'executive';
  baseSalary: number;
  minSmarts: number;
  minEducation: EducationLevel;
  minAge: number;
  maxAge: number;
  promotionChain: string[];
  experienceYearsForPromo: number;
  fireChance: number;
  respectGain: number;
}

export interface JobHistoryEntry {
  jobTitle: string;
  sector: string;
  startAge: number;
  endAge: number;
  endReason: 'quit' | 'fired' | 'promoted' | 'retired';
  finalSalary: number;
}

export interface CareerState {
  currentJob: Job | null;
  currentSalary: number;
  yearsInCurrentJob: number;
  totalWorkYears: number;
  performanceScore: number;
  jobHistory: JobHistoryEntry[];
  isRetired: boolean;
  pension: number;
  lifetimeEarnings: number;
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
  career: CareerState;
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
  travelCount: number;
  crimeCount: number;
  lowestHealth: number;
  highestHealth: number;
  divorceCount: number;
  marriageYear: number | null;
  // YKS sınav ve bölüm sistemi
  examStudyCount: number;
  lastExamScore: number | null;
  lastExamAge: number | null;
  universityDepartment: Department | null;
}

export interface LogEntry {
  age: number;
  text: string;
  type: LogType;
}

export interface Education {
  name: string;
  cost: number;
  smartsReq: number;
  smartsGain: number;
  years: number;
  minAge: number;
  maxAge: number;
  auto: boolean;
  prereq: string | null;
  examRequired: boolean;
  examPassRate: number;
  dropCanAge: number | null;
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
  minAge: number;
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
  applyForJob: (jobId: string) => void;
  quitJob: () => void;
  retire: () => void;
  startEdu: (edu: Education) => void;
  dropOut: () => void;
  takeExam: () => void;
  selectDepartment: (deptId: string) => void;
  doAction: (actionId: string) => void;
  newGame: () => void;
  setActiveTab: (tab: TabId) => void;
  marry: () => void;
  divorce: () => void;
  haveChild: () => void;
  interactRelation: (relationId: string, type: 'spend_time' | 'argue') => void;
}
