import type { Appointment, Customer, DaySchedule, Provider } from './types';

const today = new Date().toISOString().split('T')[0];
const yesterday = new Date(Date.now() - 86400000).toISOString().split('T')[0];
const twoDaysAgo = new Date(Date.now() - 2 * 86400000).toISOString().split('T')[0];
const threeDaysAgo = new Date(Date.now() - 3 * 86400000).toISOString().split('T')[0];
const tomorrow = new Date(Date.now() + 86400000).toISOString().split('T')[0];

export const mockProvider: Provider = {
  id: 'p1',
  name: 'Ahmet Yılmaz',
  businessName: 'Yılmaz Kuaför',
  category: 'Kuaför',
};

export const mockSchedule: DaySchedule[] = [
  { day: 'monday', isActive: true, startTime: '09:00', endTime: '18:00' },
  { day: 'tuesday', isActive: true, startTime: '09:00', endTime: '18:00' },
  { day: 'wednesday', isActive: true, startTime: '09:00', endTime: '18:00' },
  { day: 'thursday', isActive: true, startTime: '09:00', endTime: '18:00' },
  { day: 'friday', isActive: true, startTime: '09:00', endTime: '18:00' },
  { day: 'saturday', isActive: true, startTime: '10:00', endTime: '14:00' },
  { day: 'sunday', isActive: false, startTime: '09:00', endTime: '18:00' },
];

export const mockCustomers: Customer[] = [
  { id: 'c1', name: 'Mehmet Demir', phone: '0532 111 2233', appointmentCount: 8, lastVisit: yesterday },
  { id: 'c2', name: 'Ayşe Kaya', phone: '0533 222 3344', appointmentCount: 5, lastVisit: twoDaysAgo },
  { id: 'c3', name: 'Fatma Çelik', phone: '0534 333 4455', appointmentCount: 12, lastVisit: today },
  { id: 'c4', name: 'Ali Öztürk', phone: '0535 444 5566', appointmentCount: 3, lastVisit: threeDaysAgo },
  { id: 'c5', name: 'Zeynep Arslan', phone: '0536 555 6677', appointmentCount: 7, lastVisit: yesterday },
  { id: 'c6', name: 'Emre Şahin', phone: '0537 666 7788', appointmentCount: 2, lastVisit: twoDaysAgo },
];

export const mockAppointments: Appointment[] = [
  {
    id: 'a1',
    customerId: 'c1',
    customerName: 'Mehmet Demir',
    date: today,
    time: '09:30',
    service: 'Saç Kesimi',
    status: 'approved',
  },
  {
    id: 'a2',
    customerId: 'c3',
    customerName: 'Fatma Çelik',
    date: today,
    time: '11:00',
    service: 'Saç Boyama',
    status: 'approved',
  },
  {
    id: 'a3',
    customerId: 'c5',
    customerName: 'Zeynep Arslan',
    date: today,
    time: '14:00',
    service: 'Fön',
    status: 'pending',
  },
  {
    id: 'a4',
    customerId: 'c2',
    customerName: 'Ayşe Kaya',
    date: today,
    time: '16:00',
    service: 'Manikür',
    status: 'pending',
  },
  {
    id: 'a5',
    customerId: 'c4',
    customerName: 'Ali Öztürk',
    date: tomorrow,
    time: '10:00',
    service: 'Saç Kesimi',
    status: 'pending',
  },
  {
    id: 'a6',
    customerId: 'c6',
    customerName: 'Emre Şahin',
    date: tomorrow,
    time: '13:00',
    service: 'Sakal Tıraşı',
    status: 'pending',
  },
  {
    id: 'a7',
    customerId: 'c1',
    customerName: 'Mehmet Demir',
    date: yesterday,
    time: '10:00',
    service: 'Saç Kesimi',
    status: 'approved',
  },
  {
    id: 'a8',
    customerId: 'c2',
    customerName: 'Ayşe Kaya',
    date: twoDaysAgo,
    time: '15:00',
    service: 'Saç Boyama',
    status: 'approved',
  },
  {
    id: 'a9',
    customerId: 'c4',
    customerName: 'Ali Öztürk',
    date: threeDaysAgo,
    time: '11:30',
    service: 'Fön',
    status: 'rejected',
    note: 'Müsait değilim',
  },
  {
    id: 'a10',
    customerId: 'c5',
    customerName: 'Zeynep Arslan',
    date: yesterday,
    time: '09:00',
    service: 'Manikür',
    status: 'approved',
  },
];
