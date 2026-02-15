import type { GameEvent } from '../types';

export const BABY_EVENTS: readonly GameEvent[] = [
  { t: 'İlk adımlarını attın!', fx: { happiness: 10 } },
  { t: 'Süt dişin çıktı.', fx: { health: -5 } },
  { t: 'Annen seni parka götürdü.', fx: { happiness: 8 } },
  { t: 'Gece boyunca ağladın.', fx: { happiness: -5 } },
  { t: 'İlk kelimeni söyledin: "Anne"', fx: { happiness: 12, smarts: 5 } },
  { t: 'Grip oldun.', fx: { health: -10 } },
  { t: 'Doğum günü partisi yapıldı!', fx: { happiness: 15 } },
  { t: 'Oyuncak ayını çok sevdin.', fx: { happiness: 8 } },
  { t: 'Parkta sallanmayı öğrendin.', fx: { happiness: 5, health: 3 } },
  { t: 'Komşunun kedisi seni tırmaladı.', fx: { health: -8, happiness: -5 } },
];

export const CHILD_EVENTS: readonly GameEvent[] = [
  { t: 'Okulda yeni bir arkadaş edindin.', fx: { happiness: 10 } },
  { t: 'Matematik sınavından 100 aldın!', fx: { smarts: 8, happiness: 5 } },
  { t: 'Bahçede oynarken düştün.', fx: { health: -8 } },
  { t: 'Resim yarışmasında birinci oldun!', fx: { happiness: 12, smarts: 3 } },
  { t: 'Arkadaşınla kavga ettin.', fx: { happiness: -10 } },
  { t: 'Bisiklet sürmeyi öğrendin.', fx: { happiness: 8, health: 3 } },
  { t: 'Karne hediyesi aldın!', fx: { happiness: 10 } },
  { t: 'Su çiçeği oldun.', fx: { health: -12, looks: -5 } },
  { t: 'Futbol takımına seçildin!', fx: { health: 5, happiness: 8 } },
  { t: 'Okul gezisine katıldın.', fx: { happiness: 10, smarts: 3 } },
  { t: 'Ders çalışmaktan sıkıldın.', fx: { happiness: -5, smarts: -3 } },
  { t: 'Kuzenlerinle bayramı kutladın.', fx: { happiness: 12 } },
];

export const TEEN_EVENTS: readonly GameEvent[] = [
  { t: 'İlk aşkını yaşadın!', fx: { happiness: 15 } },
  { t: 'Sivilceler çıktı.', fx: { looks: -8, happiness: -5 } },
  { t: 'Sınav stresinden bunaldın.', fx: { happiness: -12, health: -5 } },
  { t: 'Basketbol takımına girdin.', fx: { health: 8, happiness: 5, looks: 3 } },
  { t: 'Arkadaşlarınla konsere gittin.', fx: { happiness: 12 } },
  { t: 'Gitar çalmayı öğrendin.', fx: { happiness: 8, smarts: 3 } },
  { t: 'Ailen tatile çıktı.', fx: { happiness: 10 } },
  { t: 'Kitap okuma alışkanlığı kazandın.', fx: { smarts: 10 } },
  { t: 'Kalbini kırdılar.', fx: { happiness: -15 } },
  { t: 'Yazılım öğrenmeye başladın.', fx: { smarts: 12 } },
  { t: 'Okul müsabakasında derece yaptın!', fx: { happiness: 10, smarts: 5 } },
  { t: 'Arkadaş grubuyla arası açıldın.', fx: { happiness: -8 } },
];

export const ADULT_EVENTS: readonly GameEvent[] = [
  { t: 'Terfi aldın!', fx: { happiness: 12, money: 5000 } },
  { t: 'Araba kazası geçirdin.', fx: { health: -20, money: -10000 } },
  { t: 'Yeni bir hobi edindin.', fx: { happiness: 8 } },
  { t: 'Spor salonuna yazıldın.', fx: { health: 10, looks: 5 } },
  { t: 'İş yerinde tartışma yaşadın.', fx: { happiness: -8 } },
  { t: 'Tatile çıktın.', fx: { happiness: 15, money: -3000 } },
  { t: 'Yatırımın değer kazandı!', fx: { money: 15000, happiness: 10 } },
  { t: 'Dişin çekildi.', fx: { health: -5, money: -500 } },
  { t: 'Evini yeniledim.', fx: { happiness: 8, money: -8000 } },
  { t: 'Eski bir arkadaşınla karşılaştın.', fx: { happiness: 10 } },
  { t: 'Grip oldun ve bir hafta yattın.', fx: { health: -10 } },
  { t: 'Online kurs tamamladın.', fx: { smarts: 8 } },
];

export const ELDER_EVENTS: readonly GameEvent[] = [
  { t: 'Torunun doğdu!', fx: { happiness: 20 } },
  { t: 'Bel ağrıların arttı.', fx: { health: -12 } },
  { t: 'Emekli maaşın bağlandı.', fx: { money: 2000, happiness: 5 } },
  { t: 'Bahçeyle uğraşmaya başladın.', fx: { happiness: 10, health: 3 } },
  { t: 'Kan basıncın yükseldi.', fx: { health: -15 } },
  { t: 'Eski fotoğraflara baktın.', fx: { happiness: 8 } },
  { t: 'Komşularla çay içtin.', fx: { happiness: 5 } },
  { t: 'Düşüp kalçanı incittin.', fx: { health: -18, happiness: -8 } },
  { t: 'Torunlarınla vakit geçirdin.', fx: { happiness: 15 } },
  { t: 'Göz ameliyatı oldun.', fx: { health: -8, money: -5000 } },
];

/** Yaşa uygun olay havuzunu getir */
export const getEventsForAge = (age: number): readonly GameEvent[] => {
  if (age <= 3) return BABY_EVENTS;
  if (age <= 12) return CHILD_EVENTS;
  if (age <= 17) return TEEN_EVENTS;
  if (age <= 64) return ADULT_EVENTS;
  return ELDER_EVENTS;
};
