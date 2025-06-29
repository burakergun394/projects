import {
  DefaultTheme as NavigationDefaultTheme,
  ThemeProvider as NavigationThemeProvider,
} from '@react-navigation/native';
import { Stack } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import 'react-native-reanimated';
import { ThemeProvider as StyledComponentsThemeProvider } from 'styled-components/native';

import { Colors } from '../constants/Colors';
import '../i18n';

export default function RootLayout() {
  const theme = Colors;
  
  const navigationTheme = {
    dark: false,
    colors: {
      primary: theme.primary,
      background: theme.backgroundPrimary,
      card: theme.surface,
      text: theme.text,
      border: theme.border,
      notification: theme.accent,
    },
    fonts: NavigationDefaultTheme.fonts,
  };

  return (
    <StyledComponentsThemeProvider theme={theme}>
      <NavigationThemeProvider value={navigationTheme}>
        <Stack>
          <Stack.Screen name="index" options={{ headerShown: false }} />
          <Stack.Screen name="+not-found" />
        </Stack>
        <StatusBar style="dark" />
      </NavigationThemeProvider>
    </StyledComponentsThemeProvider>
  );
}
