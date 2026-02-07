# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**KolayRandevu** (EasyAppointment) — a dual-sided mobile appointment platform built with Expo (React Native).

Two user roles:
- **Service Providers (Businesses):** Create schedules, set working hours, approve/reject reservations, view customer history/analytics.
- **Customers:** Search providers, view available slots, request bookings, track appointment history.

## Tech Stack

- **Framework:** Expo (React Native)
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

## Repository Structure

```
├── App.js                          # Entry point
├── src/
│   ├── navigation/
│   │   ├── AppNavigator.js         # Root navigator (Auth vs Main)
│   │   ├── AuthStack.js            # Login / Register stack
│   │   ├── CustomerTabs.js         # Customer bottom tab navigator
│   │   └── ProviderTabs.js         # Provider bottom tab navigator
│   ├── screens/
│   │   ├── auth/
│   │   │   ├── LoginScreen.js
│   │   │   └── RegisterScreen.js
│   │   ├── customer/
│   │   │   ├── HomeScreen.js
│   │   │   ├── BookingScreen.js
│   │   │   └── MyAppointmentsScreen.js
│   │   └── provider/
│   │       ├── DashboardScreen.js
│   │       ├── ScheduleScreen.js
│   │       └── RequestsScreen.js
│   ├── components/
│   │   └── common/
│   │       └── Button.js
│   ├── constants/
│   │   └── colors.js
│   ├── hooks/
│   └── utils/
└── prompts/                        # Architecture and setup prompts
```

## Note

The git history contains commits from a previous unrelated multi-project repository. The KolayRandevu project starts fresh from the current working tree.
