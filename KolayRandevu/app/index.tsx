import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { View, Text, Pressable, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Store, UserSearch, ChevronRight } from 'lucide-react-native';
import { LanguageToggle } from '@/components/language-toggle';
import { Card } from '@/components/ui/card';
import { IconCircle } from '@/components/ui/icon-circle';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';

export default function RoleSelection() {
  const { t } = useTranslation();
  const router = useRouter();
  const setRole = useStore((s) => s.setRole);

  const selectRole = (role: 'provider' | 'customer') => {
    setRole(role);
    if (role === 'provider') {
      router.replace('/(provider)/dashboard');
    }
  };

  return (
    <View style={styles.screen}>
      <SafeAreaView style={styles.safe}>
        <View style={styles.content}>
          {/* Top bar */}
          <View style={styles.topBar}>
            <LanguageToggle />
          </View>

          {/* Content */}
          <View style={styles.center}>
            {/* Brand area */}
            <View style={styles.brand}>
              <View style={styles.brandRow}>
                <Text style={styles.brandLight}>Kolay</Text>
                <Text style={styles.brandBold}>Randevu</Text>
              </View>
              <Text style={styles.subtitle}>{t('role.subtitle')}</Text>
            </View>

            {/* Decorative line */}
            <View style={styles.decorRow}>
              <View style={styles.decorLine} />
              <View style={styles.decorDiamond} />
              <View style={styles.decorLine} />
            </View>

            {/* Provider card */}
            <Card shadow="xl" onPress={() => selectRole('provider')} style={styles.providerCard}>
              <IconCircle icon={Store} size="lg" variant="orange" />
              <View style={styles.cardText}>
                <Text style={styles.providerTitle}>{t('role.provider')}</Text>
                <Text style={styles.providerDesc}>{t('role.providerDesc')}</Text>
              </View>
              <ChevronRight size={20} color={colors.chevron} strokeWidth={2} />
            </Card>

            {/* Customer card */}
            <Pressable onPress={() => selectRole('customer')} style={styles.customerCard}>
              <View style={styles.customerIcon}>
                <UserSearch size={26} color="rgba(255,255,255,0.75)" strokeWidth={1.8} />
              </View>
              <View style={styles.cardText}>
                <Text style={styles.customerTitle}>{t('role.customer')}</Text>
                <Text style={styles.customerDesc}>{t('role.customerDesc')}</Text>
              </View>
              <ChevronRight size={20} color="rgba(255,255,255,0.25)" strokeWidth={2} />
            </Pressable>
          </View>
        </View>
      </SafeAreaView>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: { flex: 1, backgroundColor: colors.navy },
  safe: { flex: 1 },
  content: { flex: 1, paddingHorizontal: 24 },
  topBar: { flexDirection: 'row', justifyContent: 'flex-end', paddingTop: 8 },
  center: { flex: 1, justifyContent: 'center' },
  brand: { alignItems: 'center', marginBottom: 16 },
  brandRow: { flexDirection: 'row', alignItems: 'baseline' },
  brandLight: {
    color: colors.surface,
    fontSize: 36,
    fontWeight: '300',
    letterSpacing: 0.5,
  },
  brandBold: {
    color: colors.orange,
    fontSize: 36,
    fontWeight: '800',
    letterSpacing: 0.5,
  },
  subtitle: {
    color: 'rgba(255,255,255,0.5)',
    fontSize: 14,
    marginTop: 8,
    textAlign: 'center',
  },
  decorRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 10,
    marginBottom: 32,
  },
  decorLine: { width: 40, height: 1, backgroundColor: 'rgba(255,152,0,0.3)' },
  decorDiamond: {
    width: 6,
    height: 6,
    backgroundColor: colors.orange,
    borderRadius: 2,
    transform: [{ rotate: '45deg' }],
  },
  providerCard: {
    padding: 20,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
    marginBottom: 16,
  },
  cardText: { flex: 1, gap: 4 },
  providerTitle: { color: colors.navy, fontSize: 18, fontWeight: '700' },
  providerDesc: { color: colors.muted, fontSize: 12, lineHeight: 18 },
  customerCard: {
    borderRadius: 16,
    padding: 20,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
    borderWidth: 1,
    borderColor: 'rgba(255,255,255,0.15)',
    backgroundColor: 'rgba(255,255,255,0.08)',
    borderCurve: 'continuous',
  },
  customerIcon: {
    width: 56,
    height: 56,
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'rgba(255,255,255,0.08)',
    borderCurve: 'continuous',
  },
  customerTitle: { color: colors.surface, fontSize: 18, fontWeight: '700' },
  customerDesc: { color: 'rgba(255,255,255,0.45)', fontSize: 12, lineHeight: 18 },
});
