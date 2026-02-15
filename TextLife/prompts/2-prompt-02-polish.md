# TextLife — Prompt 02: Game Content & Feature Expansion

Paste the following prompt into Claude Code:

---

```
Read CLAUDE.md. The project is already set up with feature-based Expo Router architecture, Zustand store, typed data files, and NativeWind styling. Now expand the game content and add missing features.

## 1. Expand Event Pools

Current event pools are thin (8-15 events each). Triple them to make gameplay less repetitive.

### `src/features/game/data/events.ts`

Add events to reach these targets:

| Pool | Current | Target | Focus Areas to Add |
|---|---|---|---|
| BABY_EVENTS | 8 | 24 | Sickness, first words variants, daycare, sibling born, pet interactions, food reactions |
| CHILD_EVENTS | 12 | 36 | School competitions, bullying variants, hobbies (drawing/music/sports), family events, holidays, friendships |
| TEEN_EVENTS | 12 | 36 | Romantic relationships, social media, exam stress, part-time jobs, driver's license, gap year, peer pressure, identity |
| ADULT_EVENTS | 15 | 45 | Marriage variants, kids, home buying, career changes, health scares, travel, side business, divorce, promotions, layoffs, investments |
| ELDER_EVENTS | 9 | 27 | Grandchildren, retirement hobbies, health decline variants, legacy, travel, memoir writing, losing friends, wisdom |

Rules:
- All text in Turkish
- Each event has balanced `fx` (stat effects) — don't make all new events positive
- Maintain existing events, only ADD new ones
- Some events should have money effects (both positive and negative)
- Add rare high-impact events (5% of pool): lottery win, serious accident, inheritance, etc.

## 2. Add Relationship System

### New Types — `src/features/game/types/index.ts`

Add:

```typescript
interface Relationship {
  id: string;
  name: string;
  surname: string;
  type: 'spouse' | 'child' | 'friend' | 'parent' | 'sibling';
  age: number;
  closeness: number;    // 0-100
  isAlive: boolean;
}
```

Update `Character` interface:
```typescript
interface Character {
  // ... existing fields
  relationships: Relationship[];
  isMarried: boolean;
  childCount: number;
}
```

### Relationship Events — `src/features/game/data/relationships.ts`

Create relationship-specific event pools:

```typescript
const MARRIAGE_EVENTS: GameEvent[]     // meet someone, dating, proposal, wedding, fights, divorce
const FRIENDSHIP_EVENTS: GameEvent[]   // make friend, lose friend, betrayal, reunion
const FAMILY_EVENTS: GameEvent[]       // child born, child milestone, parent aging, sibling rivalry
```

### Store Updates — `src/features/game/stores/gameStore.ts`

Add actions:
- `marry()` — triggered by event or player choice at age 20+
- `divorce()` — costs money, happiness hit
- `haveChild()` — triggered by event when married, age 22+

Inside `ageUp()`:
- Age all relationships +1
- Random relationship events based on relationship types
- Relationship closeness drift (±random small amount per year)
- Parents can die (age-based probability like character)

### Relationships Tab — new tab in game screen

Add a 5th tab: `{ id: 'relations', label: 'İlişkiler', emoji: '❤️' }`

Update `TabId` type: add `'relations'`

Create `src/features/game/components/RelationList.tsx`:
- Group by type (Aile / Arkadaşlar / Eş)
- Show name, age, closeness bar, alive/dead status
- Interaction buttons: "Zaman Geçir" (+closeness), "Tartış" (-closeness, event)

## 3. Add Achievement System

### Types — `src/features/game/types/index.ts`

```typescript
interface Achievement {
  id: string;
  title: string;          // Turkish
  description: string;    // Turkish
  emoji: string;
  condition: (character: Character, log: LogEntry[]) => boolean;
  unlocked: boolean;
}
```

### Achievement Data — `src/features/game/data/achievements.ts`

Create 20 achievements:

```typescript
export const ACHIEVEMENTS: Achievement[] = [
  { id: 'first_job', title: 'İlk Maaş', description: 'İlk işine başla', emoji: '💼', ... },
  { id: 'millionaire', title: 'Milyoner', description: '₺1.000.000 biriktir', emoji: '💰', ... },
  { id: 'scholar', title: 'Akademisyen', description: 'Doktora tamamla', emoji: '🎓', ... },
  { id: 'centenarian', title: 'Yüzyıllık Çınar', description: '100 yaşına ulaş', emoji: '🎂', ... },
  { id: 'heartbreaker', title: 'Kalp Kıran', description: '3 kez boşan', emoji: '💔', ... },
  { id: 'athlete', title: 'Sporcu', description: 'Sağlık 95+ ulaş', emoji: '💪', ... },
  { id: 'genius', title: 'Dahi', description: 'Zeka 95+ ulaş', emoji: '🧠', ... },
  { id: 'lucky', title: 'Şanslı', description: 'Piyango kazan', emoji: '🍀', ... },
  { id: 'ceo', title: 'Patron', description: 'CEO ol', emoji: '👔', ... },
  { id: 'family_person', title: 'Aile İnsanı', description: '3+ çocuk sahibi ol', emoji: '👨‍👩‍👧‍👦', ... },
  { id: 'traveler', title: 'Gezgin', description: '5 tatil etkinliği yaşa', emoji: '✈️', ... },
  { id: 'criminal', title: 'Suç Baronu', description: '5 suç işle yakalanmadan', emoji: '🤫', ... },
  { id: 'healthy', title: 'Sağlık Gurusu', description: '50 kez spor yap', emoji: '🏋️', ... },
  { id: 'poor', title: 'Beş Parasız', description: '₺-50.000 borca düş', emoji: '📉', ... },
  { id: 'long_marriage', title: 'Ömürlük', description: '25 yıl evli kal', emoji: '💍', ... },
  { id: 'dropout', title: 'Okul Terk', description: 'Hiç eğitim almadan 30 yaşına gel', emoji: '🚫', ... },
  { id: 'comeback', title: 'Geri Dönüş', description: 'Sağlık 10 altına düş sonra 80+ çık', emoji: '🔥', ... },
  { id: 'peaceful', title: 'Huzurlu Son', description: 'Tüm statlar 70+ iken öl', emoji: '🕊️', ... },
  { id: 'young_death', title: 'Erken Veda', description: '30 yaş altında öl', emoji: '😢', ... },
  { id: 'jack_of_all', title: 'Her İşin Adamı', description: '5 farklı işte çalış', emoji: '🔄', ... },
];
```

### Store Updates

Add to Character: `achievements: string[]` (unlocked achievement IDs), `actionCounts: Record<string, number>` (track gym count, crime count, etc.)

Add to store: `checkAchievements()` — runs after every ageUp, checks all conditions, adds newly unlocked to character.achievements, logs milestone.

### Achievement Display

Show unlocked achievements on death screen (CharacterCard).
Add toast/notification component when achievement unlocks during gameplay.

## 4. Expand Jobs

### `src/features/game/data/jobs.ts`

Add 10 more jobs to reach 24 total. Add job categories:

```typescript
interface Job {
  title: string;
  salary: number;
  req: number;
  category: 'entry' | 'skilled' | 'professional' | 'executive';
}
```

New jobs to add:
- Entry: Temizlikçi (₺11.000), Şoför (₺15.000), Aşçı (₺13.500)
- Skilled: Grafik Tasarımcı (₺22.000), Elektrikçi (₺19.000), Eczacı (₺30.000)
- Professional: Mimar (₺42.000), Pilot (₺55.000), Akademisyen (₺35.000)
- Executive: Müdür (₺70.000)

Group job list by category in `JobList.tsx` with section headers.

## 5. Add Activities

### `src/features/game/data/activities.ts`

Expand from 8 to 14 activities:

New activities:
- `travel` — Seyahat Et: ₺3-10K, +happiness, +smarts, low health risk
- `volunteer` — Gönüllü Ol: free, +happiness, +smarts
- `diet` — Diyet Yap: free, +health, +looks, -happiness
- `beauty` — Güzellik Bakımı: ₺1-3K, +looks, +happiness
- `read` — Kitap Oku: free, +smarts, +happiness (small)
- `gamble` — Kumar Oyna: ₺1-20K risk, high reward/loss variance

## 6. Death Screen Expansion

### `src/features/character/components/CharacterCard.tsx`

Expand death summary to show:
- Life timeline (key milestones from log)
- Relationships summary (spouse name, children count)
- Achievement badges (unlocked achievements grid)
- Final stats with color coding (good/bad thresholds)
- "Hayat Puanı" calculation: weighted score from all stats, money, education, relationships, achievements

## 7. Sound Effects (Optional — prep only)

Create `src/shared/constants/sounds.ts` with sound event mapping:

```typescript
export const SOUND_EVENTS = {
  ageUp: 'age-up.mp3',
  goodEvent: 'positive.mp3',
  badEvent: 'negative.mp3',
  death: 'death.mp3',
  achievement: 'achievement.mp3',
  buttonPress: 'tap.mp3',
} as const;
```

Don't implement audio playback yet — just the mapping and hook skeleton `useSoundEffect.ts` that can be wired up later.

## Rules

- All new content in Turkish
- Follow existing TypeScript patterns and CLAUDE.md conventions
- No StyleSheet.create — NativeWind only
- New types extend existing interfaces — don't break current ones
- Game balance: new events should feel fair, not all positive or all negative
- React.memo on all list item components
- Test that existing gameplay still works after all additions
```
