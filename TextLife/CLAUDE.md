# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TextLife is a Turkish life simulation game (BitLife-style) built as a mobile app with **TypeScript** and **React Native (Expo)**. It lives in a subdirectory within a monorepo at `projects/`. All source files use `.ts` / `.tsx` extensions — no `.js` / `.jsx` allowed.

**Game concept:** Player creates a character, advances year by year, encounters random life events, manages 4 stats (health, happiness, smarts, looks), earns money through jobs, pursues education, and performs activities — all text-based.

## Tech Stack

| Concern | Library | Notes |
|---|---|---|
| Framework | Expo SDK 52+ (Managed Workflow) | |
| Routing | Expo Router v4 (file-based) | Minimal routes — see Screen Flow |
| State | Zustand | Single source of truth for game state |
| Styling | NativeWind v4 (Tailwind for RN) | No `StyleSheet.create` |
| Storage | MMKV | Save/load game, settings, preferences |
| Animations | React Native Reanimated 3 + Moti | Stat bars, screen transitions, death effects |
| Testing | Jest + React Native Testing Library | |
| Fonts | Outfit (UI) + JetBrains Mono (data) | Via expo-font |

### Removed from Original Plan (unnecessary for this project)

| Library | Reason |
|---|---|
| TanStack React Query | No API calls — game is fully offline/local |
| Axios | No network requests needed |
| React Hook Form + Zod | No forms — only button interactions |
| expo-secure-store | No auth tokens or sensitive data |
| i18next + expo-localization | Single language (Turkish) — add later if needed |
| FlashList | Log list is <500 items — FlatList is sufficient |

## Architecture

Feature-Based (Domain-Driven) Clean Architecture:

```
src/
├── app/                    # Expo Router — minimal routes
│   ├── _layout.tsx         # Root layout (fonts, providers)
│   ├── index.tsx           # Menu screen (default route)
│   ├── create.tsx          # Character creation
│   ├── game.tsx            # Main game screen
│   └── death.tsx           # Death/summary screen
├── features/
│   ├── game/               # Core game engine
│   │   ├── components/     # GameHeader, TabBar, LogList, StatBars, AgeButton
│   │   ├── hooks/          # useGameEngine, useAgeUp, useActions
│   │   ├── stores/         # gameStore.ts (Zustand)
│   │   ├── data/           # events.ts, jobs.ts, education.ts, activities.ts
│   │   ├── types/          # Character, LogEntry, Job, Education, GameEvent
│   │   ├── utils/          # rand, pick, clamp, pct, getZodiac, getEventsForAge
│   │   └── index.ts        # Barrel export
│   ├── character/          # Character creation & display
│   │   ├── components/     # GenderSelect, CharacterCard, ZodiacBadge
│   │   ├── hooks/          # useCharacterCreate
│   │   ├── types/          # Gender, ZodiacSign
│   │   └── index.ts
│   └── save/               # Save/load system
│       ├── hooks/          # useSaveGame, useLoadGame, useAutoSave
│       ├── utils/          # serialization, migration
│       ├── types/          # SaveData, SaveSlot
│       └── index.ts
├── shared/
│   ├── components/ui/      # Button, Card, ProgressBar, Badge, Modal
│   ├── hooks/              # useAppState, useBackHandler
│   ├── theme/              # tokens.ts (colors, spacing, radius, shadows, typography)
│   ├── constants/          # APP_CONFIG, GAME_BALANCE
│   ├── types/              # Common utility types
│   └── utils/              # formatMoney, formatAge
└── assets/
    ├── fonts/              # Outfit-*.ttf, JetBrainsMono-*.ttf
    └── animations/         # Lottie files (death, celebration, etc.)
```

### Key Architectural Rules

- **Feature isolation**: Features export only through barrel `index.ts`. Never import directly into feature internals from outside.
- **All game state in Zustand**: Single `gameStore` holds character, log, screen, activeTab. No prop drilling.
- **Components render, hooks manage logic**: No business logic in components. Game logic lives in hooks (`useGameEngine`, `useAgeUp`).
- **Composition over inheritance**: Break screens into small composed components.
- **Data files are pure**: Event pools, job lists, education data — plain typed arrays, no logic.

## Game State Machine

```
┌──────┐    createCharacter()    ┌──────────┐
│ MENU │ ──────────────────────> │  CREATE   │
└──────┘                         └────┬─────┘
   ^                                  │ selectGender()
   │                                  v
   │         ┌─────────────────────────────┐
   │         │            GAME             │
   │         │  ┌─────┐ ┌─────┐ ┌───────┐ │
   │         │  │Life │ │Job  │ │Edu    │ │
   │         │  │Log  │ │Tab  │ │Tab    │ │
   │         │  └─────┘ └─────┘ └───────┘ │
   │         │  ┌─────────────┐            │
   │         │  │Activity Tab │            │
   │         │  └─────────────┘            │
   │         └────────────┬────────────────┘
   │                      │ character.isAlive === false
   │                      v
   │              ┌──────────────┐
   └───────────── │    DEATH     │
    newGame()     │  (Summary)   │
                  └──────────────┘
```

### Zustand Game Store Structure

```typescript
interface GameStore {
  // State
  screen: 'menu' | 'create' | 'game' | 'dead';
  character: Character | null;
  log: LogEntry[];
  activeTab: 'life' | 'job' | 'edu' | 'actions';

  // Actions
  setScreen: (screen: Screen) => void;
  createCharacter: (gender: 'M' | 'F') => void;
  ageUp: () => void;
  getJob: (job: Job) => void;
  quitJob: () => void;
  startEdu: (edu: Education) => void;
  doAction: (actionId: string) => void;
  newGame: () => void;
  setActiveTab: (tab: TabId) => void;
}
```

## Brand Kit / Design Tokens

All visual values come from `src/shared/theme/tokens.ts`. Reference: `brandkit/text-life-brandkit.html` v2.1.

### Colors

```typescript
export const colors = {
  bg: '#f7f9fc',
  bgElevated: '#ffffff',
  bgSunken: '#eef1f6',

  brandPrimary: '#3b82f6',
  brandSecondary: '#60a5fa',
  brandSubtle: '#dbeafe',

  success: '#22c55e',
  successSubtle: '#dcfce7',
  successOn: '#166534',

  warning: '#f59e0b',
  warningSubtle: '#fef3c7',
  warningOn: '#92400e',

  danger: '#ef4444',
  dangerSubtle: '#fee2e2',
  dangerOn: '#991b1b',

  statHealth: '#22c55e',
  statHappiness: '#e8a308',    // NOT warning — separate token
  statSmarts: '#2563eb',       // NOT brand-primary — separate token
  statLooks: '#a855f7',

  textPrimary: '#0f172a',
  textSecondary: '#475569',
  textTertiary: '#94a3b8',
  textDisabled: '#cbd5e1',
  textInverse: '#ffffff',

  borderDefault: '#e2e8f0',
} as const;
```

### Typography

```typescript
export const fonts = {
  primary: 'Outfit',        // Headings, body, buttons, labels
  mono: 'JetBrainsMono',    // Stat values, age, money, tokens
} as const;

export const fontSizes = {
  xs: 11,   // Labels, tokens
  sm: 13,   // Buttons, helper text
  base: 15, // Body text (default)
  lg: 17,   // Description text
  xl: 20,   // Character name
  '2xl': 24, // Card title
  '3xl': 32, // Section heading
  '4xl': 40, // Page title
} as const;
```

### Spacing (4px grid)

```typescript
export const spacing = {
  xs: 4,
  sm: 8,
  md: 12,
  lg: 16,
  xl: 24,
  '2xl': 32,
  '3xl': 48,
  '4xl': 64,
} as const;
```

### Border Radius

```typescript
export const radius = {
  sm: 8,
  md: 12,
  lg: 16,
  xl: 20,
  full: 9999,
} as const;
```

## Save/Load System

Uses MMKV for persistence. Auto-save on every `ageUp()`.

```typescript
interface SaveData {
  version: number;          // For migration compatibility
  timestamp: number;
  character: Character;
  log: LogEntry[];
}
```

**Rules:**
- Auto-save after every age advancement
- Max 3 save slots
- Save format versioned for future migration
- Corrupted saves fail gracefully — never crash the app

## Animation Specifications

| Animation | Library | Duration | Easing |
|---|---|---|---|
| Stat bar fill | Reanimated | 200ms | ease-out `(0.16, 1, 0.3, 1)` |
| Screen transitions | Moti | 350ms | ease-in-out `(0.45, 0, 0.55, 1)` |
| Button press scale | Reanimated | 120ms | ease-out |
| Death screen entrance | Moti | 450ms | ease-in-out |
| Log entry appear | Moti | 200ms | ease-out, translateY 12→0 |
| Age button pulse | Reanimated | loop 2s | ease |

## Performance Rules

- **Memoize components** that receive the same props across re-renders: `React.memo()` for LogEntryItem, StatBar, ActionCard.
- **useCallback** for all functions passed as props or used in dependency arrays.
- **useMemo** for derived data: `availableJobs`, `availableEdu`, filtered lists.
- **Never** create objects/arrays inline in render — extract to constants or memoize.
- **FlatList** for log with `keyExtractor`, `getItemLayout` (fixed height), `maxToRenderPerBatch: 20`.
- **Avoid** re-rendering entire game screen on every state change — split into isolated Zustand selectors per component.

## Error Handling

- **Save corruption**: Try-catch around MMKV reads. If parse fails, offer "start new game" — never crash.
- **State inconsistency**: `ageUp()` validates character exists and `isAlive` before mutating.
- **Overflow protection**: `clamp()` on all stat modifications (0–100 range).
- **Money**: Allow negative (debt) but cap at -100,000.

## Conventions

- **Language**: All user-facing strings in Turkish. Code comments in Turkish where contextual.
- **TypeScript strict mode**: No `any`. Use proper generics or `unknown`.
- **Functional components only** with named exports (default export only for route screens in `app/`).
- **NativeWind className** for styling — no `StyleSheet.create`.
- **expo-image** instead of `Image` for any loaded images.
- Use `Reanimated`/`Moti` for animations, not `setTimeout` or `Animated` API.

### Naming

| Type | Convention | Example |
|---|---|---|
| Components | PascalCase | `StatBar.tsx` |
| Hooks | camelCase, `use` prefix | `useGameEngine.ts` |
| Stores | camelCase, `Store` suffix | `gameStore.ts` |
| Types/Interfaces | PascalCase | `Character` |
| Constants | UPPER_SNAKE_CASE | `DEATH_REASONS` |
| Data files | camelCase | `events.ts` |
| Route files | kebab-case | `index.tsx` |
| Utils | camelCase | `formatMoney.ts` |

### Import Order

1. React & React Native
2. Third-party libraries (expo, zustand, reanimated, moti)
3. Shared modules (`@/shared/...`)
4. Feature modules (`@/features/...`)
5. Relative imports (same feature)
6. Type-only imports (`import type { ... }`)

### Git

- Branch naming: `feature/...`, `fix/...`, `chore/...`
- Conventional Commits: `feat:`, `fix:`, `refactor:`, `chore:`, `docs:`

## Core Types Reference

```typescript
type Gender = 'M' | 'F';
type Screen = 'menu' | 'create' | 'game' | 'dead';
type TabId = 'life' | 'job' | 'edu' | 'actions';
type LogType = 'birth' | 'good' | 'bad' | 'milestone' | 'death' | 'event';

interface ZodiacSign {
  name: string;
  emoji: string;
  m: number;
  d: number;
}

interface Character {
  name: string;
  surname: string;
  gender: Gender;
  city: string;
  zodiac: ZodiacSign;
  birthYear: number;
  age: number;
  health: number;       // 0-100
  happiness: number;    // 0-100
  smarts: number;       // 0-100
  looks: number;        // 0-100
  money: number;        // can go negative (debt)
  job: Job | null;
  education: string[];
  currentEdu: Education | null;
  eduYearsLeft: number;
  isAlive: boolean;
  deathAge: number | null;
  deathReason: string | null;
}

interface LogEntry {
  age: number;
  text: string;
  type: LogType;
}

interface Job {
  title: string;
  salary: number;
  req: number;          // minimum smarts required
}

interface Education {
  name: string;
  cost: number;
  smartsReq: number;
  smartsGain: number;
  years: number;
  minAge: number;
}

interface GameEvent {
  t: string;
  fx: {
    health?: number;
    happiness?: number;
    smarts?: number;
    looks?: number;
    money?: number;
  };
}
```
