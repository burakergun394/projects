import type { Job, EducationLevel } from '../types';

export const JOBS: readonly Job[] = [
  // ── ENTRY LEVEL (diploma yok, 16+) ──────────────────────────────────────
  { id: 'garson', title: 'Garson', sector: 'entry', baseSalary: 11000, minSmarts: 0, minEducation: 'none', minAge: 16, maxAge: 45, promotionChain: ['garson_sr', 'restoran_mudur'], experienceYearsForPromo: 3, fireChance: 12, respectGain: 5 },
  { id: 'garson_sr', title: 'Kıdemli Garson', sector: 'entry', baseSalary: 14000, minSmarts: 10, minEducation: 'none', minAge: 18, maxAge: 50, promotionChain: ['restoran_mudur'], experienceYearsForPromo: 3, fireChance: 10, respectGain: 8 },
  { id: 'restoran_mudur', title: 'Restoran Müdürü', sector: 'entry', baseSalary: 25000, minSmarts: 20, minEducation: 'none', minAge: 22, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 8, respectGain: 15 },

  { id: 'kasiyer', title: 'Kasiyer', sector: 'entry', baseSalary: 11500, minSmarts: 0, minEducation: 'none', minAge: 16, maxAge: 45, promotionChain: ['reyon_sorumlu', 'magaza_mudur'], experienceYearsForPromo: 3, fireChance: 12, respectGain: 5 },
  { id: 'reyon_sorumlo', title: 'Reyon Sorumlusu', sector: 'entry', baseSalary: 15000, minSmarts: 10, minEducation: 'none', minAge: 18, maxAge: 50, promotionChain: ['magaza_mudur'], experienceYearsForPromo: 3, fireChance: 10, respectGain: 10 },
  { id: 'magaza_mudur', title: 'Mağaza Müdürü', sector: 'entry', baseSalary: 28000, minSmarts: 25, minEducation: 'none', minAge: 24, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 8, respectGain: 18 },

  { id: 'kurye', title: 'Kurye', sector: 'entry', baseSalary: 13000, minSmarts: 0, minEducation: 'none', minAge: 16, maxAge: 45, promotionChain: ['lojistik_sorumlu'], experienceYearsForPromo: 3, fireChance: 15, respectGain: 4 },
  { id: 'lojistik_sorumlo', title: 'Lojistik Sorumlusu', sector: 'entry', baseSalary: 18000, minSmarts: 15, minEducation: 'none', minAge: 20, maxAge: 55, promotionChain: [], experienceYearsForPromo: 0, fireChance: 10, respectGain: 12 },

  { id: 'temizlikci', title: 'Temizlikçi', sector: 'entry', baseSalary: 11000, minSmarts: 0, minEducation: 'none', minAge: 16, maxAge: 55, promotionChain: ['bina_yoneticisi'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 3 },
  { id: 'bina_yoneticisi', title: 'Bina Yöneticisi', sector: 'entry', baseSalary: 16000, minSmarts: 10, minEducation: 'none', minAge: 22, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 8, respectGain: 10 },

  // ── TRADE / SKILLED (lise, 18+) ─────────────────────────────────────────
  { id: 'asci', title: 'Aşçı', sector: 'trade', baseSalary: 14000, minSmarts: 10, minEducation: 'lise', minAge: 18, maxAge: 50, promotionChain: ['sef', 'bas_sef'], experienceYearsForPromo: 3, fireChance: 10, respectGain: 8 },
  { id: 'sef', title: 'Şef', sector: 'trade', baseSalary: 22000, minSmarts: 20, minEducation: 'lise', minAge: 22, maxAge: 55, promotionChain: ['bas_sef'], experienceYearsForPromo: 4, fireChance: 8, respectGain: 15 },
  { id: 'bas_sef', title: 'Baş Şef', sector: 'trade', baseSalary: 35000, minSmarts: 30, minEducation: 'lise', minAge: 26, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 5, respectGain: 22 },

  { id: 'elektrikci', title: 'Elektrikçi', sector: 'trade', baseSalary: 16000, minSmarts: 15, minEducation: 'lise', minAge: 18, maxAge: 50, promotionChain: ['usta_elektrikci', 'teknik_mudur'], experienceYearsForPromo: 3, fireChance: 8, respectGain: 10 },
  { id: 'usta_elektrikci', title: 'Usta Elektrikçi', sector: 'trade', baseSalary: 24000, minSmarts: 25, minEducation: 'lise', minAge: 22, maxAge: 55, promotionChain: ['teknik_mudur'], experienceYearsForPromo: 4, fireChance: 6, respectGain: 15 },
  { id: 'teknik_mudur', title: 'Teknik Müdür', sector: 'trade', baseSalary: 35000, minSmarts: 35, minEducation: 'lise', minAge: 28, maxAge: 60, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 5, respectGain: 22 },

  { id: 'sofor', title: 'Şoför', sector: 'trade', baseSalary: 15000, minSmarts: 5, minEducation: 'lise', minAge: 18, maxAge: 55, promotionChain: ['filo_sorumlu'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 7 },
  { id: 'filo_sorumlo', title: 'Filo Sorumlusu', sector: 'trade', baseSalary: 22000, minSmarts: 20, minEducation: 'lise', minAge: 24, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 8, respectGain: 14 },

  { id: 'teknisyen', title: 'Teknisyen', sector: 'trade', baseSalary: 17000, minSmarts: 15, minEducation: 'lise', minAge: 18, maxAge: 50, promotionChain: ['sr_teknisyen', 'teknik_sef'], experienceYearsForPromo: 3, fireChance: 8, respectGain: 10 },
  { id: 'sr_teknisyen', title: 'Kıdemli Teknisyen', sector: 'trade', baseSalary: 24000, minSmarts: 25, minEducation: 'lise', minAge: 22, maxAge: 55, promotionChain: ['teknik_sef'], experienceYearsForPromo: 4, fireChance: 6, respectGain: 15 },
  { id: 'teknik_sef', title: 'Teknik Şef', sector: 'trade', baseSalary: 32000, minSmarts: 35, minEducation: 'lise', minAge: 26, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 5, respectGain: 20 },

  { id: 'grafiker', title: 'Grafik Tasarımcı', sector: 'trade', baseSalary: 18000, minSmarts: 20, minEducation: 'lise', minAge: 18, maxAge: 45, promotionChain: ['sr_grafiker', 'kreatif_direktor'], experienceYearsForPromo: 3, fireChance: 12, respectGain: 12 },
  { id: 'sr_grafiker', title: 'Kıdemli Tasarımcı', sector: 'trade', baseSalary: 28000, minSmarts: 30, minEducation: 'lise', minAge: 22, maxAge: 55, promotionChain: ['kreatif_direktor'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 18 },
  { id: 'kreatif_direktor', title: 'Kreatif Direktör', sector: 'trade', baseSalary: 45000, minSmarts: 40, minEducation: 'lise', minAge: 28, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 8, respectGain: 25 },

  // ── PUBLIC SECTOR (üniversite, 22+) ─────────────────────────────────────
  { id: 'memur', title: 'Memur', sector: 'public', baseSalary: 22000, minSmarts: 30, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['memur_sef', 'mudur_yrd_kamu', 'mudur_kamu'], experienceYearsForPromo: 4, fireChance: 3, respectGain: 12 },
  { id: 'memur_sef', title: 'Şef (Kamu)', sector: 'public', baseSalary: 28000, minSmarts: 35, minEducation: 'universite', minAge: 26, maxAge: 55, promotionChain: ['mudur_yrd_kamu', 'mudur_kamu'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 16 },
  { id: 'mudur_yrd_kamu', title: 'Müdür Yardımcısı (Kamu)', sector: 'public', baseSalary: 35000, minSmarts: 40, minEducation: 'universite', minAge: 30, maxAge: 60, promotionChain: ['mudur_kamu'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 22 },
  { id: 'mudur_kamu', title: 'Müdür (Kamu)', sector: 'public', baseSalary: 45000, minSmarts: 50, minEducation: 'universite', minAge: 35, maxAge: 65, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 28 },

  { id: 'ogretmen', title: 'Öğretmen', sector: 'public', baseSalary: 24000, minSmarts: 35, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['sr_ogretmen', 'okul_mudur_yrd', 'okul_mudur'], experienceYearsForPromo: 5, fireChance: 3, respectGain: 15 },
  { id: 'sr_ogretmen', title: 'Kıdemli Öğretmen', sector: 'public', baseSalary: 30000, minSmarts: 40, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['okul_mudur_yrd', 'okul_mudur'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 18 },
  { id: 'okul_mudur_yrd', title: 'Müdür Yardımcısı (Okul)', sector: 'public', baseSalary: 36000, minSmarts: 45, minEducation: 'universite', minAge: 32, maxAge: 60, promotionChain: ['okul_mudur'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 22 },
  { id: 'okul_mudur', title: 'Okul Müdürü', sector: 'public', baseSalary: 42000, minSmarts: 50, minEducation: 'universite', minAge: 38, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 2, respectGain: 28 },

  { id: 'hemsire', title: 'Hemşire', sector: 'public', baseSalary: 23000, minSmarts: 30, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['sr_hemsire', 'bashemsire'], experienceYearsForPromo: 4, fireChance: 3, respectGain: 14 },
  { id: 'sr_hemsire', title: 'Kıdemli Hemşire', sector: 'public', baseSalary: 30000, minSmarts: 35, minEducation: 'universite', minAge: 26, maxAge: 55, promotionChain: ['bashemsire'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 18 },
  { id: 'bashemsire', title: 'Başhemşire', sector: 'public', baseSalary: 38000, minSmarts: 45, minEducation: 'universite', minAge: 32, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 2, respectGain: 24 },

  { id: 'polis', title: 'Polis', sector: 'public', baseSalary: 25000, minSmarts: 25, minEducation: 'universite', minAge: 22, maxAge: 40, promotionChain: ['komiser_yrd', 'komiser'], experienceYearsForPromo: 5, fireChance: 5, respectGain: 16 },
  { id: 'komiser_yrd', title: 'Komiser Yardımcısı', sector: 'public', baseSalary: 32000, minSmarts: 35, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['komiser'], experienceYearsForPromo: 5, fireChance: 3, respectGain: 22 },
  { id: 'komiser', title: 'Komiser', sector: 'public', baseSalary: 40000, minSmarts: 45, minEducation: 'universite', minAge: 34, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 2, respectGain: 28 },

  // ── PRIVATE SECTOR (üniversite, 22+) ────────────────────────────────────
  { id: 'yazilimci', title: 'Yazılımcı', sector: 'private', baseSalary: 35000, minSmarts: 50, minEducation: 'universite', minAge: 22, maxAge: 40, promotionChain: ['sr_yazilimci', 'takim_lideri', 'cto'], experienceYearsForPromo: 3, fireChance: 10, respectGain: 18 },
  { id: 'sr_yazilimci', title: 'Kıdemli Yazılımcı', sector: 'private', baseSalary: 55000, minSmarts: 60, minEducation: 'universite', minAge: 25, maxAge: 50, promotionChain: ['takim_lideri', 'cto'], experienceYearsForPromo: 3, fireChance: 8, respectGain: 24 },
  { id: 'takim_lideri', title: 'Takım Lideri', sector: 'private', baseSalary: 70000, minSmarts: 65, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['cto'], experienceYearsForPromo: 4, fireChance: 8, respectGain: 30 },

  { id: 'muhasebeci', title: 'Muhasebeci', sector: 'private', baseSalary: 25000, minSmarts: 35, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['mali_musavir', 'finans_mudur', 'cfo'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 14 },
  { id: 'mali_musavir', title: 'Mali Müşavir', sector: 'private', baseSalary: 40000, minSmarts: 45, minEducation: 'universite', minAge: 26, maxAge: 55, promotionChain: ['finans_mudur', 'cfo'], experienceYearsForPromo: 4, fireChance: 8, respectGain: 20 },
  { id: 'finans_mudur', title: 'Finans Müdürü', sector: 'private', baseSalary: 65000, minSmarts: 55, minEducation: 'universite', minAge: 30, maxAge: 60, promotionChain: ['cfo'], experienceYearsForPromo: 5, fireChance: 8, respectGain: 28 },

  { id: 'pazarlamaci', title: 'Pazarlamacı', sector: 'private', baseSalary: 22000, minSmarts: 30, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['pazarlama_uzman', 'pazarlama_mudur'], experienceYearsForPromo: 3, fireChance: 12, respectGain: 12 },
  { id: 'pazarlama_uzman', title: 'Pazarlama Uzmanı', sector: 'private', baseSalary: 35000, minSmarts: 40, minEducation: 'universite', minAge: 25, maxAge: 55, promotionChain: ['pazarlama_mudur'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 18 },
  { id: 'pazarlama_mudur', title: 'Pazarlama Müdürü', sector: 'private', baseSalary: 55000, minSmarts: 50, minEducation: 'universite', minAge: 30, maxAge: 60, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 8, respectGain: 26 },

  { id: 'satis_temsilci', title: 'Satış Temsilcisi', sector: 'private', baseSalary: 20000, minSmarts: 25, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['bolge_mudur', 'satis_direktor'], experienceYearsForPromo: 3, fireChance: 15, respectGain: 10 },
  { id: 'bolge_mudur', title: 'Bölge Müdürü', sector: 'private', baseSalary: 40000, minSmarts: 40, minEducation: 'universite', minAge: 26, maxAge: 55, promotionChain: ['satis_direktor'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 22 },
  { id: 'satis_direktor', title: 'Satış Direktörü', sector: 'private', baseSalary: 70000, minSmarts: 55, minEducation: 'universite', minAge: 32, maxAge: 60, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 8, respectGain: 30 },

  { id: 'ik_uzman', title: 'İnsan Kaynakları Uzmanı', sector: 'private', baseSalary: 24000, minSmarts: 30, minEducation: 'universite', minAge: 22, maxAge: 45, promotionChain: ['ik_mudur'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 12 },
  { id: 'ik_mudur', title: 'İK Müdürü', sector: 'private', baseSalary: 45000, minSmarts: 45, minEducation: 'universite', minAge: 28, maxAge: 60, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 8, respectGain: 24 },

  // ── PROFESSIONAL (üniversite + uzmanlık, 24+) ──────────────────────────
  { id: 'avukat', title: 'Avukat', sector: 'professional', baseSalary: 30000, minSmarts: 55, minEducation: 'universite', minAge: 24, maxAge: 45, promotionChain: ['sr_avukat', 'ortak_avukat'], experienceYearsForPromo: 4, fireChance: 8, respectGain: 20 },
  { id: 'sr_avukat', title: 'Kıdemli Avukat', sector: 'professional', baseSalary: 50000, minSmarts: 65, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['ortak_avukat'], experienceYearsForPromo: 5, fireChance: 5, respectGain: 28 },
  { id: 'ortak_avukat', title: 'Ortak Avukat', sector: 'professional', baseSalary: 80000, minSmarts: 70, minEducation: 'universite', minAge: 34, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 3, respectGain: 35 },

  { id: 'doktor', title: 'Doktor', sector: 'professional', baseSalary: 40000, minSmarts: 60, minEducation: 'universite', minAge: 24, maxAge: 45, promotionChain: ['uzman_doktor', 'bashekim'], experienceYearsForPromo: 4, fireChance: 3, respectGain: 25 },
  { id: 'uzman_doktor', title: 'Uzman Doktor', sector: 'professional', baseSalary: 65000, minSmarts: 70, minEducation: 'universite', minAge: 28, maxAge: 60, promotionChain: ['bashekim'], experienceYearsForPromo: 5, fireChance: 2, respectGain: 32 },
  { id: 'bashekim', title: 'Başhekim', sector: 'professional', baseSalary: 90000, minSmarts: 75, minEducation: 'universite', minAge: 36, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 2, respectGain: 40 },

  { id: 'mimar', title: 'Mimar', sector: 'professional', baseSalary: 28000, minSmarts: 50, minEducation: 'universite', minAge: 24, maxAge: 45, promotionChain: ['sr_mimar', 'proje_direktor'], experienceYearsForPromo: 4, fireChance: 10, respectGain: 18 },
  { id: 'sr_mimar', title: 'Kıdemli Mimar', sector: 'professional', baseSalary: 45000, minSmarts: 60, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['proje_direktor'], experienceYearsForPromo: 5, fireChance: 8, respectGain: 25 },
  { id: 'proje_direktor', title: 'Proje Direktörü', sector: 'professional', baseSalary: 70000, minSmarts: 65, minEducation: 'universite', minAge: 34, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 5, respectGain: 32 },

  { id: 'eczaci', title: 'Eczacı', sector: 'professional', baseSalary: 30000, minSmarts: 50, minEducation: 'universite', minAge: 24, maxAge: 45, promotionChain: ['eczane_sahibi'], experienceYearsForPromo: 5, fireChance: 5, respectGain: 20 },
  { id: 'eczane_sahibi', title: 'Eczane Sahibi', sector: 'professional', baseSalary: 55000, minSmarts: 55, minEducation: 'universite', minAge: 30, maxAge: 65, promotionChain: [], experienceYearsForPromo: 0, fireChance: 3, respectGain: 28 },

  { id: 'muhendis', title: 'Mühendis', sector: 'professional', baseSalary: 32000, minSmarts: 55, minEducation: 'universite', minAge: 24, maxAge: 45, promotionChain: ['sr_muhendis', 'muhendislik_mudur'], experienceYearsForPromo: 3, fireChance: 8, respectGain: 20 },
  { id: 'sr_muhendis', title: 'Kıdemli Mühendis', sector: 'professional', baseSalary: 50000, minSmarts: 65, minEducation: 'universite', minAge: 28, maxAge: 55, promotionChain: ['muhendislik_mudur'], experienceYearsForPromo: 4, fireChance: 6, respectGain: 28 },
  { id: 'muhendislik_mudur', title: 'Mühendislik Müdürü', sector: 'professional', baseSalary: 75000, minSmarts: 70, minEducation: 'universite', minAge: 34, maxAge: 65, promotionChain: ['genel_mudur'], experienceYearsForPromo: 5, fireChance: 5, respectGain: 35 },

  { id: 'pilot', title: 'Pilot', sector: 'professional', baseSalary: 45000, minSmarts: 55, minEducation: 'universite', minAge: 24, maxAge: 40, promotionChain: ['kaptan_pilot', 'bas_pilot'], experienceYearsForPromo: 4, fireChance: 5, respectGain: 25 },
  { id: 'kaptan_pilot', title: 'Kaptan Pilot', sector: 'professional', baseSalary: 75000, minSmarts: 65, minEducation: 'universite', minAge: 30, maxAge: 55, promotionChain: ['bas_pilot'], experienceYearsForPromo: 5, fireChance: 3, respectGain: 32 },
  { id: 'bas_pilot', title: 'Baş Pilot', sector: 'professional', baseSalary: 100000, minSmarts: 70, minEducation: 'universite', minAge: 36, maxAge: 60, promotionChain: [], experienceYearsForPromo: 0, fireChance: 2, respectGain: 38 },

  { id: 'akademisyen', title: 'Akademisyen', sector: 'professional', baseSalary: 28000, minSmarts: 65, minEducation: 'yuksek_lisans', minAge: 24, maxAge: 40, promotionChain: ['docent', 'profesor'], experienceYearsForPromo: 5, fireChance: 3, respectGain: 20 },
  { id: 'docent', title: 'Doçent', sector: 'professional', baseSalary: 38000, minSmarts: 75, minEducation: 'yuksek_lisans', minAge: 30, maxAge: 55, promotionChain: ['profesor'], experienceYearsForPromo: 6, fireChance: 2, respectGain: 30 },
  { id: 'profesor', title: 'Profesör', sector: 'professional', baseSalary: 50000, minSmarts: 85, minEducation: 'doktora', minAge: 38, maxAge: 70, promotionChain: [], experienceYearsForPromo: 0, fireChance: 1, respectGain: 40 },

  // ── EXECUTIVE (sadece terfi ile ulaşılır) ───────────────────────────────
  { id: 'genel_mudur', title: 'Genel Müdür', sector: 'executive', baseSalary: 90000, minSmarts: 70, minEducation: 'universite', minAge: 35, maxAge: 65, promotionChain: ['ceo'], experienceYearsForPromo: 5, fireChance: 10, respectGain: 40 },
  { id: 'cto', title: 'CTO', sector: 'executive', baseSalary: 120000, minSmarts: 75, minEducation: 'universite', minAge: 32, maxAge: 65, promotionChain: ['ceo'], experienceYearsForPromo: 5, fireChance: 10, respectGain: 42 },
  { id: 'cfo', title: 'CFO', sector: 'executive', baseSalary: 110000, minSmarts: 70, minEducation: 'universite', minAge: 34, maxAge: 65, promotionChain: ['ceo'], experienceYearsForPromo: 5, fireChance: 10, respectGain: 42 },
  { id: 'ceo', title: 'CEO', sector: 'executive', baseSalary: 150000, minSmarts: 80, minEducation: 'universite', minAge: 38, maxAge: 70, promotionChain: [], experienceYearsForPromo: 0, fireChance: 12, respectGain: 50 },
] as const;

export const SECTOR_LABELS: Record<Job['sector'], string> = {
  entry: '🏪 Giriş Seviyesi',
  trade: '🔧 Vasıflı / Esnaf',
  public: '🏛️ Kamu Sektörü',
  private: '🏢 Özel Sektör',
  professional: '⚕️ Profesyonel',
  executive: '👔 Üst Yönetim',
};

export const SECTOR_ORDER: readonly Job['sector'][] = [
  'entry', 'trade', 'public', 'private', 'professional', 'executive',
] as const;

/** Vergi hesaplama (aylık maaşa göre) */
export const getTaxRate = (monthlySalary: number): number => {
  if (monthlySalary <= 15000) return 0.15;
  if (monthlySalary <= 30000) return 0.20;
  if (monthlySalary <= 50000) return 0.27;
  if (monthlySalary <= 80000) return 0.35;
  return 0.40;
};

/** Eğitim seviyesi kontrolü */
export const meetsEducationReq = (education: string[], req: EducationLevel): boolean => {
  if (req === 'none') return true;
  if (req === 'ilkokul') return education.includes('İlkokul');
  if (req === 'ortaokul') return education.includes('Ortaokul');
  if (req === 'lise') return education.includes('Lise');
  if (req === 'universite') return education.some((e) => e.startsWith('Üniversite'));
  if (req === 'yuksek_lisans') return education.includes('Yüksek Lisans');
  if (req === 'doktora') return education.includes('Doktora');
  return false;
};

export const getJobById = (id: string): Job | undefined =>
  JOBS.find((j) => j.id === id);
