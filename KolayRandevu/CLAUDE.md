# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**KolayRandevu** (EasyAppointment) — a dual-sided mobile appointment platform built with Expo (React Native).

Two user roles:
- **Service Providers (Businesses):** Create schedules, set working hours, approve/reject reservations, view customer history/analytics.
- **Customers:** Search providers, view available slots, request bookings, track appointment history.

## Tech Stack

- **Framework:** Expo (React Native)
- **Language:** TypeScript (strict mode)
- **Navigation:** React Navigation (Stack and Tab navigators)
- **Component Style:** Functional components with Hooks, clean and modular

## Design System

- **Primary color (trust/branding):** Navy Blue `#1A237E`
- **CTA color (buttons/highlights):** Orange `#FF9800`

## Build & Run Commands

- `npx expo start` — Start the Expo development server
- `npx expo start --web` — Start for web
- `npx expo start --android` — Start for Android
- `npx expo start --ios` — Start for iOS
- `npm install` — Install dependencies
- `npm run typecheck` — Run TypeScript type checker (no emit)

## Repository Structure

```
├── App.tsx                         # Entry point
├── tsconfig.json                   # TypeScript configuration
├── src/
│   ├── navigation/
│   │   ├── types.ts                # Navigation param list type definitions
│   │   ├── AppNavigator.tsx        # Root navigator (Auth vs Main)
│   │   ├── AuthStack.tsx           # Login / Register stack
│   │   ├── CustomerTabs.tsx        # Customer bottom tab navigator
│   │   └── ProviderTabs.tsx        # Provider bottom tab navigator
│   ├── screens/
│   │   ├── auth/
│   │   │   ├── LoginScreen.tsx
│   │   │   └── RegisterScreen.tsx
│   │   ├── customer/
│   │   │   ├── HomeScreen.tsx
│   │   │   ├── BookingScreen.tsx
│   │   │   └── MyAppointmentsScreen.tsx
│   │   └── provider/
│   │       ├── DashboardScreen.tsx
│   │       ├── ScheduleScreen.tsx
│   │       └── RequestsScreen.tsx
│   ├── components/
│   │   └── common/
│   │       └── Button.tsx
│   ├── constants/
│   │   └── colors.ts
│   ├── hooks/
│   └── utils/
└── prompts/                        # Architecture and setup prompts
```

## Note

The git history contains commits from a previous unrelated multi-project repository. The KolayRandevu project starts fresh from the current working tree.
