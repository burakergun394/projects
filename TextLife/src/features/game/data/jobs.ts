import type { Job } from '../types';

export const JOBS: readonly Job[] = [
  { title: 'Bulaşıkçı', salary: 8000, req: 0 },
  { title: 'Garson', salary: 10000, req: 0 },
  { title: 'Kasiyer', salary: 11000, req: 10 },
  { title: 'Kurye', salary: 12000, req: 0 },
  { title: 'Satış Danışmanı', salary: 14000, req: 20 },
  { title: 'Sekreter', salary: 16000, req: 30 },
  { title: 'Muhasebeci', salary: 22000, req: 45 },
  { title: 'Öğretmen', salary: 25000, req: 50 },
  { title: 'Hemşire', salary: 28000, req: 55 },
  { title: 'Mühendis', salary: 35000, req: 65 },
  { title: 'Avukat', salary: 40000, req: 70 },
  { title: 'Doktor', salary: 55000, req: 80 },
  { title: 'Yazılımcı', salary: 45000, req: 60 },
  { title: 'Pilot', salary: 60000, req: 85 },
] as const;
