---
trigger: always_on
---

MOCK DATA RULES

- Mock data must be stored only under `src/data/`.
- Mock data files must not contain business logic or functions.
- Mock data must be static and deterministic.
- No random data generation is allowed unless explicitly required.
- Mock data must reflect realistic domain values.
- IDs must be stable and unique.
- Coordinates (latitude/longitude) must be valid and realistic.
- Mock data must be typed using TypeScript types from `src/types/`.
- Mock data must not be mutated directly.
- Mock data must not be mixed with UI components or services.
