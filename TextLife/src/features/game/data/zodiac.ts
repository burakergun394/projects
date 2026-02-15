import type { ZodiacSign } from '../types';

export const ZODIAC_SIGNS: readonly ZodiacSign[] = [
  { name: 'Oğlak', emoji: '♑', m: 1, d: 19 },
  { name: 'Kova', emoji: '♒', m: 2, d: 18 },
  { name: 'Balık', emoji: '♓', m: 3, d: 20 },
  { name: 'Koç', emoji: '♈', m: 4, d: 19 },
  { name: 'Boğa', emoji: '♉', m: 5, d: 20 },
  { name: 'İkizler', emoji: '♊', m: 6, d: 20 },
  { name: 'Yengeç', emoji: '♋', m: 7, d: 22 },
  { name: 'Aslan', emoji: '♌', m: 8, d: 22 },
  { name: 'Başak', emoji: '♍', m: 9, d: 22 },
  { name: 'Terazi', emoji: '♎', m: 10, d: 22 },
  { name: 'Akrep', emoji: '♏', m: 11, d: 21 },
  { name: 'Yay', emoji: '♐', m: 12, d: 21 },
  { name: 'Oğlak', emoji: '♑', m: 12, d: 31 },
] as const;

/** Ay ve güne göre burç hesapla */
export const getZodiac = (month: number, day: number): ZodiacSign => {
  for (const sign of ZODIAC_SIGNS) {
    if (month < sign.m || (month === sign.m && day <= sign.d)) {
      return sign;
    }
  }
  // Aralık sonu → Oğlak
  return ZODIAC_SIGNS[0];
};
