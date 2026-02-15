import type { GameEvent } from '../types';

export const BABY_EVENTS: readonly GameEvent[] = [
  // ── Mevcut olaylar (10) ──────────────────────────────────────────────
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

  // ── Yeni olaylar (14) ────────────────────────────────────────────────
  // Hastalıklar
  { t: 'Kulak enfeksiyonu geçirdin.', fx: { health: -12, happiness: -5 } },
  { t: 'Ateşin çıktı, bütün gece uyuyamadın.', fx: { health: -8, happiness: -3 } },
  { t: 'Alerji tepkimesi gösterdin, hastaneye gittiniz.', fx: { health: -15, money: -500 } },

  // İlk kelime varyantları
  { t: 'İlk kelimeni söyledin: "Baba"', fx: { happiness: 12, smarts: 5 } },
  { t: 'İlk kelimeni söyledin: "Su"', fx: { happiness: 8, smarts: 6 } },

  // Kreş
  { t: 'Kreşe başladın, ilk gün çok ağladın.', fx: { happiness: -8 } },
  { t: 'Kreşte diğer çocuklarla oynamayı öğrendin.', fx: { happiness: 10, smarts: 3 } },

  // Kardeş doğumu
  { t: 'Yeni bir kardeşin oldu!', fx: { happiness: 7 } },
  { t: 'Kardeşin doğdu ama ilgi azaldı, kıskançlık hissettin.', fx: { happiness: -10 } },

  // Evcil hayvan etkileşimleri
  { t: 'Evin köpeğiyle oynamayı çok sevdin.', fx: { happiness: 10, health: 2 } },
  { t: 'Balık akvaryumunu izleyerek saatler geçirdin.', fx: { happiness: 5, smarts: 2 } },

  // Yemek tepkileri
  { t: 'İlk defa katı gıda yedin, yüzünü buruşturdun.', fx: { happiness: -3 } },
  { t: 'Annenin çorbasını çok sevdin.', fx: { happiness: 6, health: 3 } },

  // Nadir yüksek etki (%5) — ciddi hastalık
  { t: 'Ağır bir enfeksiyon kaptın, hastanede yattın.', fx: { health: -25, money: -3000, happiness: -10 } },
];

export const CHILD_EVENTS: readonly GameEvent[] = [
  // ── Mevcut olaylar (12) ──────────────────────────────────────────────
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

  // ── Yeni olaylar (24) ────────────────────────────────────────────────
  // Okul yarışmaları
  { t: 'Bilgi yarışmasında okulunu temsil ettin.', fx: { smarts: 6, happiness: 5 } },
  { t: 'Şiir okuma yarışmasında derece yaptın!', fx: { happiness: 10, smarts: 4 } },
  { t: 'Dikteyi hatasız yazdın, öğretmenin seni övdü.', fx: { smarts: 5, happiness: 8 } },
  { t: 'Koşu yarışmasında sonuncu oldun.', fx: { happiness: -8, health: 2 } },

  // Zorbalık varyantları
  { t: 'Büyük sınıftakiler seni itip kaktı.', fx: { happiness: -12, health: -3 } },
  { t: 'Sınıfta seninle dalga geçtiler.', fx: { happiness: -10, looks: -2 } },
  { t: 'Zorbalığa karşı durdun, öğretmenin destek oldu.', fx: { happiness: 5, smarts: 3 } },

  // Hobiler (resim/müzik/spor)
  { t: 'Piyano derslerine başladın.', fx: { smarts: 5, happiness: 3, money: -1000 } },
  { t: 'Yüzme kursuna yazıldın.', fx: { health: 8, happiness: 5, money: -800 } },
  { t: 'Resim yapmayı çok sevdin, duvarlar boyandı!', fx: { happiness: 8, looks: 2 } },
  { t: 'Satranç kulübüne katıldın.', fx: { smarts: 8, happiness: 3 } },
  { t: 'Voleybol oynamayı öğrendin.', fx: { health: 5, happiness: 6 } },

  // Aile olayları
  { t: 'Annen ve baban tartıştı, üzüldün.', fx: { happiness: -12 } },
  { t: 'Büyükannenle yemek yapmayı öğrendin.', fx: { happiness: 10, smarts: 2 } },
  { t: 'Aile toplantısında kuzenlerinle eğlendin.', fx: { happiness: 8 } },
  { t: 'Baban seni balığa götürdü.', fx: { happiness: 10, health: 2 } },

  // Tatiller
  { t: 'Yaz tatilinde denize gittin, çok eğlendin!', fx: { happiness: 12, health: 3 } },
  { t: 'Bayram harçlığı biriktirdin.', fx: { happiness: 5, money: 200 } },
  { t: 'Ramazan Bayramı\'nda çok çikolata yedin, midenin bozuldu.', fx: { happiness: 3, health: -5 } },

  // Arkadaşlıklar
  { t: 'En yakın arkadaşın başka şehre taşındı.', fx: { happiness: -12 } },
  { t: 'Okuldaki herkes senin doğum gününe geldi!', fx: { happiness: 15 } },
  { t: 'Gizli bir kulüp kurdun, çok eğlenceli!', fx: { happiness: 8, smarts: 2 } },

  // Olumsuz / nötr
  { t: 'Televizyon izlerken gözlerin bozuldu, gözlük taktın.', fx: { looks: -3, smarts: 2, money: -500 } },

  // Nadir yüksek etki (%5) — ciddi kaza
  { t: 'Ağaçtan düştün, kolun kırıldı!', fx: { health: -25, happiness: -10, money: -2000 } },
];

export const TEEN_EVENTS: readonly GameEvent[] = [
  // ── Mevcut olaylar (12) ──────────────────────────────────────────────
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

  // ── Yeni olaylar (24) ────────────────────────────────────────────────
  // Romantik ilişkiler
  { t: 'Hoşlandığın kişi sana karşılık verdi!', fx: { happiness: 15, looks: 2 } },
  { t: 'Sevgilinle ayrıldın, çok kötü hissediyorsun.', fx: { happiness: -18 } },
  { t: 'Okul balosuna davet edildin!', fx: { happiness: 12, looks: 3 } },

  // Sosyal medya
  { t: 'Sosyal medyada paylaşımın viral oldu!', fx: { happiness: 10, looks: 3 } },
  { t: 'İnternette siber zorbalığa uğradın.', fx: { happiness: -15, health: -3 } },
  { t: 'Sosyal medya bağımlılığın arttı, notların düştü.', fx: { smarts: -5, happiness: -3 } },

  // Sınav stresi
  { t: 'YKS\'ye hazırlık kursuna başladın.', fx: { smarts: 8, happiness: -5, money: -5000 } },
  { t: 'Deneme sınavında ilk 100\'e girdin!', fx: { smarts: 10, happiness: 12 } },
  { t: 'Sınav gecesi uyuyamadın, sınava yorgun girdin.', fx: { smarts: -3, health: -5, happiness: -8 } },

  // Yarı zamanlı iş
  { t: 'Yaz tatilinde markette çalıştın.', fx: { money: 3000, happiness: -3, smarts: 2 } },
  { t: 'Garsonluk yaparak harçlık kazandın.', fx: { money: 2500, health: -3 } },

  // Ehliyet
  { t: 'Ehliyet sınavını geçtin!', fx: { happiness: 12, smarts: 3 } },
  { t: 'Ehliyet sınavında kaldın, çok sinir oldun.', fx: { happiness: -10, money: -500 } },

  // Akran baskısı
  { t: 'Arkadaşların sigara içmeni istedi, hayır dedin.', fx: { happiness: -3, health: 5 } },
  { t: 'Akran baskısıyla kötü alışkanlıklar edindin.', fx: { health: -10, happiness: -5 } },
  { t: 'Arkadaşlarınla gece geç saatte dışarı çıktın, ailen çok kızdı.', fx: { happiness: -8 } },

  // Kimlik / kişisel gelişim
  { t: 'Felsefe kitapları okumaya başladın, dünyaya bakışın değişti.', fx: { smarts: 8, happiness: 3 } },
  { t: 'Gönüllü çalışmaya katıldın, insanlara yardım ettin.', fx: { happiness: 10, smarts: 3 } },
  { t: 'Günlük tutmaya başladın, kendini daha iyi tanıyorsun.', fx: { happiness: 5, smarts: 4 } },

  // Spor / sağlık
  { t: 'Spor yaparken ayak bileğini burktun.', fx: { health: -10, happiness: -5 } },
  { t: 'Boks kursuna yazıldın, özgüvenin arttı.', fx: { health: 5, happiness: 5, looks: 3 } },

  // Gap year / macera
  { t: 'Yaz kampına katıldın, harika bir deneyim oldu!', fx: { happiness: 12, smarts: 5, health: 3 } },
  { t: 'Arkadaşlarınla kamp yaptın, doğada vakit geçirdin.', fx: { happiness: 8, health: 5 } },

  // Nadir yüksek etki (%5) — miras
  { t: 'Uzak bir akrabandan beklenmedik miras kaldı!', fx: { money: 25000, happiness: 15 } },
];

export const ADULT_EVENTS: readonly GameEvent[] = [
  // ── Mevcut olaylar (12) ──────────────────────────────────────────────
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

  // ── Yeni olaylar (33) ────────────────────────────────────────────────
  // Evlilik varyantları
  { t: 'Evlendin, muhteşem bir düğün oldu!', fx: { happiness: 20, money: -25000 } },
  { t: 'Evlilik yıldönümünü romantik bir şekilde kutladın.', fx: { happiness: 12, money: -2000 } },
  { t: 'Eşinle ilişkiniz gerginleşti.', fx: { happiness: -12 } },
  { t: 'Nikah masrafları cebini yaktı ama çok mutlusun.', fx: { happiness: 15, money: -15000 } },

  // Çocuk
  { t: 'Çocuğun dünyaya geldi!', fx: { happiness: 25, money: -5000 } },
  { t: 'Çocuğun ilk adımlarını attı, çok gururlusun.', fx: { happiness: 15 } },
  { t: 'Çocuğun hasta oldu, hastaneye koştun.', fx: { happiness: -10, health: -3, money: -3000 } },

  // Ev alma
  { t: 'İlk evini satın aldın!', fx: { happiness: 18, money: -50000 } },
  { t: 'Ev kredisi ödemen arttı, bütçen daraldı.', fx: { happiness: -8, money: -5000 } },
  { t: 'Kiracılarınla sorun yaşadın.', fx: { happiness: -10, money: -2000 } },

  // Kariyer değişiklikleri
  { t: 'İşinden istifa edip yeni bir sektöre geçtin.', fx: { happiness: 5, smarts: 5, money: -3000 } },
  { t: 'Patron değişti, iş ortamı kötüleşti.', fx: { happiness: -10 } },
  { t: 'İş yerinde yılın çalışanı seçildin!', fx: { happiness: 12, money: 3000, smarts: 3 } },
  { t: 'Maaşına zam geldi!', fx: { happiness: 8, money: 4000 } },

  // Sağlık korkuları
  { t: 'Sırt ağrıların başladı, fizik tedaviye gidiyorsun.', fx: { health: -8, money: -2000 } },
  { t: 'Check-up yaptırdın, her şey normal çıktı.', fx: { happiness: 5, money: -1000 } },
  { t: 'Kolesterol seviyen yüksek çıktı, diyet yapman gerekiyor.', fx: { health: -5, happiness: -3 } },
  { t: 'Diz ameliyatı oldun, iyileşme süreci uzun sürdü.', fx: { health: -15, money: -8000, happiness: -5 } },

  // Seyahat
  { t: 'Avrupa turuna çıktın, harika bir deneyimdi!', fx: { happiness: 18, smarts: 5, money: -15000 } },
  { t: 'Yurt dışında dil kursuna katıldın.', fx: { smarts: 10, money: -10000, happiness: 8 } },
  { t: 'Hafta sonu kaçamağı yaptın, dinlendin.', fx: { happiness: 8, health: 3, money: -1500 } },

  // Yan iş / girişimcilik
  { t: 'E-ticaret işine girdin, ilk satışını yaptın!', fx: { money: 5000, happiness: 10, smarts: 3 } },
  { t: 'Yan iş girişimin batdı, para kaybettin.', fx: { money: -12000, happiness: -10 } },
  { t: 'Freelance iş aldın, ekstra gelir elde ettin.', fx: { money: 8000, smarts: 3 } },

  // Boşanma
  { t: 'Boşanma davası açıldı, çok yıprandın.', fx: { happiness: -20, money: -20000, health: -5 } },
  { t: 'Boşandıktan sonra yeni bir başlangıç yaptın.', fx: { happiness: 5, money: -5000 } },

  // Terfiler ve işten çıkarmalar
  { t: 'Şirket küçülmeye gitti, işten çıkarıldın.', fx: { happiness: -18, money: -5000 } },
  { t: 'Yönetici pozisyonuna yükseldin!', fx: { happiness: 15, money: 10000, smarts: 5 } },
  { t: 'İş arkadaşın terfi aldı, sen alamadın. Moral bozukluğu.', fx: { happiness: -10 } },

  // Yatırımlar
  { t: 'Borsadaki yatırımların değer kaybetti.', fx: { money: -20000, happiness: -12 } },
  { t: 'Kripto paradan beklenmedik bir kazanç elde ettin!', fx: { money: 30000, happiness: 12 } },

  // Nadir yüksek etki (%5) — ciddi kaza / büyük miras
  { t: 'Ciddi bir trafik kazası geçirdin, ameliyat oldun.', fx: { health: -35, money: -25000, happiness: -15 } },
  { t: 'Akrabandan büyük bir miras kaldı!', fx: { money: 100000, happiness: 20 } },
];

export const ELDER_EVENTS: readonly GameEvent[] = [
  // ── Mevcut olaylar (10) ──────────────────────────────────────────────
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

  // ── Yeni olaylar (17) ────────────────────────────────────────────────
  // Torunlar
  { t: 'Torunun okula başladı, çok gururlusun.', fx: { happiness: 12 } },
  { t: 'Torununa bisiklet sürmeyi öğrettin.', fx: { happiness: 10, health: 2 } },
  { t: 'Torunun seni ziyarete geldi, harika bir gün geçirdiniz.', fx: { happiness: 15 } },

  // Emeklilik hobileri
  { t: 'Yağlı boya resim yapmaya başladın.', fx: { happiness: 8, smarts: 3 } },
  { t: 'Halk eğitim merkezinde el sanatları kursuna katıldın.', fx: { happiness: 8, smarts: 2 } },
  { t: 'Balık tutmayı hobi edindin, göl kenarında huzur buldun.', fx: { happiness: 10, health: 2 } },
  { t: 'Tavla turnuvasında birinci oldun!', fx: { happiness: 10, smarts: 3 } },

  // Sağlık bozulması varyantları
  { t: 'Diyabet teşhisi konuldu, ilaç kullanmaya başladın.', fx: { health: -12, happiness: -5, money: -2000 } },
  { t: 'Kalp ritim bozukluğu saptandı.', fx: { health: -15, happiness: -8 } },
  { t: 'Eklem ağrıların arttı, yürümekte zorlanıyorsun.', fx: { health: -10, happiness: -5 } },
  { t: 'İşitme kaybı başladı, cihaz aldın.', fx: { health: -5, money: -3000, happiness: -3 } },

  // Miras / hikaye
  { t: 'Hatıratını yazmaya başladın.', fx: { happiness: 8, smarts: 5 } },
  { t: 'Aile bireylerini toplayıp hayat hikayeni anlattın.', fx: { happiness: 12 } },

  // Seyahat
  { t: 'Hac\'a gittin, manevi huzur buldun.', fx: { happiness: 18, health: -3, money: -15000 } },
  { t: 'Eski bir arkadaşınla yıllar sonra buluştun.', fx: { happiness: 12 } },

  // Arkadaş kaybı
  { t: 'Yakın bir arkadaşın vefat etti, çok üzüldün.', fx: { happiness: -18, health: -5 } },

  // Nadir yüksek etki (%5) — ciddi sağlık krizi
  { t: 'Kalp krizi geçirdin, yoğun bakıma kaldırıldın!', fx: { health: -30, happiness: -15, money: -20000 } },
];

/** Yaşa uygun olay havuzunu getir */
export const getEventsForAge = (age: number): readonly GameEvent[] => {
  if (age <= 3) return BABY_EVENTS;
  if (age <= 12) return CHILD_EVENTS;
  if (age <= 17) return TEEN_EVENTS;
  if (age <= 64) return ADULT_EVENTS;
  return ELDER_EVENTS;
};
