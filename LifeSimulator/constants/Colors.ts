export const Colors = {
  // Background gradients inspired by life/nature
  backgroundPrimary: '#F0F9FF', // Light sky blue
  backgroundSecondary: '#ECFDF5', // Light mint green
  backgroundGradient: ['#F0F9FF', '#ECFDF5'], // Sky to nature gradient
  
  // Surface colors
  surface: '#FFFFFF',
  surfaceSecondary: '#F8FAFC',
  surfaceElevated: '#FFFFFF',
  
  // Logo-inspired primary colors
  primary: '#059669', // Logo green
  primaryLight: '#10B981',
  primaryDark: '#047857',
  
  secondary: '#06B6D4', // Logo cyan
  secondaryLight: '#22D3EE',
  secondaryDark: '#0891B2',
  
  accent: '#3B82F6', // Logo blue
  accentLight: '#60A5FA',
  accentDark: '#2563EB',
  
  // Text colors
  text: '#0F172A',
  textSecondary: '#475569',
  textMuted: '#64748B',
  textLight: '#94A3B8',
  
  // Border colors
  border: '#E2E8F0',
  borderLight: '#F1F5F9',
  borderMuted: '#CBD5E1',
  
  // Status colors
  success: '#10B981',
  warning: '#F59E0B',
  error: '#EF4444',
  info: '#3B82F6',
  
  // Life Simulator specific colors (inspired by life elements)
  life: '#10B981', // Green for life/nature
  energy: '#F59E0B', // Orange for energy/sun
  happiness: '#EC4899', // Pink for emotions/happiness
  health: '#EF4444', // Red for health/vitality
  money: '#059669', // Green for money/prosperity
  experience: '#8B5CF6', // Purple for wisdom/experience
  
  // Nature-inspired accent colors
  nature: {
    forest: '#047857',
    ocean: '#0891B2',
    sky: '#3B82F6',
    sunset: '#F59E0B',
    flower: '#EC4899',
    earth: '#92400E',
  },
} as const;

export type ColorName = keyof typeof Colors; 