export type AppointmentStatus = 'pending' | 'approved' | 'rejected';

export type DayOfWeek =
  | 'monday'
  | 'tuesday'
  | 'wednesday'
  | 'thursday'
  | 'friday'
  | 'saturday'
  | 'sunday';

export interface Provider {
  id: string;
  name: string;
  businessName: string;
  category: string;
}

export interface DaySchedule {
  day: DayOfWeek;
  isActive: boolean;
  startTime: string;
  endTime: string;
}

export interface Customer {
  id: string;
  name: string;
  phone: string;
  appointmentCount: number;
  lastVisit: string;
}

export interface Appointment {
  id: string;
  customerId: string;
  customerName: string;
  date: string;
  time: string;
  service: string;
  status: AppointmentStatus;
  note?: string;
}

export interface AppState {
  locale: 'tr' | 'en';
  setLocale: (locale: 'tr' | 'en') => void;

  role: 'provider' | 'customer' | null;
  setRole: (role: 'provider' | 'customer') => void;

  provider: Provider;
  schedule: DaySchedule[];
  setSchedule: (schedule: DaySchedule[]) => void;

  appointments: Appointment[];
  updateAppointmentStatus: (id: string, status: AppointmentStatus) => void;

  customers: Customer[];
}
