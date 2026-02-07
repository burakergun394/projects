import { Stack, Link } from 'expo-router';
import { View, Text, StyleSheet } from 'react-native';
import { Container } from '@/components/Container';
import { colors } from '@/src/theme';

export default function NotFoundScreen() {
  return (
    <View style={styles.screen}>
      <Stack.Screen options={{ title: 'Oops!' }} />
      <Container>
        <Text style={styles.title}>{"This screen doesn't exist."}</Text>
        <Link href="/" style={styles.link}>
          <Text style={styles.linkText}>Go to home screen!</Text>
        </Link>
      </Container>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: '#fff' },
  title: { fontSize: 20, fontWeight: '700' },
  link: { marginTop: 16, paddingVertical: 16 },
  linkText: { fontSize: 14, color: colors.navy },
});
