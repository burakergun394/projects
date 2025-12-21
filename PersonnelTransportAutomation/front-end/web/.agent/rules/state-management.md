---
trigger: always_on
---

STATE MANAGEMENT RULES

- Local state must be managed using React hooks.
- State must be kept as close as possible to where it is used.
- No global state management library is allowed in the MVP phase.
- Derived state must not be duplicated.
- Avoid unnecessary state.
- Side effects must be handled using `useEffect`.
- State updates must be predictable and explicit.
- Do not store mock data directly in state unless required.
- Routing and algorithm results may be stored in component state.
