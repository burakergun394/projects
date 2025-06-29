import * as Localization from 'expo-localization';
import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import en from './locales/en';
import tr from './locales/tr';

const resources = {
  en: en,
  tr: tr,
};

i18n.use(initReactI18next).init({
  resources,
  lng: Localization.locale.split('-')[0],
  fallbackLng: 'en',
  compatibilityJSON: 'v4',
  interpolation: {
    escapeValue: false,
  },
});

export default i18n; 