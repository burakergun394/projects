# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**KolayRandevu** (EasyAppointment) — a dual-sided mobile appointment platform built with Expo (React Native).

Two user roles:
- **Service Providers (Businesses):** Create schedules, set working hours, approve/reject reservations, view customer history/analytics.
- **Customers:** Search providers, view available slots, request bookings, track appointment history.

## Color Palette

- **Navy Blue:** `#1A237E` — primary color
- **Orange:** `#FF9800` — accent color

## Commands

```bash
npm start            # Start Expo dev server
npm run android      # Start on Android
npm run ios          # Start on iOS
npm run web          # Start on web
npm run lint         # Run ESLint + Prettier check
npm run format       # Auto-fix ESLint + Prettier
```

## Architecture

**Expo Router v6** with file-based routing. Entry point is `expo-router/entry` (configured in package.json `main`).

- `app/` — Routes. `_layout.tsx` defines a Stack navigator. Each file becomes a route (e.g., `details.tsx` → `/details`). Special files: `+html.tsx` (web HTML shell), `+not-found.tsx` (404).
- `components/` — Shared UI components (Button, Container, ScreenContent).
- `src/global.css` — Tailwind CSS global stylesheet (imported in `app/_layout.tsx`). Defines custom theme colors (`--color-navy`, `--color-orange`) and platform font variables.
- `src/tw/` — CSS-wrapped React Native components for use with Tailwind `className`. Import `View`, `Text`, `Pressable`, `ScrollView`, `TextInput`, `TouchableHighlight`, `Link`, `AnimatedScrollView`, and `useCSSVariable` from `@/src/tw`. Import `Image` from `@/src/tw/image`. Import `Animated` from `@/src/tw/animated`.
- `store/` — Zustand state management. Single store in `store.ts` exporting `useStore` hook.

**Path aliases:** `@/*` maps to the project root (configured in tsconfig.json). Use `@/components/Button` style imports.

**Typed routes** are enabled (`experiments.typedRoutes` in app.json). Expo generates route types automatically.

## Style Guidelines

Use **Tailwind CSS v4 + NativeWind v5** via `className` for styling. Import components from `@/src/tw` (not from `react-native` directly) to get `className` support. Use Tailwind utility classes like `className="flex-1 bg-navy text-white p-4"`. Custom theme colors `navy` and `orange` are available (e.g., `bg-navy`, `text-orange`).

Do not create inline `StyleSheet` definitions in every component.

## Key Config

- **TypeScript:** Strict mode, extends `expo/tsconfig.base`
- **Babel:** Uses `babel-preset-expo` + `react-native-worklets/plugin`
- **Prettier:** Single quotes, 100 char width, trailing commas (es5), bracket same line
- **ESLint:** Flat config with `eslint-config-expo`, `react/display-name` disabled
- **Tailwind CSS v4 + NativeWind v5:** Styling via `className` on CSS-wrapped components from `@/src/tw`. Metro config wrapped with `withNativewind`. PostCSS configured with `@tailwindcss/postcss`. Global CSS in `src/global.css`.
- **Platforms:** iOS and Android (defined in app.json). Web supported via metro bundler with static output.
