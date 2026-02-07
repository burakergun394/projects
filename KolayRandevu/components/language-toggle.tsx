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
    <Pressable
      onPress={toggle}
      className="flex-row items-center rounded-full px-1 py-1"
      style={{ backgroundColor: 'rgba(255,255,255,0.08)' }}
    >
      <View
        className="px-2.5 py-1 rounded-full"
        style={locale === 'tr' ? { backgroundColor: 'rgba(255,255,255,0.18)' } : undefined}
      >
        <Text
          className="font-semibold"
          style={{
            fontSize: 10,
            color: locale === 'tr' ? '#F8F9FA' : 'rgba(255,255,255,0.4)',
            letterSpacing: 0.5,
          }}
        >
          TR
        </Text>
      </View>
      <View
        className="px-2.5 py-1 rounded-full"
        style={locale === 'en' ? { backgroundColor: 'rgba(255,255,255,0.18)' } : undefined}
      >
        <Text
          className="font-semibold"
          style={{
            fontSize: 10,
            color: locale === 'en' ? '#F8F9FA' : 'rgba(255,255,255,0.4)',
            letterSpacing: 0.5,
          }}
        >
          EN
        </Text>
      </View>
    </Pressable>
  );
};
