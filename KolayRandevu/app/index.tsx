import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { View, Text, Pressable } from '@/src/tw';
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
        <View className="flex-1 px-6">
          <View className="flex-row justify-end pt-2">
            <LanguageToggle />
          </View>

          <View className="flex-1 justify-center gap-8">
            <View className="items-center gap-2">
              <Text className="text-white text-3xl font-bold">{t('role.title')}</Text>
              <Text className="text-white/70 text-base text-center">{t('role.subtitle')}</Text>
            </View>

            <View className="gap-4">
              <Pressable
                onPress={() => selectRole('provider')}
                className="bg-white rounded-3xl p-6 gap-2"
                style={{ borderCurve: 'continuous' }}
              >
                <Text className="text-navy text-xl font-bold">{t('role.provider')}</Text>
                <Text className="text-gray-500 text-sm">{t('role.providerDesc')}</Text>
              </Pressable>

              <Pressable
                onPress={() => selectRole('customer')}
                className="bg-white/10 rounded-3xl p-6 gap-2 border border-white/20"
                style={{ borderCurve: 'continuous' }}
              >
                <Text className="text-white text-xl font-bold">{t('role.customer')}</Text>
                <Text className="text-white/60 text-sm">{t('role.customerDesc')}</Text>
              </Pressable>
            </View>
          </View>
        </View>
      </SafeAreaView>
    </View>
  );
}
