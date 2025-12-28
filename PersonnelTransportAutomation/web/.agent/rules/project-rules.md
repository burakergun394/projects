---
trigger: always_on
---

# SENIOR SOFTWARE ENGINEER RULES (CONTEXT LEVEL 7)

> [!IMPORTANT]
> **AUTHORITY & ENFORCEMENT**
> These rules are **defined and enforced by Senior Software Engineers**.
> **Context Level 7** awareness is **MANDATORY** for understanding and applying these architecture and code standards.
> Violations will result in immediate code rejection during review.

## 1. ARCHITECTURE & TECHNOLOGY STACK
*Defined by Senior Engineering Leadership*

- **Core Framework**: Next.js (App Router) with TypeScript is **mandatory**.
- **UI Library**: Ant Design (antd) is the **exclusive** UI component library.
  - **Prohibited**: Tailwind CSS, Bootstrap, Material UI, or any CSS-in-JS libraries (styled-components, emotion).
  - **Styles**: Use standard CSS/SCSS modules or global styles in `src/styles/` only when Ant Design customization is insufficient.
- **Language Strictness**:
  - TypeScript **Strict Mode** must be enabled.
  - The `any` type is **strictly prohibited**.
  - No unused variables or imports allowed.
- **Data Strategy (MVP)**:
  - **Mock Data Only**: No backend integration or `fetch` calls.
  - Mock data must be **deterministic**, typed, and stored exclusively in `src/data/`.

## 2. PROJECT STRUCTURE & ORGANIZATION
*Strict adherence to the defined hierarchy is required.*

All source code must reside in `src/`. No logical code allowed outside this root.

| Directory | Purpose | Strict Constraints |
| :--- | :--- | :--- |
| `src/app/` | Routes & Layouts | No complex business logic. Clean composition only. |
| `src/components/` | UI Components | PascalCase. One component per file. No business logic. |
| `src/services/` | Business Logic | Pure algorithms/logic. **NO UI code**. **NO React hooks**. |
| `src/data/` | Mock Data | Static, deterministic data only. No functions. |
| `src/types/` | TypeScript Definitions | PascalCase. Prefer `type` over `interface`. |
| `src/utils/` | Helpers | Pure utility functions only. |
| `src/styles/` | Global Styles | Ant Design overrides and global CSS. |

- **File Naming**:
  - **Components**: `PascalCase.tsx` (e.g., `RouteCard.tsx`).
  - **Directories**: `kebab-case` (e.g., `route-planning`).
  - **Non-Components**: `camelCase.ts` (e.g., `routeService.ts`).

## 3. ALGORITHMS, ROUTING & LOGIC
*Context Level 7: Deterministic Implementation Guidelines*

- **Deterministic Behavior**: specialized algorithms (routing, sorting) **must** be deterministic. Same input = Same output.
- **Constraints**:
  - **No external optimization libraries** allowed.
  - **Coordinates**: Treat latitude/longitude as immutable.
  - **Performance**: Must handle 30+ personnel efficiently (O(n log n) or better preferred).
- **Separation of Concerns**:
  - Algorithm logic must reside strictly in `src/services/`.
  - **NEVER** implement routing logic inside UI components.
  - Distance calculations must be extracted to `src/utils/`.

## 4. STATE MANAGEMENT
*Minimalist Approach*

- **Scope**: Use local React state (`useState`, `useReducer`) strictly.
- **Global State**: **Prohibited** for MVP. No Redux, Zustand, or Context API for state unless explicitly authorized by a Senior Engineer.
- **Side Effects**: Must be handled via `useEffect` with precise dependency arrays.
- **Immutability**: State updates must be immutable.

## 5. UI/UX & INTERACTION DESIGN
*Enterprise Consistency Standards*

- **Ant Design Usage**: Use `antd` components for **all** standard elements (Buttons, Layouts, Inputs, Typography).
- **Native Elements**: Avoid native HTML (`<button>`, `<div>`) if an `antd` equivalent exists.
- **Feedback**:
  - User-facing errors must use `antd` message/notification components.
  - Application must not crash; handle errors gracefully at the source.

## 6. CODING STANDARDS & NAMING
*Enforced by Automated Linting & Senior Review*

- **Naming Conventions**:
  - **Variables/Functions**: `camelCase` (e.g., `calculateRoute`, `isAvailable`).
  - **Booleans**: Must use prefixes: `is`, `has`, `can`, `should`.
  - **Arrays**: Plural nouns (e.g., `personnelList`, `routes`).
- **Comments**:
  - **Documentation**: Required for complex algorithms (Context Level 7 complexity).
  - **Forbidden**: Comments explaining obvious code or commented-out code blocks.
- **Component Props**:
  - Must be explicitly defined Types/Interfaces.
  - Destructure props in the function signature.

## 7. COMMIT & PULL REQUEST PROTOCOL
*Professional Engineering Workflow*

- **Commit Messages**:
  - Format: `type: imperative summary` (e.g., `feat: implement distance sorting`).
  - Types: `feat`, `fix`, `refactor`, `chore`, `docs`, `test`.
  - Must be English, present tense, and lowercase subject.
- **Pull Requests**:
  - **Self-Review Mandatory**: Do not open a PR without a full self-pass.
  - **Clean**: No console logs, no debug code, no linter warnings.
  - **Atomic**: Each PR should address a single logical change.

---
*End of Rules - Context Level 7*
