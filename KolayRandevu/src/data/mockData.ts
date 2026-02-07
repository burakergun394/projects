export interface Appointment {
  id: string;
  customerName: string;
  service: string;
  date: string;
  time: string;
  status: 'pending' | 'confirmed' | 'rejected';
}

export interface Customer {
  id: string;
  name: string;
  visitCount: number;
}

export interface WorkingDay {
  day: string;
  enabled: boolean;
  startTime: string;
  endTime: string;
}

export const mockAppointments: Appointment[] = [
  { id: '1', customerName: 'Ahmet Yilmaz', service: 'Haircut', date: '2026-02-07', time: '09:00', status: 'confirmed' },
  { id: '2', customerName: 'Ayse Demir', service: 'Hair Coloring', date: '2026-02-07', time: '10:00', status: 'confirmed' },
  { id: '3', customerName: 'Mehmet Kaya', service: 'Beard Trim', date: '2026-02-07', time: '11:30', status: 'pending' },
  { id: '4', customerName: 'Fatma Celik', service: 'Manicure', date: '2026-02-07', time: '13:00', status: 'pending' },
  { id: '5', customerName: 'Ali Ozturk', service: 'Haircut', date: '2026-02-07', time: '14:00', status: 'pending' },
  { id: '6', customerName: 'Zeynep Arslan', service: 'Hair Styling', date: '2026-02-08', time: '09:30', status: 'confirmed' },
];

export const mockCustomers: Customer[] = [
  { id: '1', name: 'Ahmet Yilmaz', visitCount: 12 },
  { id: '2', name: 'Ayse Demir', visitCount: 8 },
  { id: '3', name: 'Mehmet Kaya', visitCount: 5 },
  { id: '4', name: 'Fatma Celik', visitCount: 3 },
  { id: '5', name: 'Ali Ozturk', visitCount: 1 },
];

export const defaultWorkingDays: WorkingDay[] = [
  { day: 'Monday', enabled: true, startTime: '09:00', endTime: '18:00' },
  { day: 'Tuesday', enabled: true, startTime: '09:00', endTime: '18:00' },
  { day: 'Wednesday', enabled: true, startTime: '09:00', endTime: '18:00' },
  { day: 'Thursday', enabled: true, startTime: '09:00', endTime: '18:00' },
  { day: 'Friday', enabled: true, startTime: '09:00', endTime: '18:00' },
  { day: 'Saturday', enabled: true, startTime: '10:00', endTime: '16:00' },
  { day: 'Sunday', enabled: false, startTime: '10:00', endTime: '14:00' },
];
