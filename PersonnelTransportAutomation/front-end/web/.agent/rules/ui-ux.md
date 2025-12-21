---
trigger: always_on
---

UI / UX CONSISTENCY RULES

- Ant Design (antd) must be used as the primary UI library.
- Native HTML elements must be avoided when Ant Design components exist.
- Layout must be built using Ant Design layout components.
- Spacing, typography, and alignment must follow Ant Design defaults.
- Do not override Ant Design styles unless necessary.
- Custom styles must be minimal and placed under `src/styles/`.
- Reusable UI patterns must be extracted into components.
- Buttons must clearly indicate their intent (primary, default, danger).
- UI must remain consistent across all pages and components.
- No inline styles are allowed except for minor layout adjustments.
