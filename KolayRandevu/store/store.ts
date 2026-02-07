import { create } from 'zustand';
import type { AppState, AppointmentStatus, DaySchedule } from './types';
import { mockAppointments, mockCustomers, mockProvider, mockSchedule } from './mock-data';

export const useStore = create<AppState>((set) => ({
  locale: 'tr',
  setLocale: (locale) => set({ locale }),

  role: null,
  setRole: (role) => set({ role }),

  provider: mockProvider,

  schedule: mockSchedule,
  setSchedule: (schedule: DaySchedule[]) => set({ schedule }),

  appointments: mockAppointments,
  updateAppointmentStatus: (id: string, status: AppointmentStatus) =>
    set((state) => ({
      appointments: state.appointments.map((a) => (a.id === id ? { ...a, status } : a)),
    })),

  customers: mockCustomers,
}));
