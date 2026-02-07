import { View, Text, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Card } from '@/components/ui/card';
import { colors } from '@/src/theme';
import type { Customer } from '@/store/types';

interface CustomerRowProps {
  customer: Customer;
  onPress?: () => void;
}

export const CustomerRow = ({ customer, onPress }: CustomerRowProps) => {
  const { t } = useTranslation();

  return (
    <Card shadow="sm" onPress={onPress} style={styles.card}>
      <View style={styles.avatar}>
        <Text style={styles.avatarText}>{customer.name.charAt(0)}</Text>
      </View>
      <View style={styles.info}>
        <Text style={styles.name}>{customer.name}</Text>
        <Text style={styles.visits}>
          {customer.appointmentCount} {t('customers.visits')}
        </Text>
      </View>
      <Text style={styles.date}>{customer.lastVisit}</Text>
    </Card>
  );
};

const styles = StyleSheet.create({
  card: {
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
  },
  avatar: {
    backgroundColor: colors.navy,
    borderRadius: 9999,
    width: 44,
    height: 44,
    alignItems: 'center',
    justifyContent: 'center',
  },
  avatarText: {
    color: '#fff',
    fontWeight: '700',
    fontSize: 16,
  },
  info: {
    flex: 1,
    gap: 2,
  },
  name: {
    color: '#111827',
    fontWeight: '600',
    fontSize: 16,
  },
  visits: {
    color: '#6b7280',
    fontSize: 14,
  },
  date: {
    color: '#9ca3af',
    fontSize: 12,
  },
});
