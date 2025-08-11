# Rahmet Kapısı - Surah List App Development

**IMPORTANT: Use @context 7 for all file operations and maintain context awareness throughout the development process.**

## Project Status
✅ **Expo project already created** with `npx create-expo-app RahmetKapisi --template blank-typescript`

## Project Overview
Develop a modern, clean, and user-friendly Quran application named "Rahmet Kapısı" (Gate of Mercy). **PHASE 1: Start with Surah List only** - Focus on creating a beautiful surah listing interface with navigation to individual surah pages. Individual ayahs will be added in later phases.

## Next Steps for Development

### 1. Install Required Dependencies
```bash
# Navigation and routing
npx expo install expo-router expo-secure-store
npm install @expo/vector-icons

# State management
npm install zustand

# Styling
npm install @tamagui/core @tamagui/config @tamagui/animations-react-native @tamagui/font-inter @tamagui/theme-base @tamagui/lucide-icons react-native-svg
npm install --save-dev @tamagui/babel-plugin

# Additional utilities
npx expo install expo-screen-orientation expo-keep-awake
```

### 2. Configure Project Structure
Update the following files and create the required folder structure.

## Technical Stack

### Core Technologies
- **Framework:** Expo (React Native) - ✅ Already created
- **Language:** TypeScript - ✅ Already configured
- **Styling:** Tamagui (Modern React Native UI System)
- **State Management:** Zustand
- **Local Storage:** Expo SecureStore
- **Navigation:** Expo Router (file-based routing)
- **Icons:** @tamagui/lucide-icons

### Data Source
- **NO API Integration** - Use provided local JSON file for Surah data
- Use the provided `surah.json` file structure
- All data should be embedded in the app (offline-first)

## Required File Structure to Create

```
app/
├── (tabs)/
│   ├── index.tsx          // Home Dashboard
│   ├── surah-list.tsx     // Surah List (Main Feature)
│   ├── settings.tsx       // Settings
│   └── _layout.tsx        // Tab Layout
├── surah/
│   └── [id].tsx          // Individual Surah Page (Placeholder for now)
├── _layout.tsx           // Root Layout
└── +not-found.tsx        // 404 Page

assets/
├── data/
│   └── surah.json        // Provided surah data (COPY PROVIDED FILE HERE)

components/
├── ui/                  // Reusable UI components
│   ├── SurahCard.tsx    // Individual surah card component
│   ├── SearchBar.tsx    // Search functionality
│   └── LoadingSpinner.tsx
└── surah/              // Surah-specific components

stores/
└── surahStore.ts       // Zustand store for surah data

types/
└── index.ts            // TypeScript interfaces

constants/
└── Colors.ts           // App color scheme
```

## Configuration Files to Update

### 1. app.json
```json
{
  "expo": {
    "name": "Rahmet Kapısı",
    "slug": "rahmet-kapisi",
    "version": "1.0.0",
    "scheme": "rahmet-kapisi",
    "orientation": "portrait",
    "icon": "./assets/icon.png",
    "userInterfaceStyle": "automatic",
    "splash": {
      "image": "./assets/splash.png",
      "resizeMode": "contain",
      "backgroundColor": "#059669"
    },
    "assetBundlePatterns": [
      "**/*"
    ],
    "ios": {
      "supportsTablet": true
    },
    "android": {
      "adaptiveIcon": {
        "foregroundImage": "./assets/adaptive-icon.png",
        "backgroundColor": "#059669"
      }
    },
    "web": {
      "bundler": "metro",
      "output": "static",
      "favicon": "./assets/favicon.png"
    },
    "plugins": ["expo-router"],
    "experiments": {
      "typedRoutes": true
    }
  }
}
```

### 2. tamagui.config.ts
```typescript
import { config } from '@tamagui/config/v3'
import { createTamagui } from '@tamagui/core'

const appConfig = createTamagui({
  ...config,
  themes: {
    ...config.themes,
    light: {
      ...config.themes.light,
      primary: '#059669',
      primaryLight: '#ecfdf5',
      primaryDark: '#047857',
      background: '#ffffff',
      backgroundSoft: '#f9fafb',
      color: '#111827',
      colorPress: '#374151',
    },
    dark: {
      ...config.themes.dark,
      primary: '#10b981',
      primaryLight: '#022c22',
      primaryDark: '#047857',
      background: '#111827',
      backgroundSoft: '#1f2937',
      color: '#f9fafb',
      colorPress: '#d1d5db',
    },
  },
})

export default appConfig
```

### 3. metro.config.js
```javascript
const { getDefaultConfig } = require('expo/metro-config');

const config = getDefaultConfig(__dirname);

module.exports = config;
```

## Data Structure (Using Provided surah.json)

```typescript
// types/index.ts
interface SurahData {
  data: Surah[];
}

interface Surah {
  id: number;
  name: string;           // Turkish name (e.g., "Fatiha")
  name_en: string;        // English name (e.g., "Al-Fatihah")
  slug: string;           // URL slug (e.g., "fatiha")
  verse_count: number;    // Number of ayahs (e.g., 7)
  page_number: number;    // Mushaf page number (e.g., 0)
  name_original: string;  // Arabic name (e.g., "سُورَةُ ٱلْفَاتِحَةِ")
}

// User preferences interface
interface UserPreferences {
  isDarkMode: boolean;
  fontSize: number;
  favoriteSurahs: number[];
  lastReadSurah: number | null;
}
```

## Zustand Store Structure (Phase 1)

```typescript
// stores/surahStore.ts
import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import * as SecureStore from 'expo-secure-store';
import { SurahData, Surah, UserPreferences } from '../types';

interface SurahStore {
  // Data
  surahData: SurahData | null;
  isLoading: boolean;
  error: string | null;
  
  // Search & Filter
  searchQuery: string;
  filteredSurahs: Surah[];
  
  // User Preferences
  isDarkMode: boolean;
  fontSize: number;
  favoriteSurahs: number[];
  lastReadSurah: number | null;
  
  // Actions
  loadSurahData: () => Promise<void>;
  setSearchQuery: (query: string) => void;
  toggleDarkMode: () => void;
  setFontSize: (size: number) => void;
  addToFavorites: (surahId: number) => void;
  removeFromFavorites: (surahId: number) => void;
  setLastReadSurah: (surahId: number) => void;
  
  // Computed
  getFilteredSurahs: () => Surah[];
}

// Implementation with SecureStore persistence
const secureStorage = {
  getItem: async (name: string): Promise<string | null> => {
    return await SecureStore.getItemAsync(name);
  },
  setItem: async (name: string, value: string): Promise<void> => {
    await SecureStore.setItemAsync(name, value);
  },
  removeItem: async (name: string): Promise<void> => {
    await SecureStore.deleteItemAsync(name);
  },
};

export const useSurahStore = create<SurahStore>()(
  persist(
    (set, get) => ({
      // Initial state
      surahData: null,
      isLoading: false,
      error: null,
      searchQuery: '',
      filteredSurahs: [],
      isDarkMode: false,
      fontSize: 18,
      favoriteSurahs: [],
      lastReadSurah: null,
      
      // Actions implementation here...
    }),
    {
      name: 'rahmet-kapisi-storage',
      storage: createJSONStorage(() => secureStorage),
      partialize: (state) => ({
        isDarkMode: state.isDarkMode,
        fontSize: state.fontSize,
        favoriteSurahs: state.favoriteSurahs,
        lastReadSurah: state.lastReadSurah,
      }),
    }
  )
);
```

## Key Components to Build

### 1. SurahCard Component
```typescript
// components/ui/SurahCard.tsx
interface SurahCardProps {
  surah: Surah;
  onPress: () => void;
  isFavorite: boolean;
  onToggleFavorite: () => void;
}

// Should display:
// - Arabic name (RTL, larger font)
// - Turkish and English names
// - Verse count and page number
// - Favorite heart icon
// - Beautiful gradient or card design
```

### 2. SearchBar Component
```typescript
// components/ui/SearchBar.tsx
interface SearchBarProps {
  value: string;
  onChangeText: (text: string) => void;
  placeholder?: string;
}

// Should search through:
// - name (Turkish)
// - name_en (English) 
// - name_original (Arabic)
```

## Navigation Setup

### Root Layout (app/_layout.tsx)
```typescript
import { Stack } from 'expo-router/stack';
import { useColorScheme } from 'nativewind';
import '../global.css';

export default function Layout() {
  const { colorScheme } = useColorScheme();
  
  return (
    <Stack screenOptions={{
      headerStyle: { 
        backgroundColor: colorScheme === 'dark' ? '#1F2937' : '#059669' 
      },
      headerTintColor: '#fff',
    }}>
      <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
      <Stack.Screen 
        name="surah/[id]" 
        options={{ 
          title: 'Sure',
          presentation: 'card'
        }} 
      />
    </Stack>
  );
}
```

### Tab Layout (app/(tabs)/_layout.tsx)
```typescript
import { Tabs } from 'expo-router/tabs';
import { Ionicons } from '@expo/vector-icons';
import { useColorScheme } from 'nativewind';

export default function TabLayout() {
  const { colorScheme } = useColorScheme();
  
  return (
    <Tabs screenOptions={{
      tabBarActiveTintColor: '#059669',
      tabBarInactiveTintColor: colorScheme === 'dark' ? '#9CA3AF' : '#6B7280',
      tabBarStyle: {
        backgroundColor: colorScheme === 'dark' ? '#1F2937' : '#fff',
        borderTopColor: colorScheme === 'dark' ? '#374151' : '#E5E7EB',
      },
      headerStyle: {
        backgroundColor: colorScheme === 'dark' ? '#1F2937' : '#059669',
      },
      headerTintColor: '#fff',
    }}>
      <Tabs.Screen
        name="index"
        options={{
          title: 'Ana Sayfa',
          tabBarIcon: ({ color, size }) => (
            <Ionicons name="home" size={size} color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="surah-list"
        options={{
          title: 'Sureler',
          tabBarIcon: ({ color, size }) => (
            <Ionicons name="list" size={size} color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="settings"
        options={{
          title: 'Ayarlar',
          tabBarIcon: ({ color, size }) => (
            <Ionicons name="settings" size={size} color={color} />
          ),
        }}
      />
    </Tabs>
  );
}
```

## Page Components (Phase 1 Focus)

### 1. Home Dashboard (app/(tabs)/index.tsx)
- Welcome message with "Rahmet Kapısı" branding
- Quick stats (total surahs: 114)
- Last read surah quick access
- Favorite surahs section
- Beautiful Islamic-themed design

### 2. **Surah List (app/(tabs)/surah-list.tsx) - MAIN FEATURE**
- Display all 114 surahs from provided JSON
- Search functionality (by Turkish, English, or Arabic name)
- Each surah card should show:
  - Arabic name (name_original)
  - Turkish name (name)
  - English name (name_en)
  - Verse count (verse_count)
  - Page number (page_number)
- Beautiful card design with proper RTL support for Arabic text
- Smooth scrolling with FlatList
- Pull-to-refresh functionality
- Heart icon for favorites

### 3. Individual Surah Page (app/surah/[id].tsx)
- **Phase 1: Simple placeholder page**
- Show surah details from JSON
- "Coming Soon" message for ayah reading
- Navigation back to surah list

### 4. Settings (app/(tabs)/settings.tsx)
- Dark/Light mode toggle
- Font size slider (for future Arabic text)
- About page with app info
- Reset favorites option

## Tamagui Styling Guidelines

```typescript
// constants/Colors.ts
export const Colors = {
  light: {
    primary: '#059669',
    background: '#FFFFFF',
    card: '#F9FAFB',
    text: '#111827',
    textSecondary: '#6B7280',
    border: '#E5E7EB',
  },
  dark: {
    primary: '#10B981',
    background: '#111827',
    card: '#1F2937',
    text: '#F9FAFB',
    textSecondary: '#9CA3AF',
    border: '#374151',
  },
};

// Common Tamagui components and styling patterns
import { YStack, XStack, Text, Button } from '@tamagui/core';

// Container patterns
<YStack flex={1} backgroundColor="$background">
  // Content
</YStack>

// Card patterns
<YStack backgroundColor="$backgroundSoft" borderRadius="$4" padding="$4" marginBottom="$3">
  // Card content
</YStack>

// Button patterns
<Button backgroundColor="$primary" color="white" borderRadius="$4">
  Button Text
</Button>

// Text patterns
<Text fontSize="$6" fontWeight="600" color="$color" textAlign="right">
  Arabic Text
</Text>
```

## Development Priority (Phase 1)

1. **✅ Project Setup** - Already completed
2. **📦 Dependencies Installation** - Install required packages
3. **⚙️ Configuration** - Setup routing, styling, and build config
4. **📁 File Structure** - Create required folders and files
5. **🗃️ Data Integration** - Copy surah.json and create TypeScript interfaces
6. **🏪 Zustand Store** - Implement state management with persistence
7. **🎨 UI Components** - SurahCard, SearchBar, LoadingSpinner
8. **📋 Surah List Page** - Main feature implementation
9. **🏠 Home & Settings** - Basic pages
10. **✨ Polish** - Animations, error handling, optimization

## Phase 1 Goals
- ✅ Beautiful, functional surah listing
- ✅ Search and favorites functionality  
- ✅ Smooth navigation and user experience
- ✅ Dark/Light mode support
- ✅ Offline-first architecture
- ⏳ Ayah reading (Phase 2)
- ⏳ Bookmarking specific ayahs (Phase 2)
- ⏳ Reading progress tracking (Phase 2)

## Important Notes
- **Use @context 7** throughout development for full codebase awareness
- **Copy the provided surah.json** to `assets/data/surah.json`
- **Focus on Phase 1 features** - beautiful surah listing first
- **Ensure RTL support** for Arabic text display
- **Implement proper TypeScript** - no `any` types allowed
- **Performance first** - use FlatList, proper memoization
- **Accessibility** - proper labels and navigation

**Remember: Use @context 7 throughout development to maintain awareness of the entire codebase and ensure consistency across all files and components.**

## Expo Router File Structure (Phase 1)

```
app/
├── (tabs)/
│   ├── index.tsx          // Home Dashboard
│   ├── surah-list.tsx     // Surah List (Main Feature)
│   ├── settings.tsx       // Settings
│   └── _layout.tsx        // Tab Layout
├── surah/
│   └── [id].tsx          // Individual Surah Page (Placeholder for now)
├── _layout.tsx           // Root Layout
└── +not-found.tsx        // 404 Page

assets/
├── data/
│   └── surah.json        // Provided surah data
└── fonts/               // Arabic fonts (if needed)

components/
├── ui/                  // Reusable UI components
│   ├── SurahCard.tsx    // Individual surah card component
│   ├── SearchBar.tsx    // Search functionality
│   └── LoadingSpinner.tsx
└── surah/              // Surah-specific components

stores/
└── surahStore.ts       // Zustand store for surah data

types/
└── index.ts            // TypeScript interfaces
```

## Data Structure (Using Provided surah.json)

```typescript
// Based on the provided surah.json structure
interface SurahData {
  data: Surah[];
}

interface Surah {
  id: number;
  name: string;           // Turkish name (e.g., "Fatiha")
  name_en: string;        // English name (e.g., "Al-Fatihah")
  slug: string;           // URL slug (e.g., "fatiha")
  verse_count: number;    // Number of ayahs (e.g., 7)
  page_number: number;    // Mushaf page number (e.g., 0)
  name_original: string;  // Arabic name (e.g., "سُورَةُ ٱلْفَاتِحَةِ")
}

// Phase 1: Focus on Surah listing only
// Individual ayahs will be added in later phases
```

## Zustand Store Structure (Phase 1)

```typescript
interface SurahStore {
  // Data
  surahData: SurahData | null;
  isLoading: boolean;
  error: string | null;
  
  // Search & Filter
  searchQuery: string;
  filteredSurahs: Surah[];
  
  // User Preferences
  isDarkMode: boolean;
  fontSize: number;
  
  // Favorites & Bookmarks (for future phases)
  favoriteSurahs: number[];
  lastReadSurah: number | null;
  
  // Actions
  loadSurahData: () => Promise<void>;
  setSearchQuery: (query: string) => void;
  toggleDarkMode: () => void;
  setFontSize: (size: number) => void;
  addToFavorites: (surahId: number) => void;
  removeFromFavorites: (surahId: number) => void;
  setLastReadSurah: (surahId: number) => void;
  
  // Storage
  saveToStorage: () => Promise<void>;
  loadFromStorage: () => Promise<void>;
}

// Simplified interfaces for Phase 1
interface SurahData {
  data: Surah[];
}

interface Surah {
  id: number;
  name: string;
  name_en: string;
  slug: string;
  verse_count: number;
  page_number: number;
  name_original: string;
}
```

## Navigation Setup

### Root Layout (app/_layout.tsx)
```typescript
import { Stack } from 'expo-router/stack';
import { useColorScheme } from 'nativewind';

export default function Layout() {
  const { colorScheme } = useColorScheme();
  
  return (
    <Stack screenOptions={{
      headerStyle: { backgroundColor: colorScheme === 'dark' ? '#1F2937' : '#059669' },
      headerTintColor: '#fff',
    }}>
      <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
      <Stack.Screen name="surah/[id]" options={{ title: 'Surah Reading' }} />
    </Stack>
  );
}
```

### Tab Layout (app/(tabs)/_layout.tsx)
```typescript
import { Tabs } from 'expo-router/tabs';
import { Ionicons } from '@expo/vector-icons';
import { useColorScheme } from 'nativewind';

export default function TabLayout() {
  const { colorScheme } = useColorScheme();
  
  return (
    <Tabs screenOptions={{
      tabBarActiveTintColor: '#059669',
      tabBarInactiveTintColor: colorScheme === 'dark' ? '#9CA3AF' : '#6B7280',
      tabBarStyle: {
        backgroundColor: colorScheme === 'dark' ? '#1F2937' : '#fff',
      },
    }}>
      <Tabs.Screen
        name="index"
        options={{
          title: 'Home',
          tabBarIcon: ({ color, size }) => <Ionicons name="home" size={size} color={color} />,
        }}
      />
      <Tabs.Screen
        name="surah-list"
        options={{
          title: 'Surahs',
          tabBarIcon: ({ color, size }) => <Ionicons name="list" size={size} color={color} />,
        }}
      />
      <Tabs.Screen
        name="settings"
        options={{
          title: 'Settings',
          tabBarIcon: ({ color, size }) => <Ionicons name="settings" size={size} color={color} />,
        }}
      />
    </Tabs>
  );
}
```

## Page Components (Phase 1 Focus)

### 1. Home Dashboard (app/(tabs)/index.tsx)
- Welcome message with "Rahmet Kapısı" branding
- Quick stats (total surahs: 114)
- Last read surah quick access
- Favorite surahs section
- Beautiful Islamic-themed design

### 2. **Surah List (app/(tabs)/surah-list.tsx) - MAIN FEATURE**
- Display all 114 surahs from provided JSON
- Search functionality (by Turkish, English, or Arabic name)
- Each surah card should show:
  - Arabic name (name_original)
  - Turkish name (name)
  - English name (name_en)
  - Verse count (verse_count)
  - Page number (page_number)
- Beautiful card design with proper RTL support for Arabic text
- Smooth scrolling with FlatList
- Pull-to-refresh functionality
- Heart icon for favorites

### 3. Individual Surah Page (app/surah/[id].tsx)
- **Phase 1: Simple placeholder page**
- Show surah details from JSON
- "Coming Soon" message for ayah reading
- Navigation back to surah list

### 4. Settings (app/(tabs)/settings.tsx)
- Dark/Light mode toggle
- Font size slider (for future Arabic text)
- About page with app info
- Reset favorites option

## Key Components to Build

### SurahCard Component
```typescript
interface SurahCardProps {
  surah: Surah;
  onPress: () => void;
  isFavorite: boolean;
  onToggleFavorite: () => void;
}

// Should display:
// - Arabic name (RTL, larger font)
// - Turkish and English names
// - Verse count and page number
// - Favorite heart icon
// - Beautiful gradient or card design
```

### SearchBar Component
```typescript
interface SearchBarProps {
  value: string;
  onChangeText: (text: string) => void;
  placeholder?: string;
}

// Should search through:
// - name (Turkish)
// - name_en (English) 
// - name_original (Arabic)
```

## NativeWind Styling Guidelines

```typescript
// Design System
const colors = {
  primary: 'emerald-600',
  primaryDark: 'emerald-800',
  secondary: 'slate-600',
  background: {
    light: 'gray-50',
    dark: 'gray-900'
  },
  text: {
    light: 'gray-800',
    dark: 'gray-100'
  }
};

// Common styles
const commonStyles = {
  container: "flex-1 bg-gray-50 dark:bg-gray-900",
  arabicText: "text-right font-arabic leading-loose text-gray-800 dark:text-gray-100",
  translationText: "text-left text-base text-gray-600 dark:text-gray-300 mt-2",
  card: "bg-white dark:bg-gray-800 rounded-lg shadow-sm p-4 mb-3",
  button: "bg-emerald-600 hover:bg-emerald-700 px-4 py-2 rounded-lg",
  buttonText: "text-white font-medium text-center",
};
```

## Key Features Implementation

### RTL Support for Arabic Text
- Use `textAlign: 'right'` for Arabic text
- Implement proper line height for Arabic fonts
- Use `writingDirection: 'rtl'` where needed

### Performance Optimization
- Use `FlatList` with `getItemLayout` for surah lists
- Implement lazy loading for ayahs
- Use `useMemo` for heavy computations
- Optimize re-renders with proper state management

### Accessibility
- Add proper accessibility labels
- Support for screen readers
- High contrast mode support
- Large font support

### Offline-First Approach
- All data stored locally in JSON
- Use SecureStore for user preferences
- No network dependencies after initial install

## Setup Commands

```bash
# Create Expo project
npx create-expo-app RahmetKapisi --template blank-typescript

# Install dependencies
npx expo install expo-router expo-secure-store
npm install zustand
npm install @tamagui/core @tamagui/config @tamagui/animations-react-native @tamagui/font-inter @tamagui/theme-base @tamagui/lucide-icons react-native-svg
npm install --save-dev @tamagui/babel-plugin
npm install @expo/vector-icons
npx expo install expo-screen-orientation expo-keep-awake

# Tamagui setup
# Create tamagui.config.ts with theme configuration
```

## Development Priority (Phase 1)

1. **Project Setup & Navigation** 
   - Setup Expo Router with proper TypeScript config
   - Configure Tamagui with custom theme and typography

2. **Data Integration**
   - Import provided surah.json file
   - Create TypeScript interfaces matching the JSON structure
   - Setup Zustand store to load and manage surah data

3. **Core UI Components**
   - SurahCard component with Tamagui styling and animations
   - SearchBar component with Tamagui inputs and icons
   - Loading states and error handling with Tamagui components

4. **Surah List Page** 
   - Main feature: Beautiful, searchable list of all 114 surahs
   - FlatList with performance optimization
   - Search functionality across all name fields
   - Favorites system

5. **Basic Navigation**
   - Tab navigation between Home, Surah List, Settings
   - Navigation to individual surah pages (placeholder for now)

6. **Settings & Preferences**
   - Dark/Light mode implementation
   - Font size preferences (for future use)
   - Persistent storage with SecureStore

7. **Polish & Optimization**
   - Smooth animations and transitions
   - RTL support for Arabic text
   - Performance optimization
   - Error boundaries

## Phase 1 Goals
- ✅ Beautiful, functional surah listing
- ✅ Search and favorites functionality  
- ✅ Smooth navigation and user experience
- ✅ Dark/Light mode support
- ✅ Offline-first architecture
- ⏳ Ayah reading (Phase 2)
- ⏳ Bookmarking specific ayahs (Phase 2)
- ⏳ Reading progress tracking (Phase 2)

## Code Quality Requirements

- **TypeScript**: Strict mode enabled, no `any` types
- **ESLint**: Airbnb config with React Native rules
- **Prettier**: Consistent code formatting
- **File Organization**: Consistent import/export patterns
- **Component Architecture**: Functional components with hooks
- **State Management**: Centralized with Zustand
- **Error Handling**: Proper try-catch and error boundaries

**Remember: Use @context 7 throughout development to maintain awareness of the entire codebase and ensure consistency across all files and components.**