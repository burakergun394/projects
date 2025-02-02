"use client";

import { createContext, useContext, useCallback, useMemo } from "react";
import { ConfigProvider, theme as antTheme, notification } from "antd";
import { ThemeContextType, ThemeConfig } from "./types";
import { useLocalStorage } from "@/hooks/localStorage";

const ThemeContext = createContext<ThemeContextType>({
  theme: { mode: "light" },
  setTheme: () => {},
});

const THEME_KEY = "app_theme";

export function ThemeProvider({ children }: { children: React.ReactNode }) {
  const [theme, setThemeState] = useLocalStorage<ThemeConfig>(THEME_KEY, {
    mode: "light",
  });

  const setTheme = useCallback(
    (newTheme: Partial<ThemeConfig>) => {
      setThemeState((prev) => ({ ...prev, ...newTheme }));
    },
    [setThemeState]
  );

  const themeConfig = useMemo(
    () => ({
      algorithm: theme.mode === "dark" ? antTheme.darkAlgorithm : antTheme.defaultAlgorithm,
      token: {
        borderRadius: 6,
      },
      components: {
        Layout: {
          siderBg: theme.mode === "dark" ? "#141414" : "#ffffff",
          headerBg: theme.mode === "dark" ? "#141414" : "#ffffff",
        },
      },
    }),
    [theme]
  );

  const value = useMemo(
    () => ({
      theme,
      setTheme,
    }),
    [theme, setTheme]
  );

  return (
    <ThemeContext.Provider value={value}>
      <ConfigProvider theme={themeConfig}>
        {children}
      </ConfigProvider>
    </ThemeContext.Provider>
  );
}

export const useTheme = () => useContext(ThemeContext);
