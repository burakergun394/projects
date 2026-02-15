import type { Education } from '../types';

export const EDUCATION_LIST: readonly Education[] = [
  {
    name: 'Lise',
    cost: 0,
    smartsReq: 0,
    smartsGain: 10,
    years: 4,
    minAge: 14,
  },
  {
    name: 'Üniversite',
    cost: 20000,
    smartsReq: 30,
    smartsGain: 20,
    years: 4,
    minAge: 18,
  },
  {
    name: 'Yüksek Lisans',
    cost: 35000,
    smartsReq: 55,
    smartsGain: 15,
    years: 2,
    minAge: 22,
  },
  {
    name: 'Doktora',
    cost: 50000,
    smartsReq: 70,
    smartsGain: 20,
    years: 4,
    minAge: 24,
  },
] as const;
