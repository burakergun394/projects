export type ThemeMode = 'light' | 'dark';

export interface ThemeConfig {
  mode: ThemeMode;
}

export interface ThemeContextType {
  theme: ThemeConfig;
  setTheme: (theme: Partial<ThemeConfig>) => void;
} 