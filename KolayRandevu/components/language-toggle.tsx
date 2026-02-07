import { Pressable, Text, View, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import { useStore } from '@/store/store';
import { colors } from '@/src/theme';

export const LanguageToggle = () => {
  const { i18n } = useTranslation();
  const { locale, setLocale } = useStore();

  const toggle = () => {
    const next = locale === 'tr' ? 'en' : 'tr';
    setLocale(next);
    i18n.changeLanguage(next);
  };

  return (
    <Pressable onPress={toggle} style={styles.container}>
      <View style={[styles.pill, locale === 'tr' && styles.pillActive]}>
        <Text style={[styles.label, locale === 'tr' ? styles.labelActive : styles.labelInactive]}>
          TR
        </Text>
      </View>
      <View style={[styles.pill, locale === 'en' && styles.pillActive]}>
        <Text style={[styles.label, locale === 'en' ? styles.labelActive : styles.labelInactive]}>
          EN
        </Text>
      </View>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(255,255,255,0.2)',
    borderRadius: 9999,
    paddingHorizontal: 12,
    paddingVertical: 6,
  },
  pill: {
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 9999,
  },
  pillActive: {
    backgroundColor: '#fff',
  },
  label: {
    fontSize: 12,
    fontWeight: '700',
  },
  labelActive: {
    color: colors.navy,
  },
  labelInactive: {
    color: 'rgba(255,255,255,0.7)',
  },
});
