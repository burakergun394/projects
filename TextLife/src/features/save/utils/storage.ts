import AsyncStorage from '@react-native-async-storage/async-storage';

/**
 * Senkron benzeri arayüz — AsyncStorage üzerine basit sarmalayıcı.
 * Expo Go ile uyumlu. Gerçek senkron erişim için dev client + MMKV kullanılabilir.
 */

const cache = new Map<string, string>();
let initialized = false;

const ensureLoaded = async () => {
  if (initialized) return;
  try {
    const keys = await AsyncStorage.getAllKeys();
    const pairs = await AsyncStorage.multiGet(keys);
    for (const [key, value] of pairs) {
      if (value !== null) cache.set(key, value);
    }
  } catch {
    // İlk yüklemede hata olursa boş cache ile devam et
  }
  initialized = true;
};

// Uygulama başladığında cache'i doldur
ensureLoaded();

export const storage = {
  set: (key: string, value: string) => {
    cache.set(key, value);
    AsyncStorage.setItem(key, value).catch(() => {});
  },
  getString: (key: string): string | undefined => {
    return cache.get(key);
  },
  contains: (key: string): boolean => {
    return cache.has(key);
  },
  remove: (key: string) => {
    cache.delete(key);
    AsyncStorage.removeItem(key).catch(() => {});
  },
  getAllKeys: (): string[] => {
    return Array.from(cache.keys());
  },
};

export const SAVE_KEYS = {
  slot: (id: number) => `save-slot-${id}`,
  lastSlot: 'last-save-slot',
} as const;
