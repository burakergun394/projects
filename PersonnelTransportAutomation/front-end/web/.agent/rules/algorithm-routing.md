---
trigger: always_on
---

ALGORITHM & ROUTING RULES

General:
- All routing and algorithm logic must be deterministic.
- Algorithms must be easy to understand and maintain.
- Clarity is prioritized over optimality.

Location data:
- All distance calculations must use latitude and longitude.
- Coordinates must be treated as immutable input data.
- Distance calculation must be extracted into a utility function.

Routing logic:
- Personnel must be grouped into routes based on vehicle capacity.
- Default vehicle capacity is 10 personnel per route.
- Total number of routes must be calculated dynamically based on personnel count.

Route generation:
- Routes must be generated only when explicitly triggered (e.g. button click).
- No route generation logic may run on initial render.
- Routes must be independent and stateless.

Pickup order:
- Each route must assign a pickup order starting from 1.
- Pickup order must be based on distance-based sorting.
- Sorting logic must be consistent and reproducible.

Algorithm constraints:
- No external optimization libraries are allowed.
- No randomization is allowed.
- Same input must always produce the same output.

Code organization:
- Routing algorithms must live in `src/services/`.
- Distance calculation must live in `src/utils/`.
- No routing logic is allowed inside UI components.

Performance:
- Algorithms must handle at least 30 personnel without noticeable delay.
- Avoid unnecessary recalculations.

Extensibility:
- Routing logic must be written in a way that allows future extension
  (e.g. time windows, vehicle capacity changes, map integration).

Error handling:
- Invalid or missing coordinates must be handled gracefully.
- Algorithm must not throw uncaught runtime errors.
