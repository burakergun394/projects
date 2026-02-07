import { CalendarX2 } from 'lucide-react-native';
import { View, Text, StyleSheet } from 'react-native';
import { IconCircle } from '@/components/ui/icon-circle';

interface EmptyStateProps {
  message: string;
}

export const EmptyState = ({ message }: EmptyStateProps) => {
  return (
    <View style={styles.container}>
      <IconCircle icon={CalendarX2} size="xl" variant="muted" strokeWidth={1.5} />
      <Text style={styles.text}>{message}</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 64,
    gap: 16,
  },
  text: {
    color: '#9ca3af',
    fontSize: 16,
    textAlign: 'center',
  },
});
