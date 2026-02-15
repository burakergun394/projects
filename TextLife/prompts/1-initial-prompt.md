# TextLife — Initial Setup Prompt

Paste the following prompt into Claude Code:

---

```
Read the CLAUDE.md file and follow its instructions. Create the TextLife project from scratch.

## 1. Project Setup

Create a new TypeScript project with Expo SDK 52+:

```bash
npx create-expo-app@latest TextLife --template blank-typescript
cd TextLife
```

Install required dependencies:

```bash
npx expo install expo-router expo-font expo-splash-screen react-native-reanimated react-native-safe-area-context react-native-screens react-native-gesture-handler
npm install zustand nativewind tailwindcss moti react-native-mmkv
npm install -D @types/react @types/react-native
```

## 2. Configuration Files

- `tsconfig.json` → strict mode, `@/` path alias (pointing to `src/`)
- `tailwind.config.js` → NativeWind v4 configuration, brand kit colors in theme extend
- `babel.config.js` → NativeWind and Reanimated plugins
- `app.json` → Expo Router scheme, fonts, splash screen

## 3. Directory Structure

Follow the Architecture section in CLAUDE.md exactly. Create all folders and barrel `index.ts` files:

```
src/
├── app/
│   ├── _layout.tsx
│   ├── index.tsx
│   ├── create.tsx
│   ├── game.tsx
│   └── death.tsx
├── features/
│   ├── game/
│   │   ├── components/
│   │   ├── hooks/
│   │   ├── stores/
│   │   ├── data/
│   │   ├── types/
│   │   ├── utils/
│   │   └── index.ts
│   ├── character/
│   │   ├── components/
│   │   ├── hooks/
│   │   ├── types/
│   │   └── index.ts
│   └── save/
│       ├── hooks/
│       ├── utils/
│       ├── types/
│       └── index.ts
├── shared/
│   ├── components/ui/
│   ├── hooks/
│   ├── theme/
│   ├── constants/
│   ├── types/
│   └── utils/
└── assets/
    ├── fonts/
    └── animations/
```

## 4. Theme & Tokens

Create `src/shared/theme/tokens.ts`. Use all values from the Brand Kit / Design Tokens section in CLAUDE.md exactly: colors, fonts, fontSizes, spacing, radius.

Create `src/shared/theme/index.ts` barrel export.

## 5. Core Types

Write all types from the Core Types Reference section in CLAUDE.md into `src/features/game/types/index.ts`: Gender, Screen, TabId, LogType, ZodiacSign, Character, LogEntry, Job, Education, GameEvent.

## 6. Game Data

Create separate files under `src/features/game/data/`:

- `zodiac.ts` → ZODIAC_SIGNS array + getZodiac function
- `events.ts` → BABY_EVENTS, CHILD_EVENTS, TEEN_EVENTS, ADULT_EVENTS, ELDER_EVENTS + getEventsForAge
- `jobs.ts` → JOBS array (14 jobs)
- `education.ts` → EDUCATION_LIST array (4 education tiers)
- `names.ts` → MALE_NAMES, FEMALE_NAMES, SURNAMES, CITIES
- `index.ts` → barrel export

All data in Turkish. All arrays with proper TypeScript types.

## 7. Utils

`src/features/game/utils/index.ts`:

- `rand(a: number, b: number): number`
- `pick<T>(arr: T[]): T`
- `clamp(v: number, lo?: number, hi?: number): number`
- `pct(n: number): boolean`

`src/shared/utils/`:

- `formatMoney.ts` → Turkish Lira format (₺1.234.567)
- `formatAge.ts` → "24 yaş" format

## 8. Zustand Store

`src/features/game/stores/gameStore.ts` → Implement the GameStore interface from CLAUDE.md.

All state and actions in a single store:
- screen, character, log, activeTab states
- createCharacter, ageUp, getJob, quitJob, startEdu, doAction, newGame, setActiveTab actions
- Inside ageUp: aging, health decay, salary, education progress, random events, death check, milestones

## 9. Shared UI Components

Under `src/shared/components/ui/`, create base components:

- `Button.tsx` → Primary, Secondary, Ghost, Danger, Disabled variants. NativeWind className. Reanimated press scale animation (120ms).
- `Card.tsx` → Elevated card wrapper. Border, radius, shadow from tokens.
- `ProgressBar.tsx` → Animated stat bar. Reanimated width transition (200ms ease-out). Color prop.
- `Badge.tsx` → Chip/label. Subtle background + on-color text.

## 10. Game Components

`src/features/game/components/`:

- `GameHeader.tsx` → Character name, age, city, zodiac, job, money display
- `StatBars.tsx` → 4 stat bars (health/happiness/smarts/looks) with correct color tokens
- `TabBar.tsx` → 4 tabs (Hayat/İş/Eğitim/Aktivite) with active state
- `LogList.tsx` → FlatList, LogEntryItem render. Dot + chip + text design.
- `LogEntryItem.tsx` → React.memo. Colored dot (by type), age label, event text.
- `AgeButton.tsx` → Primary CTA. Disabled state (after death). Brand shadow.
- `ActionGrid.tsx` → 2-column grid, action cards (emoji + label + cost)
- `JobList.tsx` → Current job card + open positions list
- `EduList.tsx` → Completed + in-progress + available options

## 11. Character Components

`src/features/character/components/`:

- `GenderSelect.tsx` → Two cards (👦 Erkek / 👧 Kadın)
- `CharacterCard.tsx` → Summary card for death screen

## 12. Screens (Expo Router)

`src/app/_layout.tsx`:
- SafeAreaProvider, font loading (Outfit + JetBrains Mono), splash screen control
- NativeWind provider

`src/app/index.tsx` (Menu):
- Logo (🧬 + "TEXT LIFE" + "YAŞAM SİMÜLASYONU"), "Yeni Hayat Başlat" button, "Devam Et" if saved game exists

`src/app/create.tsx`:
- GenderSelect, back button

`src/app/game.tsx`:
- GameHeader, TabBar, tab content (LogList / JobList / EduList / ActionGrid), StatBars + AgeButton in sticky bottom footer
- StatBars positioned at bottom of screen: stat bars above the age button

`src/app/death.tsx`:
- Gravestone emoji, name, life years, death reason, summary card (money/education/job/score), "Yeni Hayat Başlat" button

## 13. Save System

`src/features/save/`:

- `types/index.ts` → SaveData, SaveSlot interfaces
- `utils/serialization.ts` → serializeSave, deserializeSave (JSON + version)
- `hooks/useSaveGame.ts` → save to MMKV
- `hooks/useLoadGame.ts` → load from MMKV
- `hooks/useAutoSave.ts` → auto-save after ageUp

## Rules

- All files .ts / .tsx — no .js / .jsx
- No StyleSheet.create — use NativeWind className
- No `any` type — strict TypeScript
- Every component is functional with named export (except route screens which use default export)
- Turkish user-facing strings
- Follow the import order from CLAUDE.md
- Every feature exports through barrel index.ts
- Memoization: LogEntryItem, StatBar, ActionCard → React.memo
- useCallback: ageUp, doAction, getJob, quitJob, startEdu
- useMemo: availableJobs, availableEdu

Create all files with full implementation — do not leave any file empty or with placeholder content. Make the project runnable.
```
