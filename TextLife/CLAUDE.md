# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TextLife is a Turkish life simulation game (BitLife-style) built as a mobile app with **TypeScript** and **React Native (Expo)**. All source files use `.ts` / `.tsx` extensions — no `.js` / `.jsx` allowed.

**Game concept:** Player creates a character, advances year by year, encounters random life events, manages 4 stats (health, happiness, smarts, looks), earns money through jobs, pursues education, performs activities, builds relationships, and earns achievements — all text-based.

## Build & Run

```bash
npx expo start          # Start dev server (Expo Go compatible)
npx tsc --noEmit        # Type check
```

**Note:** The app runs in Expo Go — no native build required. Storage uses AsyncStorage (not MMKV) for Expo Go compatibility.

## Tech Stack

| Concern | Library | Notes |
|---|---|---|
| Framework | Expo SDK 54 (Managed Workflow) | |
| Routing | Expo Router v4 (file-based) | 4 routes: index, create, game, death |
| State | Zustand | Single `gameStore` — all game state |
| Styling | NativeWind v4 (Tailwind for RN) | No `StyleSheet.create` |
| Storage | AsyncStorage | With in-memory Map cache for sync-like API |
| Animations | React Native Reanimated 3 + Moti | Stat bars, screen transitions |
| Fonts | Outfit (UI) + JetBrains Mono (data) | Via @expo-google-fonts |

## Architecture

Feature-Based (Domain-Driven) Clean Architecture:

```
src/
├── app/                    # Expo Router — minimal routes
│   ├── _layout.tsx         # Root layout (fonts, providers)
│   ├── index.tsx           # Menu screen
│   ├── create.tsx          # Character creation (gender select)
│   ├── game.tsx            # Main game screen (5 tabs)
│   └── death.tsx           # Death summary screen
├── features/
│   ├── game/
│   │   ├── components/     # GameHeader, TabBar, LogList, StatBars, AgeButton,
│   │   │                   # ActionGrid, JobList, EduList, RelationList
│   │   ├── hooks/          # useGameEngine
│   │   ├── stores/         # gameStore.ts (Zustand — single source of truth)
│   │   ├── data/           # events, jobs, education, activities, relationships,
│   │   │                   # achievements, names, zodiac
│   │   ├── types/          # Character, LogEntry, Job, Education, GameEvent,
│   │   │                   # Activity, Relationship, Achievement
│   │   └── utils/          # rand, pick, clamp, pct
│   ├── character/
│   │   ├── components/     # GenderSelect, CharacterCard
│   │   └── types/
│   └── save/
│       ├── hooks/          # useSaveGame, useLoadGame, useAutoSave
│       ├── utils/          # storage.ts (AsyncStorage wrapper), serialization
│       └── types/
├── shared/
│   ├── components/ui/      # Button, Card, ProgressBar, Badge
│   ├── hooks/              # useAppState, useBackHandler, useSoundEffect
│   ├── theme/              # tokens.ts (colors, spacing, radius, shadows)
│   ├── constants/          # APP_CONFIG, GAME_BALANCE, DEATH_REASONS, SOUND_EVENTS
│   └── utils/              # formatMoney, formatAge
└── assets/fonts/           # Outfit-*.ttf, JetBrainsMono-*.ttf
```

### Key Architectural Rules

- **Feature isolation**: Features export only through barrel `index.ts`. Never import directly into feature internals from outside.
- **All game state in Zustand**: Single `gameStore` holds character, log, screen, activeTab. No prop drilling.
- **Isolated selectors**: Each component subscribes to only the state it needs via Zustand selectors (`useCharacter`, `useLog`, `useActiveTab`, etc.).
- **Data files are pure**: Event pools, job lists, education data — plain typed arrays, no logic.
- **Composition over inheritance**: Break screens into small composed components.

## Game State Machine

```
MENU → CREATE → GAME (5 tabs: Life, Job, Edu, Actions, Relations) → DEATH → MENU
```

### Zustand Game Store — Key Actions

| Action | Description |
|---|---|
| `createCharacter(gender)` | Generate character with random stats, parents, optional sibling |
| `ageUp()` | Core loop: age++, health decay, salary, edu progress, random events, relationship events, friend generation, achievement check, death check |
| `getJob(job)` / `quitJob()` | Employment management (tracks jobHistory) |
| `startEdu(edu)` | Start higher education (exam system, prereqs, diploma check) |
| `dropOut()` | Drop out of current education (if allowed) |
| `doAction(actionId)` | Activities — special mechanics for gamble, crime, travel |
| `marry()` / `divorce()` / `haveChild()` | Relationship lifecycle |
| `interactRelation(id, type)` | spend_time / argue — affects closeness |
| `newGame()` | Full reset |

## Game Content

| Data | Count | File |
|---|---|---|
| Events (5 age pools) | 168 total (24/36/36/45/27) | `data/events.ts` |
| Jobs (4 categories) | 24 | `data/jobs.ts` |
| Activities | 14 | `data/activities.ts` |
| Achievements | 20 | `data/achievements.ts` |
| Relationship event pools | 36 (14 marriage + 10 friendship + 12 family) | `data/relationships.ts` |
| Education tiers | 7 (İlkokul→Ortaokul→Lise + Üni Devlet/Özel→YL→Doktora) | `data/education.ts` |
| Names/Cities | 24M + 24F + 20 surnames + 16 cities | `data/names.ts` |

## Save/Load System

Uses AsyncStorage with an in-memory `Map` cache for synchronous reads. Auto-saves on every `ageUp()`.

```typescript
// src/features/save/utils/storage.ts — sync-like wrapper
const cache = new Map<string, string>();
export const storage = {
  set(key, value)    // cache + async persist
  getString(key)     // sync from cache
  contains(key)      // sync from cache
  remove(key)        // cache + async remove
};
```

## Core Types Reference

```typescript
type Gender = 'M' | 'F';
type Screen = 'menu' | 'create' | 'game' | 'dead';
type TabId = 'life' | 'job' | 'edu' | 'actions' | 'relations';
type LogType = 'birth' | 'good' | 'bad' | 'milestone' | 'death' | 'event';

interface Character {
  name, surname, gender, city, zodiac, birthYear, age,
  health, happiness, smarts, looks,    // 0-100
  money,                                // can go negative (cap -100K)
  job: Job | null,
  education: string[],
  currentEdu: Education | null,
  eduYearsLeft: number,
  isAlive, deathAge, deathReason,
  // Relationships & social
  relationships: Relationship[],
  isMarried: boolean,
  childCount: number,
  marriageYear: number | null,
  divorceCount: number,
  // Tracking & achievements
  achievements: string[],
  actionCounts: Record<string, number>,
  jobHistory: string[],
  travelCount: number,
  crimeCount: number,
  lowestHealth: number,
  highestHealth: number,
}

interface Job {
  title: string;
  salary: number;
  req: number;                          // minimum smarts
  category: 'entry' | 'skilled' | 'professional' | 'executive';
}

interface Education {
  name: string;
  cost: number;
  smartsReq: number;
  smartsGain: number;
  years: number;
  minAge: number;
  maxAge: number;
  auto: boolean;                        // true = compulsory (İlkokul/Ortaokul/Lise)
  prereq: string | null;
  examRequired: boolean;
  examPassRate: number;
  dropCanAge: number | null;            // null = cannot drop
}

interface Relationship {
  id: string;
  name: string;
  surname: string;
  type: 'spouse' | 'child' | 'friend' | 'parent' | 'sibling';
  age: number;
  closeness: number;                    // 0-100
  isAlive: boolean;
}

interface Achievement {
  id: string;
  title: string;
  description: string;
  emoji: string;
  condition: (character: Character, log: LogEntry[]) => boolean;
}
```

## Conventions

- **Language**: All user-facing strings in Turkish. Code comments in Turkish where contextual.
- **TypeScript strict mode**: No `any`. Use proper generics or `unknown`.
- **Named exports** for components/hooks. Default exports only for route screens in `app/`.
- **NativeWind className** for styling — no `StyleSheet.create`.
- **React.memo** on list item components (LogEntryItem, RelationCard, ActionCard).
- **useCallback/useMemo** for all derived data and callback props.

### Naming

| Type | Convention | Example |
|---|---|---|
| Components | PascalCase | `StatBars.tsx` |
| Hooks | camelCase, `use` prefix | `useGameEngine.ts` |
| Stores | camelCase, `Store` suffix | `gameStore.ts` |
| Types/Interfaces | PascalCase | `Character` |
| Constants | UPPER_SNAKE_CASE | `DEATH_REASONS` |
| Data files | camelCase | `events.ts` |
| Route files | kebab-case | `index.tsx` |

### Import Order

1. React & React Native
2. Third-party libraries (expo, zustand, reanimated, moti)
3. Shared modules (`@/shared/...`)
4. Feature modules (`@/features/...`)
5. Relative imports (same feature)
6. Type-only imports (`import type { ... }`)

### Git

- Conventional Commits: `feat:`, `fix:`, `refactor:`, `chore:`, `docs:`
