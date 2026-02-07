import type { ElementType } from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Card } from '@/components/ui/card';
import { IconCircle } from '@/components/ui/icon-circle';
import { colors } from '@/src/theme';

interface StatCardProps {
  label: string;
  value: number | string;
  accent?: boolean;
  icon?: ElementType;
}

export const StatCard = ({ label, value, accent = false, icon: Icon }: StatCardProps) => {
  return (
    <Card shadow="md" style={styles.card}>
      <View style={styles.topRow}>
        {Icon && (
          <IconCircle icon={Icon} size="sm" variant={accent ? 'orange' : 'navy'} strokeWidth={2} />
        )}
        <Text style={styles.value}>{value}</Text>
      </View>
      <Text style={styles.label}>{label}</Text>
    </Card>
  );
};

const styles = StyleSheet.create({
  card: { flex: 1, padding: 16, gap: 12 },
  topRow: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' },
  value: {
    color: colors.navy,
    fontWeight: '700',
    fontSize: 30,
    fontVariant: ['tabular-nums'],
  },
  label: { color: '#6b7280', fontSize: 12 },
});
