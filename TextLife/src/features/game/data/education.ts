import type { Education } from '../types';

export const EDUCATION_LIST: readonly Education[] = [
  // Zorunlu eğitim (otomatik başlar)
  {
    name: 'İlkokul',
    cost: 0,
    smartsReq: 0,
    smartsGain: 8,
    years: 4,
    minAge: 6,
    maxAge: 6,
    auto: true,
    prereq: null,
    examRequired: false,
    examPassRate: 0,
    dropCanAge: null,
  },
  {
    name: 'Ortaokul',
    cost: 0,
    smartsReq: 0,
    smartsGain: 8,
    years: 4,
    minAge: 10,
    maxAge: 10,
    auto: true,
    prereq: 'İlkokul',
    examRequired: false,
    examPassRate: 0,
    dropCanAge: null,
  },
  {
    name: 'Lise',
    cost: 0,
    smartsReq: 0,
    smartsGain: 10,
    years: 4,
    minAge: 14,
    maxAge: 14,
    auto: true,
    prereq: 'Ortaokul',
    examRequired: false,
    examPassRate: 0,
    dropCanAge: 16,
  },
  // Üniversite artık YKS sınav + bölüm seçim sistemiyle çalışıyor (departments.ts)
  // Sadece Yüksek Lisans ve Doktora eski startEdu akışını kullanır
  {
    name: 'Yüksek Lisans',
    cost: 15_000,
    smartsReq: 55,
    smartsGain: 12,
    years: 2,
    minAge: 22,
    maxAge: 45,
    auto: false,
    prereq: 'Üniversite',
    examRequired: true,
    examPassRate: 55,
    dropCanAge: 0,
  },
  {
    name: 'Doktora',
    cost: 10_000,
    smartsReq: 70,
    smartsGain: 10,
    years: 5,
    minAge: 24,
    maxAge: 50,
    auto: false,
    prereq: 'Yüksek Lisans',
    examRequired: true,
    examPassRate: 45,
    dropCanAge: 0,
  },
] as const;

/** Tamamlanmış eğitim kontrolü — "Üniversite" prereq'i tüm üniversite biçimlerini kabul eder */
export const hasCompletedEdu = (education: string[], name: string): boolean => {
  if (name === 'Üniversite') {
    // "Üniversite — Bilgisayar Müh." vb. tüm biçimleri kabul et
    return education.some((e) => e.startsWith('Üniversite'));
  }
  return education.includes(name);
};
