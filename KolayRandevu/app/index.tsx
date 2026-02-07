import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Pressable as RNPressable } from 'react-native';
import { Store, UserSearch, ChevronRight } from 'lucide-react-native';
import { View, Text } from '@/src/tw';
import { LanguageToggle } from '@/components/language-toggle';
import { useStore } from '@/store/store';

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
    <View className="flex-1 bg-navy">
      <SafeAreaView style={{ flex: 1 }}>
        <View style={{ flex: 1, paddingHorizontal: 24 }}>
          {/* Top bar */}
          <View style={{ flexDirection: 'row', justifyContent: 'flex-end', paddingTop: 8 }}>
            <LanguageToggle />
          </View>

          {/* Content */}
          <View style={{ flex: 1, justifyContent: 'center' }}>
            {/* Brand area */}
            <View style={{ alignItems: 'center', marginBottom: 16 }}>
              <View style={{ flexDirection: 'row', alignItems: 'baseline' }}>
                <Text style={{ fontSize: 34, fontWeight: '300', color: '#F8F9FA', letterSpacing: 1 }}>
                  Kolay
                </Text>
                <Text style={{ fontSize: 34, fontWeight: '800', color: '#FF9800', letterSpacing: 1 }}>
                  Randevu
                </Text>
              </View>
              <Text style={{ fontSize: 15, color: 'rgba(255,255,255,0.5)', marginTop: 8, textAlign: 'center' }}>
                {t('role.subtitle')}
              </Text>
            </View>

            {/* Decorative line */}
            <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'center', gap: 10, marginBottom: 32 }}>
              <View style={{ width: 40, height: 1, backgroundColor: 'rgba(255,152,0,0.3)' }} />
              <View style={{ width: 6, height: 6, backgroundColor: '#FF9800', borderRadius: 1, transform: [{ rotate: '45deg' }] }} />
              <View style={{ width: 40, height: 1, backgroundColor: 'rgba(255,152,0,0.3)' }} />
            </View>

            {/* Provider card */}
            <RNPressable onPress={() => selectRole('provider')} style={{ marginBottom: 16 }}>
              <View
                style={{
                  backgroundColor: '#ffffff',
                  borderRadius: 20,
                  padding: 20,
                  flexDirection: 'row',
                  alignItems: 'center',
                  gap: 16,
                  borderCurve: 'continuous',
                  boxShadow: '0 4px 20px rgba(0,0,0,0.15)',
                }}
              >
                <View
                  style={{
                    width: 56,
                    height: 56,
                    borderRadius: 16,
                    backgroundColor: 'rgba(255,152,0,0.1)',
                    alignItems: 'center',
                    justifyContent: 'center',
                    borderCurve: 'continuous',
                  }}
                >
                  <Store size={26} color="#FF9800" strokeWidth={1.8} />
                </View>
                <View style={{ flex: 1, gap: 4 }}>
                  <Text style={{ fontSize: 18, fontWeight: '700', color: '#1A237E' }}>
                    {t('role.provider')}
                  </Text>
                  <Text style={{ fontSize: 13, color: '#9CA3AF', lineHeight: 18 }}>
                    {t('role.providerDesc')}
                  </Text>
                </View>
                <ChevronRight size={20} color="#d1d5db" strokeWidth={2} />
              </View>
            </RNPressable>

            {/* Customer card */}
            <RNPressable onPress={() => selectRole('customer')}>
              <View
                style={{
                  backgroundColor: 'rgba(255,255,255,0.08)',
                  borderRadius: 20,
                  padding: 20,
                  flexDirection: 'row',
                  alignItems: 'center',
                  gap: 16,
                  borderWidth: 1,
                  borderColor: 'rgba(255,255,255,0.15)',
                  borderCurve: 'continuous',
                }}
              >
                <View
                  style={{
                    width: 56,
                    height: 56,
                    borderRadius: 16,
                    backgroundColor: 'rgba(255,255,255,0.08)',
                    alignItems: 'center',
                    justifyContent: 'center',
                    borderCurve: 'continuous',
                  }}
                >
                  <UserSearch size={26} color="rgba(255,255,255,0.75)" strokeWidth={1.8} />
                </View>
                <View style={{ flex: 1, gap: 4 }}>
                  <Text style={{ fontSize: 18, fontWeight: '700', color: '#F8F9FA' }}>
                    {t('role.customer')}
                  </Text>
                  <Text style={{ fontSize: 13, color: 'rgba(255,255,255,0.45)', lineHeight: 18 }}>
                    {t('role.customerDesc')}
                  </Text>
                </View>
                <ChevronRight size={20} color="rgba(255,255,255,0.25)" strokeWidth={2} />
              </View>
            </RNPressable>
          </View>
        </View>
      </SafeAreaView>
    </View>
  );
}
