import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useWindowDimensions } from 'react-native';
import Animated, { FadeIn, FadeInDown, FadeInUp } from 'react-native-reanimated';
import { Image } from 'expo-image';
import { View, Text, Pressable } from '@/src/tw';
import { LanguageToggle } from '@/components/language-toggle';
import { useStore } from '@/store/store';

export default function RoleSelection() {
  const { t } = useTranslation();
  const router = useRouter();
  const setRole = useStore((s) => s.setRole);
  const { width } = useWindowDimensions();

  const selectRole = (role: 'provider' | 'customer') => {
    setRole(role);
    if (role === 'provider') {
      router.replace('/(provider)/dashboard');
    }
  };

  const ringSize = width * 0.85;

  return (
    <View
      className="flex-1"
      style={{
        backgroundColor: '#0f174a',
        experimental_backgroundImage: 'linear-gradient(180deg, #263088 0%, #0f174a 100%)',
      }}
    >
      {/* ── Decorative background rings ── */}
      <Animated.View
        entering={FadeIn.duration(1000).delay(100)}
        style={{
          position: 'absolute',
          top: -ringSize * 0.28,
          right: -ringSize * 0.32,
          width: ringSize,
          height: ringSize,
          borderRadius: ringSize / 2,
          borderWidth: 1,
          borderColor: 'rgba(255,255,255,0.04)',
        }}
      />
      <Animated.View
        entering={FadeIn.duration(1000).delay(200)}
        style={{
          position: 'absolute',
          top: -ringSize * 0.28 + 28,
          right: -ringSize * 0.32 + 28,
          width: ringSize - 56,
          height: ringSize - 56,
          borderRadius: (ringSize - 56) / 2,
          borderWidth: 1,
          borderColor: 'rgba(255,255,255,0.025)',
        }}
      />
      <Animated.View
        entering={FadeIn.duration(1000).delay(350)}
        style={{
          position: 'absolute',
          bottom: '12%',
          left: -50,
          width: 140,
          height: 140,
          borderRadius: 70,
          borderWidth: 1,
          borderColor: 'rgba(255,152,0,0.07)',
        }}
      />

      <SafeAreaView style={{ flex: 1 }}>
        <View className="flex-1 px-7">
          {/* ── Language toggle ── */}
          <Animated.View
            entering={FadeIn.duration(400).delay(500)}
            style={{ alignItems: 'flex-end', paddingTop: 8 }}
          >
            <LanguageToggle />
          </Animated.View>

          <View className="flex-1 justify-center gap-12">
            {/* ── Brand area ── */}
            <Animated.View
              entering={FadeInDown.duration(650).delay(150)}
              style={{ alignItems: 'center', gap: 14 }}
            >
              {/* Accent dot row */}
              <View className="flex-row gap-1.5 mb-1">
                {[0.15, 0.3, 0.55, 1, 0.55, 0.3, 0.15].map((opacity, i) => (
                  <View
                    key={i}
                    style={{
                      width: 4,
                      height: 4,
                      borderRadius: 2,
                      backgroundColor: `rgba(255,152,0,${opacity})`,
                    }}
                  />
                ))}
              </View>

              {/* Split brand: "Kolay" light + "Randevu" bold orange */}
              <View className="flex-row items-baseline">
                <Text style={{ fontSize: 36, fontWeight: '300', color: '#F8F9FA', letterSpacing: 2 }}>
                  Kolay
                </Text>
                <Text style={{ fontSize: 36, fontWeight: '800', color: '#FF9800', letterSpacing: 2 }}>
                  Randevu
                </Text>
              </View>

              {/* Diamond separator */}
              <View className="flex-row items-center gap-3">
                <View style={{ width: 36, height: 1, backgroundColor: 'rgba(255,152,0,0.35)' }} />
                <View
                  style={{
                    width: 6,
                    height: 6,
                    backgroundColor: '#FF9800',
                    borderRadius: 1,
                    transform: [{ rotate: '45deg' }],
                  }}
                />
                <View style={{ width: 36, height: 1, backgroundColor: 'rgba(255,152,0,0.35)' }} />
              </View>

              <Text
                className="text-base text-center"
                style={{ color: 'rgba(248,249,250,0.55)', letterSpacing: 0.3 }}
              >
                {t('role.subtitle')}
              </Text>
            </Animated.View>

            {/* ── Role cards (solid navy with orange border) ── */}
            <View className="gap-4">
              {/* Provider card */}
              <Animated.View entering={FadeInUp.duration(550).delay(450)}>
                <Pressable
                  onPress={() => selectRole('provider')}
                  className="rounded-3xl p-5 flex-row items-center gap-4"
                  style={{
                    borderCurve: 'continuous',
                    backgroundColor: '#1c2670',
                    borderWidth: 1,
                    borderColor: 'rgba(255,152,0,0.3)',
                    boxShadow: '0 8px 32px rgba(0,0,0,0.4)',
                  }}
                >
                  <View
                    className="rounded-2xl items-center justify-center"
                    style={{
                      width: 64,
                      height: 64,
                      borderCurve: 'continuous',
                      backgroundColor: 'rgba(255,152,0,0.12)',
                    }}
                  >
                    <Image
                      source="sf:storefront.fill"
                      style={{ width: 30, height: 30 }}
                      tintColor="#FF9800"
                    />
                  </View>
                  <View className="flex-1 gap-1">
                    <Text style={{ fontSize: 18, fontWeight: '700', color: '#F8F9FA' }}>
                      {t('role.provider')}
                    </Text>
                    <Text style={{ fontSize: 13, color: 'rgba(248,249,250,0.5)', lineHeight: 18 }}>
                      {t('role.providerDesc')}
                    </Text>
                  </View>
                  <Image
                    source="sf:chevron.right"
                    style={{ width: 14, height: 14 }}
                    tintColor="rgba(255,152,0,0.5)"
                  />
                </Pressable>
              </Animated.View>

              {/* Customer card */}
              <Animated.View entering={FadeInUp.duration(550).delay(600)}>
                <Pressable
                  onPress={() => selectRole('customer')}
                  className="rounded-3xl p-5 flex-row items-center gap-4"
                  style={{
                    borderCurve: 'continuous',
                    backgroundColor: '#1c2670',
                    borderWidth: 1,
                    borderColor: 'rgba(255,152,0,0.3)',
                    boxShadow: '0 8px 32px rgba(0,0,0,0.4)',
                  }}
                >
                  <View
                    className="rounded-2xl items-center justify-center"
                    style={{
                      width: 64,
                      height: 64,
                      borderCurve: 'continuous',
                      backgroundColor: 'rgba(255,152,0,0.12)',
                    }}
                  >
                    <Image
                      source="sf:person.fill"
                      style={{ width: 28, height: 28 }}
                      tintColor="#FF9800"
                    />
                  </View>
                  <View className="flex-1 gap-1">
                    <Text style={{ fontSize: 18, fontWeight: '700', color: '#F8F9FA' }}>
                      {t('role.customer')}
                    </Text>
                    <Text style={{ fontSize: 13, color: 'rgba(248,249,250,0.5)', lineHeight: 18 }}>
                      {t('role.customerDesc')}
                    </Text>
                  </View>
                  <Image
                    source="sf:chevron.right"
                    style={{ width: 14, height: 14 }}
                    tintColor="rgba(255,152,0,0.5)"
                  />
                </Pressable>
              </Animated.View>
            </View>
          </View>
        </View>
      </SafeAreaView>
    </View>
  );
}
