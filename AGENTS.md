# Agent Working Instructions

## Workflow

- For every requirement, create a new **User Story** in Azure DevOps (ADO).
- Once the requirement is clear, the Agent should start implementation based on the instructions provided in the User Story.
- The Agent must:
  - Analyze the User Story
  - Perform code changes following defined rules
  - Validate the changes (build/test)
  - Create a Pull Request (PR)

---

## Branching Strategy

- Create a new branch for each task using the format:

  Feature/{ShortDescription}-{UserStoryNumber}-{Date(MMDDYYYY)}

  Example:
  Feature/PaymentIntegration-12345-05182026

---

## Coding Standards & Practices

- Write clean, maintainable, and secure code (this application is publicly exposed).
- Follow **SOLID principles** for better design and maintainability.
- Use **Dependency Injection (DI)** to ensure loose coupling between layers.
- Maintain proper separation of concerns across architecture layers.

---

## Design Patterns

- Use the **Factory Pattern** for payment processing.

  Reason:
  - Application supports multiple payment types:
    - Credit Card
    - UPI
    - Net Banking
  - Future payment methods may be added, so implementation must be extensible.

---

## Data Access Rules

- Use **Entity Framework Core** for data access.
- Call **Azure SQL stored procedures** via EF Core where required.
- Do NOT use any object mapping library (e.g., AutoMapper).

---

## Security & Quality

- Ensure input validation and proper error handling.
- Avoid hardcoded secrets or credentials.
- Follow secure coding practices at all times.

---

## Validation Before PR

- Ensure application builds successfully.
- Ensure existing functionality is not broken.
- Follow coding standards and architectural guidelines.
- Provide a clear PR description summarizing:
  - Changes made
  - Impact
  - Assumptions (if any)