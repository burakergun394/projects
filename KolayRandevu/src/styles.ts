import { StyleSheet } from 'react-native';
import { colors } from './theme';

export const shadows = {
  xs: { boxShadow: '0 1px 2px rgba(0,0,0,0.06)' },
  sm: { boxShadow: '0 1px 3px rgba(0,0,0,0.08)' },
  md: { boxShadow: '0 2px 8px rgba(0,0,0,0.06)' },
  lg: { boxShadow: '0 2px 10px rgba(0,0,0,0.07)' },
  xl: { boxShadow: '0 4px 20px rgba(0,0,0,0.15)' },
} as const;

export const common = StyleSheet.create({
  flex1: { flex: 1 },
  row: { flexDirection: 'row' },
  center: { alignItems: 'center', justifyContent: 'center' },
  screenBg: { flex: 1, backgroundColor: '#f9fafb' },
  navyHeader: {
    backgroundColor: colors.navy,
    paddingHorizontal: 24,
    paddingBottom: 24,
    paddingTop: 8,
  },
});
