import { Personnel } from '@/services/routingService';

/**
 * MOCK DATA
 * Context Level 7: Static, deterministic data.
 */

export const MOCK_PERSONNEL: Personnel[] = [
  // North Istanbul (Sariyer / Maslak)
  { id: '1', fullName: 'Ahmet Yilmaz', address: 'Maslak, Sariyer', location: { lat: 41.1122, lng: 29.0201 } },
  { id: '2', fullName: 'Ayse Kaya', address: 'Istinye, Sariyer', location: { lat: 41.1150, lng: 29.0400 } },
  { id: '3', fullName: 'Mehmet Demir', address: 'Tarabya, Sariyer', location: { lat: 41.1380, lng: 29.0550 } },
  
  // Mid Istanbul (Besiktas / Sisli)
  { id: '4', fullName: 'Fatma Celik', address: 'Levent, Besiktas', location: { lat: 41.0800, lng: 29.0100 } },
  { id: '5', fullName: 'Mustafa Sahin', address: 'Etiler, Besiktas', location: { lat: 41.0850, lng: 29.0300 } },
  { id: '6', fullName: 'Zeynep Yildiz', address: 'Mecidiyekoy, Sisli', location: { lat: 41.0650, lng: 28.9900 } },
  
  // South/Historic (Fatih / Eminonu)
  { id: '7', fullName: 'Ali Ozturk', address: 'Fatih, Istanbul', location: { lat: 41.0150, lng: 28.9500 } },
  { id: '8', fullName: 'Emine Arslan', address: 'Eminonu, Fatih', location: { lat: 41.0180, lng: 28.9700 } },
  { id: '9', fullName: 'Burak Ergun', address: 'Sultanahmet, Fatih', location: { lat: 41.0050, lng: 28.9750 } },
  
  // Asian Side (Kadikoy) - Distant outliers
  { id: '10', fullName: 'Canan Koc', address: 'Kadikoy, Istanbul', location: { lat: 40.9900, lng: 29.0250 } },
  { id: '11', fullName: 'Deniz Aydin', address: 'Moda, Kadikoy', location: { lat: 40.9850, lng: 29.0300 } },
  { id: '12', fullName: 'Eren Kara', address: 'Bostanci, Kadikoy', location: { lat: 40.9600, lng: 29.0900 } },
];
