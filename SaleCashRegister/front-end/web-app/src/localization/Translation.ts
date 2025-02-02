import i18next from "i18next";
import { Language, LANGUAGE_KEY, DEFAULT_LANGUAGE } from "./types";

class LocalizationService {
  private static instance: LocalizationService;

  private constructor() {}

  static getInstance(): LocalizationService {
    if (!LocalizationService.instance) {
      LocalizationService.instance = new LocalizationService();
    }
    return LocalizationService.instance;
  }

  changeLanguage(lang: Language): void {
    if (typeof window !== "undefined") {
      localStorage.setItem(LANGUAGE_KEY, lang);
    }
    i18next.changeLanguage(lang);
  }

  getCurrentLanguage(): Language {
    if (typeof window === "undefined") return DEFAULT_LANGUAGE;
    return (localStorage.getItem(LANGUAGE_KEY) as Language) || DEFAULT_LANGUAGE;
  }

  initializeLanguage(): void {
    const storedLanguage = this.getCurrentLanguage();
    this.changeLanguage(storedLanguage);
  }
}

export const Localization = LocalizationService.getInstance();