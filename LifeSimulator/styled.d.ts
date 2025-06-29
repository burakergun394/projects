import 'styled-components/native';

declare module 'styled-components/native' {
  export interface DefaultTheme {
    // Background colors
    backgroundPrimary: string;
    backgroundSecondary: string;
    backgroundGradient: readonly [string, string];
    
    // Surface colors
    surface: string;
    surfaceSecondary: string;
    surfaceElevated: string;
    
    // Primary colors
    primary: string;
    primaryLight: string;
    primaryDark: string;
    
    // Secondary colors
    secondary: string;
    secondaryLight: string;
    secondaryDark: string;
    
    // Accent colors
    accent: string;
    accentLight: string;
    accentDark: string;
    
    // Text colors
    text: string;
    textSecondary: string;
    textMuted: string;
    textLight: string;
    
    // Border colors
    border: string;
    borderLight: string;
    borderMuted: string;
    
    // Status colors
    success: string;
    warning: string;
    error: string;
    info: string;
    
    // Life Simulator specific colors
    life: string;
    energy: string;
    happiness: string;
    health: string;
    money: string;
    experience: string;
    
    // Nature colors
    nature: {
      forest: string;
      ocean: string;
      sky: string;
      sunset: string;
      flower: string;
      earth: string;
    };
  }
} 