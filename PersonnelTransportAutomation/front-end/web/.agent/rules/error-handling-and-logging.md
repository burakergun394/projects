---
trigger: always_on
---

ERROR HANDLING & LOGGING RULES

- The application must not crash due to unhandled errors.
- All algorithmic operations must handle invalid input gracefully.
- Errors must be handled close to their source.
- User-facing errors must be displayed in a clear and friendly way.
- Ant Design components should be used for error messages where applicable.
- Console logging must be minimal and intentional.
- Debug logs must not exist in production code.
- Errors must not expose internal implementation details.
- Failures must not block unrelated functionality.
