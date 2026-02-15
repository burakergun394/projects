import type { Department, Faculty } from '../types';

export const DEPARTMENTS: readonly Department[] = [
  // ── TIER 1 — Elite (440+) ─────────────────────────────────────────────
  {
    id: 'tip_devlet',
    name: 'Tıp',
    faculty: 'Tıp',
    minScore: 460,
    type: 'devlet',
    annualCost: 2000,
    totalYears: 6,
    smartsGain: 30,
    prestige: 10,
    unlockedCareers: ['doktor', 'uzman_doktor', 'bashekim'],
    description: "Türkiye'nin en zor bölümü. 6 yıllık eğitim.",
  },
  {
    id: 'hukuk_devlet',
    name: 'Hukuk',
    faculty: 'Hukuk',
    minScore: 440,
    type: 'devlet',
    annualCost: 1500,
    totalYears: 4,
    smartsGain: 22,
    prestige: 9,
    unlockedCareers: ['avukat', 'sr_avukat', 'ortak_avukat'],
    description: 'Adaletin kapısı. Avukatlık stajı ayrıca gerekir.',
  },

  // ── TIER 2 — High (380+) ──────────────────────────────────────────────
  {
    id: 'bilgisayar_devlet',
    name: 'Bilgisayar Mühendisliği',
    faculty: 'Mühendislik',
    minScore: 410,
    type: 'devlet',
    annualCost: 2000,
    totalYears: 4,
    smartsGain: 24,
    prestige: 9,
    unlockedCareers: ['yazilimci', 'sr_yazilimci', 'takim_lideri', 'cto'],
    description: 'Yazılım ve teknoloji dünyasının kapısı.',
  },
  {
    id: 'eczacilik_devlet',
    name: 'Eczacılık',
    faculty: 'Eczacılık',
    minScore: 400,
    type: 'devlet',
    annualCost: 1800,
    totalYears: 5,
    smartsGain: 22,
    prestige: 8,
    unlockedCareers: ['eczaci', 'eczane_sahibi'],
    description: 'İlaç bilimi ve eczacılık mesleği.',
  },
  {
    id: 'pilotaj_devlet',
    name: 'Pilotaj',
    faculty: 'Havacılık',
    minScore: 400,
    type: 'devlet',
    annualCost: 3000,
    totalYears: 4,
    smartsGain: 18,
    prestige: 8,
    unlockedCareers: ['pilot', 'kaptan_pilot', 'bas_pilot'],
    description: 'Gökyüzünün kapısı. Zorlu fiziksel testler var.',
  },
  {
    id: 'elektrik_devlet',
    name: 'Elektrik-Elektronik Mühendisliği',
    faculty: 'Mühendislik',
    minScore: 390,
    type: 'devlet',
    annualCost: 2000,
    totalYears: 4,
    smartsGain: 22,
    prestige: 8,
    unlockedCareers: ['muhendis', 'sr_muhendis', 'muhendislik_mudur'],
    description: 'Elektrik ve elektronik sistemlerin uzmanı.',
  },
  {
    id: 'mimarlik_devlet',
    name: 'Mimarlık',
    faculty: 'Mimarlık',
    minScore: 380,
    type: 'devlet',
    annualCost: 1800,
    totalYears: 4,
    smartsGain: 20,
    prestige: 8,
    unlockedCareers: ['mimar', 'sr_mimar', 'proje_direktor'],
    description: 'Yapı tasarımı ve şehir planlaması.',
  },

  // ── TIER 3 — Good (300+) ──────────────────────────────────────────────
  {
    id: 'polis_akademisi',
    name: 'Polis Akademisi',
    faculty: 'Sağlık Bilimleri',
    minScore: 330,
    type: 'devlet',
    annualCost: 0,
    totalYears: 4,
    smartsGain: 14,
    prestige: 7,
    unlockedCareers: ['polis', 'komiser_yrd', 'komiser'],
    description: 'Ücretsiz eğitim + burs. Mezuniyet sonrası atama garantisi.',
  },
  {
    id: 'isletme_devlet',
    name: 'İşletme',
    faculty: 'İşletme',
    minScore: 320,
    type: 'devlet',
    annualCost: 1500,
    totalYears: 4,
    smartsGain: 18,
    prestige: 7,
    unlockedCareers: ['muhasebeci', 'mali_musavir', 'finans_mudur', 'cfo', 'pazarlamaci', 'pazarlama_uzman', 'pazarlama_mudur', 'ik_uzman', 'ik_mudur'],
    description: 'İş dünyasının temel bölümü. Geniş kariyer seçenekleri.',
  },
  {
    id: 'iletisim_devlet',
    name: 'İletişim Fakültesi',
    faculty: 'İletişim',
    minScore: 310,
    type: 'devlet',
    annualCost: 1500,
    totalYears: 4,
    smartsGain: 16,
    prestige: 6,
    unlockedCareers: ['pazarlamaci', 'pazarlama_uzman', 'satis_temsilci'],
    description: 'Medya, gazetecilik ve iletişim.',
  },
  {
    id: 'hemsirelik_devlet',
    name: 'Hemşirelik',
    faculty: 'Sağlık Bilimleri',
    minScore: 300,
    type: 'devlet',
    annualCost: 1500,
    totalYears: 4,
    smartsGain: 16,
    prestige: 6,
    unlockedCareers: ['hemsire', 'sr_hemsire', 'bashemsire'],
    description: 'Sağlık sektörünün bel kemiği.',
  },
  {
    id: 'egitim_devlet',
    name: 'Eğitim Fakültesi',
    faculty: 'Eğitim',
    minScore: 300,
    type: 'devlet',
    annualCost: 1200,
    totalYears: 4,
    smartsGain: 16,
    prestige: 6,
    unlockedCareers: ['ogretmen', 'sr_ogretmen', 'okul_mudur_yrd', 'okul_mudur'],
    description: 'Geleceğin öğretmenlerini yetiştiren bölüm.',
  },

  // ── TIER 4 — Average (200+) ───────────────────────────────────────────
  {
    id: 'guzel_sanatlar_devlet',
    name: 'Güzel Sanatlar',
    faculty: 'Güzel Sanatlar',
    minScore: 220,
    type: 'devlet',
    annualCost: 1500,
    totalYears: 4,
    smartsGain: 12,
    prestige: 5,
    unlockedCareers: ['grafiker', 'sr_grafiker', 'kreatif_direktor'],
    description: 'Sanat ve tasarım dünyası.',
  },
  {
    id: 'fen_edebiyat_devlet',
    name: 'Fen-Edebiyat',
    faculty: 'Fen-Edebiyat',
    minScore: 200,
    type: 'devlet',
    annualCost: 1200,
    totalYears: 4,
    smartsGain: 14,
    prestige: 5,
    unlockedCareers: ['ogretmen', 'akademisyen'],
    description: 'Temel bilimler ve edebiyat. Akademik kariyer için temel.',
  },

  // ── ÖZEL ÜNİVERSİTELER ────────────────────────────────────────────────
  {
    id: 'tip_ozel',
    name: 'Tıp (Özel)',
    faculty: 'Tıp',
    minScore: 380,
    type: 'ozel',
    annualCost: 180_000,
    totalYears: 6,
    smartsGain: 28,
    prestige: 8,
    unlockedCareers: ['doktor', 'uzman_doktor', 'bashekim'],
    description: 'Özel üniversitede tıp eğitimi. Yüksek ücret.',
  },
  {
    id: 'hukuk_ozel',
    name: 'Hukuk (Özel)',
    faculty: 'Hukuk',
    minScore: 340,
    type: 'ozel',
    annualCost: 120_000,
    totalYears: 4,
    smartsGain: 20,
    prestige: 7,
    unlockedCareers: ['avukat', 'sr_avukat', 'ortak_avukat'],
    description: 'Özel üniversitede hukuk eğitimi.',
  },
  {
    id: 'muhendislik_ozel',
    name: 'Mühendislik (Özel)',
    faculty: 'Mühendislik',
    minScore: 280,
    type: 'ozel',
    annualCost: 100_000,
    totalYears: 4,
    smartsGain: 20,
    prestige: 6,
    unlockedCareers: ['yazilimci', 'sr_yazilimci', 'takim_lideri', 'muhendis', 'sr_muhendis', 'muhendislik_mudur'],
    description: 'Özel üniversitede mühendislik eğitimi.',
  },
  {
    id: 'isletme_ozel',
    name: 'İşletme (Özel)',
    faculty: 'İşletme',
    minScore: 200,
    type: 'ozel',
    annualCost: 80_000,
    totalYears: 4,
    smartsGain: 16,
    prestige: 5,
    unlockedCareers: ['muhasebeci', 'mali_musavir', 'finans_mudur', 'cfo', 'pazarlamaci', 'pazarlama_uzman', 'pazarlama_mudur', 'ik_uzman', 'ik_mudur'],
    description: 'Özel üniversitede işletme eğitimi.',
  },
  {
    id: 'iletisim_ozel',
    name: 'İletişim (Özel)',
    faculty: 'İletişim',
    minScore: 160,
    type: 'ozel',
    annualCost: 70_000,
    totalYears: 4,
    smartsGain: 14,
    prestige: 4,
    unlockedCareers: ['pazarlamaci', 'pazarlama_uzman', 'satis_temsilci'],
    description: 'Özel üniversitede iletişim eğitimi.',
  },
] as const;

/** Bölüm ID'ye göre bul */
export const getDepartmentById = (id: string): Department | undefined =>
  DEPARTMENTS.find((d) => d.id === id);

/** Sınav puanına göre açık bölümleri getir */
export const getAvailableDepartments = (score: number): Department[] =>
  DEPARTMENTS.filter((d) => score >= d.minScore)
    .sort((a, b) => b.minScore - a.minScore);

/** Tüm bölümleri puana göre sırala (yüksekten düşüğe) */
export const getAllDepartmentsSorted = (): readonly Department[] =>
  [...DEPARTMENTS].sort((a, b) => b.minScore - a.minScore);

/** Herhangi bir bölümün unlockedCareers listesinde olan iş ID'leri */
const _allDeptCareers = new Set(
  DEPARTMENTS.flatMap((d) => d.unlockedCareers),
);

/** Üniversite diploması gerektiren ama belirli bir bölüme bağlı olmayan "genel" işler */
export const GENERAL_UNI_JOBS = new Set([
  'memur', 'memur_sef', 'mudur_yrd_kamu', 'mudur_kamu',
  'satis_temsilci', 'bolge_mudur', 'satis_direktor',
  'ik_uzman', 'ik_mudur',
]);

/** İş ID'sinin bölüm-spesifik mi yoksa genel mi olduğunu kontrol et */
export const isGeneralUniJob = (jobId: string): boolean =>
  GENERAL_UNI_JOBS.has(jobId);

/** Lisansüstü eğitim için fakülte uyumluluk haritası */
export const RELATED_FACULTIES: Record<Faculty, readonly Faculty[]> = {
  'Tıp': ['Sağlık Bilimleri', 'Eczacılık'],
  'Hukuk': [],
  'Mühendislik': ['Fen-Edebiyat'],
  'İşletme': ['İletişim'],
  'Fen-Edebiyat': ['Mühendislik', 'Eğitim'],
  'Eğitim': ['Fen-Edebiyat'],
  'İletişim': ['İşletme', 'Güzel Sanatlar'],
  'Mimarlık': ['Mühendislik', 'Güzel Sanatlar'],
  'Eczacılık': ['Tıp', 'Sağlık Bilimleri'],
  'Güzel Sanatlar': ['İletişim', 'Mimarlık'],
  'Sağlık Bilimleri': ['Tıp', 'Eczacılık'],
  'Havacılık': ['Mühendislik'],
};

/** Fakülte lisansüstü uyumluluğu — aynı veya ilişkili fakülte mi? */
export const isFacultyCompatible = (baseFaculty: Faculty, targetFaculty: Faculty): boolean =>
  baseFaculty === targetFaculty || RELATED_FACULTIES[baseFaculty].includes(targetFaculty);

/** Sınav puan aralığı açıklamaları */
export const getScoreDescription = (score: number): { label: string; percentile: string } => {
  if (score >= 480) return { label: 'Türkiye derecesi! Tüm bölümler sana açık!', percentile: 'Top %1' };
  if (score >= 420) return { label: 'Çok başarılı! En iyi bölümlere yerleşebilirsin.', percentile: 'Top %5' };
  if (score >= 350) return { label: 'Başarılı. İyi bölümlere yerleşebilirsin.', percentile: 'Top %15' };
  if (score >= 280) return { label: 'Ortalama üstü. Bazı bölümler sana açık.', percentile: 'Top %35' };
  if (score >= 200) return { label: 'Ortalama. Sınırlı bölüm seçeneğin var.', percentile: 'Top %60' };
  if (score >= 150) return { label: 'Baraj üstü. Sadece düşük puanlı bölümler.', percentile: 'Top %80' };
  return { label: 'Sınav barajını geçemedin. Üniversiteye yerleşemedin.', percentile: 'Baraj altı' };
};
