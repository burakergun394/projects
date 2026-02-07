import { Pressable, Text, View } from '@/src/tw';
import { useTranslation } from 'react-i18next';
import { useStore } from '@/store/store';

export const LanguageToggle = () => {
  const { i18n } = useTranslation();
  const { locale, setLocale } = useStore();

  const toggle = () => {
    const next = locale === 'tr' ? 'en' : 'tr';
    setLocale(next);
    i18n.changeLanguage(next);
  };

  return (
    <Pressable onPress={toggle} className="flex-row items-center bg-white/20 rounded-full px-3 py-1.5">
      <View className={`px-2 py-0.5 rounded-full ${locale === 'tr' ? 'bg-white' : ''}`}>
        <Text className={`text-xs font-bold ${locale === 'tr' ? 'text-navy' : 'text-white/70'}`}>
          TR
        </Text>
      </View>
      <View className={`px-2 py-0.5 rounded-full ${locale === 'en' ? 'bg-white' : ''}`}>
        <Text className={`text-xs font-bold ${locale === 'en' ? 'text-navy' : 'text-white/70'}`}>
          EN
        </Text>
      </View>
    </Pressable>
  );
};
