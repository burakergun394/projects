import { useTranslation as useI18nTranslation } from "react-i18next";
import type { Language } from "./types";

export const useLocalization = () => {
  const { t, i18n } = useI18nTranslation();

  const changeLanguage = (lang: Language) => {
    i18n.changeLanguage(lang);
    localStorage.setItem("app_language", lang);
  };

  return {
    t,
    currentLanguage: i18n.language as Language,
    changeLanguage,
  };
};

