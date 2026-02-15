export const colors = {
  bg: '#f7f9fc',
  bgElevated: '#ffffff',
  bgSunken: '#eef1f6',

  brandPrimary: '#3b82f6',
  brandSecondary: '#60a5fa',
  brandSubtle: '#dbeafe',

  success: '#22c55e',
  successSubtle: '#dcfce7',
  successOn: '#166534',

  warning: '#f59e0b',
  warningSubtle: '#fef3c7',
  warningOn: '#92400e',

  danger: '#ef4444',
  dangerSubtle: '#fee2e2',
  dangerOn: '#991b1b',

  statHealth: '#22c55e',
  statHappiness: '#e8a308',
  statSmarts: '#2563eb',
  statLooks: '#a855f7',

  textPrimary: '#0f172a',
  textSecondary: '#475569',
  textTertiary: '#94a3b8',
  textDisabled: '#cbd5e1',
  textInverse: '#ffffff',

  borderDefault: '#e2e8f0',
} as const;

export const fonts = {
  primary: 'Outfit',
  mono: 'JetBrainsMono',
} as const;

export const fontSizes = {
  xs: 11,
  sm: 13,
  base: 15,
  lg: 17,
  xl: 20,
  '2xl': 24,
  '3xl': 32,
  '4xl': 40,
} as const;

export const spacing = {
  xs: 4,
  sm: 8,
  md: 12,
  lg: 16,
  xl: 24,
  '2xl': 32,
  '3xl': 48,
  '4xl': 64,
} as const;

export const radius = {
  sm: 8,
  md: 12,
  lg: 16,
  xl: 20,
  full: 9999,
} as const;

export const shadows = {
  sm: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 1,
  },
  md: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 4,
    elevation: 2,
  },
  lg: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.12,
    shadowRadius: 8,
    elevation: 4,
  },
} as const;
