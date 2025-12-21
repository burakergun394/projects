---
trigger: always_on
---

COMMIT & PULL REQUEST RULES

Commit messages:
- Commit messages must be written in English.
- Use the imperative mood (e.g. "Add route generation logic").
- Keep the first line under 72 characters.
- Do not end the subject line with a period.
- Each commit must represent a single logical change.
- Avoid vague messages such as "update", "fix", "changes".

Recommended commit prefixes:
- feat: A new feature
- fix: A bug fix
- refactor: Code refactoring without behavior change
- chore: Tooling, configuration, or maintenance
- docs: Documentation changes
- test: Adding or updating tests

Example:
- feat: add distance-based route generation
- refactor: extract routing logic into service layer

Commit frequency:
- Commit early and often.
- Do not combine unrelated changes in a single commit.

Pull Requests:
- Every PR must have a clear and descriptive title.
- PR descriptions must explain the purpose and scope of changes.
- Reference related issues or tasks if applicable.
- Keep PRs focused and reasonably sized.
- Large features must be split into smaller PRs.

Code quality:
- All code must pass linting and type checks before PR submission.
- No commented-out code is allowed in PRs.
- No debug logs may be included.

Review rules:
- PRs must be self-reviewed before requesting review.
- Address all review comments explicitly.
- Do not merge PRs with unresolved comments.

General:
- Main branch must always remain in a deployable state.
- Force-push to shared branches is not allowed.
