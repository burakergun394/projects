---
trigger: always_on
---

PROJECT STRUCTURE RULE

- The project must use Next.js App Router with TypeScript and Ant Design.
- All source code must live under the `src/` directory.

Required structure:

src/
- app/
  - layout.tsx
  - page.tsx
  - globals.css

- components/
  - layout/
  - routes/
  - common/

- data/
  - mock data only (no business logic)

- services/
  - business logic and algorithms only
  - no UI components

- types/
  - TypeScript types and interfaces only

- utils/
  - pure utility/helper functions only

- styles/
  - Ant Design customizations and global styles only

Rules:
- UI components must not contain business logic.
- Services must not contain UI or styling.
- Mock data must not be mixed with services.
- All new files must respect this structure.
- Do not create folders outside this structure unless explicitly required.

This structure is mandatory and must be followed consistently.
