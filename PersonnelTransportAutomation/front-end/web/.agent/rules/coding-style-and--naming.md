---
trigger: always_on
---

CODING STYLE & NAMING RULE

General:
- All code must be written in TypeScript.
- Use clear, readable, and self-explanatory naming.
- Prefer explicitness over brevity.
- Avoid unnecessary abstractions.

File naming:
- Use kebab-case for folders.
- Use PascalCase for React component files.
- Use camelCase for non-component files (services, utils, data).
- Test files (if any) must follow the same name as the target file.

Component naming:
- React components must use PascalCase.
- Component names must match their file names exactly.
- One React component per file.

Function naming:
- Use camelCase for all functions.
- Function names must describe behavior clearly.
- Avoid generic names like `handleData`, `process`, `execute`.

Variable naming:
- Use camelCase for variables and constants.
- Boolean variables must start with `is`, `has`, or `can`.
- Arrays should use plural naming (e.g. `personnelList`, `routes`).

Type naming:
- TypeScript types and interfaces must use PascalCase.
- Prefer `type` over `interface` unless extension is required.
- Type names must be descriptive and domain-specific.

Service rules:
- Service files must expose clearly named functions.
- No anonymous exported functions.
- Services must not contain UI logic.

Component structure:
- Props types must be explicitly defined.
- Destructure props at the function signature level.
- Avoid inline complex logic inside JSX.

Formatting:
- Follow ESLint and Prettier defaults.
- Avoid long inline expressions.
- Keep functions small and focused.

Comments:
- Comment only complex logic.
- Do not comment obvious code.
- Algorithm steps must be commented clearly.

Imports:
- Use import alias (`@/`) for all internal imports.
- Avoid relative imports beyond one level (`../`).

Strictness:
- No `any` type is allowed.
- No unused variables or imports.
- Code must compile without warnings.
