import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Store, UserSearch, ChevronRight } from 'lucide-react-native';
import { View, Text, Pressable } from '@/src/tw';
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
    <View className="flex-1 bg-navy">
      <SafeAreaView style={{ flex: 1 }}>
        <View className="flex-1 px-6">
          {/* Top bar */}
          <View className="flex-row justify-end pt-2">
            <LanguageToggle />
          </View>

          {/* Content */}
          <View className="flex-1 justify-center">
            {/* Brand area */}
            <View className="items-center mb-4">
              <View className="flex-row items-baseline">
                <Text className="text-surface text-4xl font-light tracking-wide">Kolay</Text>
                <Text className="text-orange text-4xl font-extrabold tracking-wide">Randevu</Text>
              </View>
              <Text className="text-white/50 text-sm mt-2 text-center">{t('role.subtitle')}</Text>
            </View>

            {/* Decorative line */}
            <View className="flex-row items-center justify-center gap-2.5 mb-8">
              <View className="w-10 h-px bg-orange/30" />
              <View
                className="w-1.5 h-1.5 bg-orange rounded-sm"
                style={{ transform: [{ rotate: '45deg' }] }}
              />
              <View className="w-10 h-px bg-orange/30" />
            </View>

            {/* Provider card */}
            <Card
              shadow="xl"
              onPress={() => selectRole('provider')}
              className="p-5 flex-row items-center gap-4 mb-4">
              <IconCircle icon={Store} size="lg" variant="orange" />
              <View className="flex-1 gap-1">
                <Text className="text-navy text-lg font-bold">{t('role.provider')}</Text>
                <Text className="text-muted text-xs leading-[18px]">{t('role.providerDesc')}</Text>
              </View>
              <ChevronRight size={20} color={colors.chevron} strokeWidth={2} />
            </Card>

            {/* Customer card */}
            <Pressable
              onPress={() => selectRole('customer')}
              className="rounded-2xl p-5 flex-row items-center gap-4 border border-white/15 bg-white/[0.08] border-continuous">
              <View className="w-14 h-14 rounded-xl items-center justify-center bg-white/[0.08] border-continuous">
                <UserSearch size={26} color="rgba(255,255,255,0.75)" strokeWidth={1.8} />
              </View>
              <View className="flex-1 gap-1">
                <Text className="text-surface text-lg font-bold">{t('role.customer')}</Text>
                <Text className="text-white/45 text-xs leading-[18px]">
                  {t('role.customerDesc')}
                </Text>
              </View>
              <ChevronRight size={20} color="rgba(255,255,255,0.25)" strokeWidth={2} />
            </Pressable>
          </View>
        </View>
      </SafeAreaView>
    </View>
  );
}
