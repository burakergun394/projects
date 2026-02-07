import { create } from 'zustand';
import type { AppState, AppointmentStatus, DayOfWeek, DaySchedule, TimeRange } from './types';
import { mockAppointments, mockCustomers, mockProvider, mockSchedule } from './mock-data';

const updateDay = (
  schedule: DaySchedule[],
  day: DayOfWeek,
  updater: (d: DaySchedule) => DaySchedule
) => schedule.map((d) => (d.day === day ? updater(d) : d));

export const useStore = create<AppState>((set) => ({
  locale: 'tr',
  setLocale: (locale) => set({ locale }),

  role: null,
  setRole: (role) => set({ role }),

  provider: mockProvider,

  schedule: mockSchedule,
  setSchedule: (schedule: DaySchedule[]) => set({ schedule }),

  toggleDay: (day: DayOfWeek, isActive: boolean) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({ ...d, isActive })),
    })),

  addTimeRange: (day: DayOfWeek, range: TimeRange) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({
        ...d,
        ranges: [...d.ranges, range],
      })),
    })),

  removeTimeRange: (day: DayOfWeek, rangeId: string) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({
        ...d,
        ranges: d.ranges.filter((r) => r.id !== rangeId),
      })),
    })),

  updateTimeRange: (day: DayOfWeek, rangeId: string, updates: Partial<TimeRange>) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({
        ...d,
        ranges: d.ranges.map((r) => (r.id === rangeId ? { ...r, ...updates } : r)),
      })),
    })),

  addSlot: (day: DayOfWeek, time: string) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({
        ...d,
        slots: [...d.slots, time].sort(),
      })),
    })),

  removeSlot: (day: DayOfWeek, time: string) =>
    set((state) => ({
      schedule: updateDay(state.schedule, day, (d) => ({
        ...d,
        slots: d.slots.filter((s) => s !== time),
      })),
    })),

  appointments: mockAppointments,
  updateAppointmentStatus: (id: string, status: AppointmentStatus) =>
    set((state) => ({
      appointments: state.appointments.map((a) => (a.id === id ? { ...a, status } : a)),
    })),

  customers: mockCustomers,
}));
