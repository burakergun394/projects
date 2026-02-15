import type { GameEvent } from '../types';

export const MARRIAGE_EVENTS: readonly GameEvent[] = [
  { t: 'Biriyle tanıştın ve hoşlandın.', fx: { happiness: 12 } },
  { t: 'Sevgilinle güzel bir akşam yemeği yedin.', fx: { happiness: 8, money: -500 } },
  { t: 'Sevgiline evlenme teklifi ettin ve kabul etti! 💍', fx: { happiness: 20 } },
  { t: 'Muhteşem bir düğün yaptın!', fx: { happiness: 15, money: -20000 } },
  { t: 'Eşinle tatile çıktın.', fx: { happiness: 10, money: -5000 } },
  { t: 'Eşinle küçük bir tartışma yaşadın.', fx: { happiness: -5 } },
  { t: 'Eşinle büyük bir kavga ettin.', fx: { happiness: -15 } },
  { t: 'Evliliğinizin yıldönümünü kutladın.', fx: { happiness: 12 } },
  { t: 'Eşin sana sürpriz bir hediye aldı.', fx: { happiness: 10 } },
  { t: 'Eşinle ilişkiniz güçlendi.', fx: { happiness: 8 } },
  { t: 'Eşinle aranızda güven sorunu oluştu.', fx: { happiness: -12 } },
  { t: 'Evlilik danışmanına gittin.', fx: { happiness: 3, money: -2000 } },
  { t: 'Eşin seni aldattı!', fx: { happiness: -25 } },
  { t: 'Boşanma davası açtın.', fx: { happiness: -15, money: -15000 } },
];

export const FRIENDSHIP_EVENTS: readonly GameEvent[] = [
  { t: 'Yeni bir arkadaş edindin.', fx: { happiness: 8 } },
  { t: 'En yakın arkadaşınla buluştun.', fx: { happiness: 10 } },
  { t: 'Arkadaşın sana ihanet etti.', fx: { happiness: -15 } },
  { t: 'Eski bir arkadaşınla barıştın.', fx: { happiness: 12 } },
  { t: 'Arkadaşlarınla piknik yaptın.', fx: { happiness: 8 } },
  { t: 'Arkadaşın zor bir döneminde yanında oldun.', fx: { happiness: 5, smarts: 2 } },
  { t: 'Arkadaşın şehir değiştirdi. Üzüldün.', fx: { happiness: -8 } },
  { t: 'Arkadaş grubuyla yılbaşı partisi yaptın.', fx: { happiness: 10 } },
  { t: 'Bir arkadaşınla bozuştun.', fx: { happiness: -10 } },
  { t: 'Arkadaşın sana borç para istedi.', fx: { happiness: -3, money: -1000 } },
];

export const FAMILY_EVENTS: readonly GameEvent[] = [
  { t: 'Çocuğun ilk adımını attı!', fx: { happiness: 15 } },
  { t: 'Çocuğun okula başladı.', fx: { happiness: 10 } },
  { t: 'Çocuğunla kaliteli zaman geçirdin.', fx: { happiness: 8 } },
  { t: 'Çocuğun hasta oldu. Endişelendin.', fx: { happiness: -8, health: -3, money: -1000 } },
  { t: 'Annen/baban seni ziyarete geldi.', fx: { happiness: 10 } },
  { t: 'Anne/babanın sağlık durumu kötüleşti.', fx: { happiness: -12 } },
  { t: 'Kardeşinle tartıştın.', fx: { happiness: -5 } },
  { t: 'Aile toplantısında güzel vakit geçirdin.', fx: { happiness: 10 } },
  { t: 'Çocuğun sınavda başarılı oldu!', fx: { happiness: 12, smarts: 2 } },
  { t: 'Anne/baban vefat etti. 😢', fx: { happiness: -25 } },
  { t: 'Kardeşinle barıştın.', fx: { happiness: 8 } },
  { t: 'Çocuğun üniversiteye başladı!', fx: { happiness: 15, money: -10000 } },
];
