import type { Job } from '../types';

export const JOBS: readonly Job[] = [
  // Entry — Giriş Seviye
  { title: 'Bulaşıkçı', salary: 8000, req: 0, category: 'entry' },
  { title: 'Garson', salary: 10000, req: 0, category: 'entry' },
  { title: 'Kasiyer', salary: 11000, req: 10, category: 'entry' },
  { title: 'Temizlikçi', salary: 11000, req: 0, category: 'entry' },
  { title: 'Kurye', salary: 12000, req: 0, category: 'entry' },
  { title: 'Aşçı', salary: 13500, req: 10, category: 'entry' },
  { title: 'Satış Danışmanı', salary: 14000, req: 20, category: 'entry' },
  { title: 'Şoför', salary: 15000, req: 10, category: 'entry' },

  // Skilled — Nitelikli
  { title: 'Sekreter', salary: 16000, req: 30, category: 'skilled' },
  { title: 'Elektrikçi', salary: 19000, req: 25, category: 'skilled' },
  { title: 'Grafik Tasarımcı', salary: 22000, req: 40, category: 'skilled' },
  { title: 'Muhasebeci', salary: 22000, req: 45, category: 'skilled' },
  { title: 'Öğretmen', salary: 25000, req: 50, category: 'skilled' },
  { title: 'Hemşire', salary: 28000, req: 55, category: 'skilled' },
  { title: 'Eczacı', salary: 30000, req: 55, category: 'skilled' },

  // Professional — Profesyonel
  { title: 'Akademisyen', salary: 35000, req: 70, category: 'professional' },
  { title: 'Mühendis', salary: 35000, req: 65, category: 'professional' },
  { title: 'Avukat', salary: 40000, req: 70, category: 'professional' },
  { title: 'Mimar', salary: 42000, req: 65, category: 'professional' },
  { title: 'Yazılımcı', salary: 45000, req: 60, category: 'professional' },
  { title: 'Doktor', salary: 55000, req: 80, category: 'professional' },
  { title: 'Pilot', salary: 60000, req: 85, category: 'professional' },

  // Executive — Yönetici
  { title: 'Müdür', salary: 70000, req: 75, category: 'executive' },
] as const;

export const JOB_CATEGORY_LABELS: Record<Job['category'], string> = {
  entry: 'Giriş Seviye',
  skilled: 'Nitelikli',
  professional: 'Profesyonel',
  executive: 'Yönetici',
};
