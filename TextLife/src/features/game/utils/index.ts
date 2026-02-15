/** Rastgele tam sayı üret (dahil) */
export const rand = (a: number, b: number): number =>
  Math.floor(Math.random() * (b - a + 1)) + a;

/** Diziden rastgele bir eleman seç */
export const pick = <T>(arr: readonly T[]): T =>
  arr[Math.floor(Math.random() * arr.length)];

/** Değeri min-max aralığına sıkıştır */
export const clamp = (v: number, lo = 0, hi = 100): number =>
  Math.min(hi, Math.max(lo, v));

/** %n olasılıkla true döndür */
export const pct = (n: number): boolean => Math.random() * 100 < n;
