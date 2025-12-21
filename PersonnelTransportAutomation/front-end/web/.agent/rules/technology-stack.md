---
trigger: always_on
---

TECHNOLOGY STACK RULE

- The project must be built with Next.js using the App Router architecture.
- TypeScript is mandatory for all source files.
- React functional components must be used exclusively.
- Ant Design (antd) must be used as the primary UI library.
- Tailwind CSS must NOT be used.
- CSS-in-JS solutions (styled-components, emotion) must NOT be used.

Frontend rules:
- Use Ant Design components for all UI elements whenever possible.
- Custom styles must be minimal and placed only under `src/styles/`.
- Ant Design theme customization must be handled via configuration or global styles.

State management:
- Local component state should be managed with React hooks.
- Do not introduce global state management libraries unless explicitly required.

Data handling:
- The application must work with mock data only.
- No backend integration is allowed in the MVP phase.
- No API calls or fetch logic should be introduced.

Utilities and logic:
- Business logic and algorithms must live in `src/services/`.
- Reusable helper functions must live in `src/utils/`.
- No business logic is allowed inside UI components.

Tooling:
- ESLint must remain enabled.
- Default Next.js and TypeScript configurations must not be removed.

General constraints:
- Do not introduce additional frameworks or libraries without explicit approval.
- Keep the stack minimal and enterprise-oriented.
