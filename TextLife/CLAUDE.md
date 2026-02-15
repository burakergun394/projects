# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TextLife is a subdirectory within a monorepo at `projects/`. The git root is `C:/Users/Burak/Documents/MY/GITHUB/BURAKERGUN394/projects`. This is a **new project** — no source code exists yet.

## Planned Tech Stack

| Concern | Library |
|---|---|
| Framework | Expo SDK 52+ (Managed Workflow) |
| Routing | Expo Router v4 (file-based) |
| State (client) | Zustand |
| State (server) | TanStack React Query v5 |
| API Layer | Axios |
| Forms | React Hook Form + Zod |
| Styling | NativeWind v4 (Tailwind for RN) |
| Storage | expo-secure-store (tokens), MMKV (general) |
| Animations | React Native Reanimated 3 + Moti |
| i18n | expo-localization + i18next |
| Testing | Jest + React Native Testing Library |

## Architecture

Feature-Based (Domain-Driven) Clean Architecture:

```
src/
├── app/              # Expo Router file-based routing (group layouts)
├── features/         # Feature modules, each with: api/, components/, hooks/, stores/, types/, index.ts
├── shared/           # Reusable code: components/ui, hooks, lib/api, providers, theme, types, utils, constants
└── assets/           # Static assets: images, fonts, animations (Lottie)
```

### Key Architectural Rules

- **Feature isolation**: Features export only through barrel `index.ts`. Never import directly into feature internals from outside.
- **Server state in TanStack Query, client state in Zustand**: Never copy API data into Zustand stores.
- **Components render, hooks manage logic**: No business logic in components.
- **Composition over inheritance**: Break screens into small composed components.
- **Query key factory pattern**: Use `createQueryKeys(feature)` for consistent cache keys.

## Conventions

- **Language**: Code comments and documentation in Turkish where contextual.
- **TypeScript strict mode**: No `any`. Use proper generics or `unknown`.
- **Functional components only** with named exports (default export only for route screens).
- **NativeWind className** for styling — no `StyleSheet.create`.
- **expo-secure-store** for tokens — never MMKV or AsyncStorage for sensitive data.
- **expo-image** instead of `Image`, **FlashList** instead of `FlatList` for large lists.
- Use `Reanimated`/`Moti` for animations, not `setTimeout`.
- All user-facing strings through i18next `t('namespace:key')`.

### Naming

| Type | Convention | Example |
|---|---|---|
| Components | PascalCase | `BookingCard.tsx` |
| Hooks | camelCase, `use` prefix | `useBookings.ts` |
| Stores | camelCase, `Store` suffix | `authStore.ts` |
| Types | PascalCase | `BookingResponse` |
| Constants | UPPER_SNAKE_CASE | `API_BASE_URL` |
| Route files | kebab-case | `booking-detail.tsx` |

### Import Order

1. React & React Native
2. Third-party libraries
3. Shared modules (`@/shared/...`)
4. Feature modules (`@/features/...`)
5. Relative imports (same feature)
6. Type-only imports

### Git

- Branch naming: `feature/...`, `fix/...`, `chore/...`
- Conventional Commits: `feat:`, `fix:`, `refactor:`, `chore:`, `docs:`
