# Copilot Instructions

---

## Project Overview

This project is an **online shopping web application** (similar to Amazon).

Users can:
- Search and browse products
- View product details
- Purchase products using multiple payment methods

The application is designed to be **scalable, secure, and extensible**, especially for adding future payment options and features.

---

## Solution Architecture

**Solution Name:** `Shopping`

The application follows a **clean layered architecture** with strict separation of concerns.

---

## Project Structure (6 Projects)

---

### 1. API Layer

- Project Name: `Shopping.Web.Api`

**Responsibilities:**
- Expose REST APIs to frontend applications

**Rules:**
- MUST NOT contain business logic
- Define routing at controller level
- Inject Service layer interfaces via constructor
- Return appropriate HTTP responses:
  - `BadRequest` for invalid input
  - `InternalServerError` (via middleware)

---

### 2. Service Layer (Business Logic)

- Project Name: `Shopping.Web.Service`

**Responsibilities:**
- Contains all business logic

**Rules:**
- MUST contain core application logic
- Inject Repository layer interfaces via constructor
- Perform validation, orchestration, and processing
- No direct dependency on API layer

---

### 3. Repository Layer (Data Access)

- Project Name: `Shopping.Web.Repository`

**Responsibilities:**
- Interact with Azure SQL database

**Rules:**
- Use **Entity Framework Core**
- Inject `DbContext` via constructor
- Implement a **Generic Repository** for common CRUD operations
- Reuse Generic Repository across all repositories
- Handle only data access logic (no business logic)

---

### 4. Entities Layer (Domain Models)

- Project Name: `Shopping.Web.Entities`

**Responsibilities:**
- Define domain/business entities (e.g., Product, Order, Customer)

**Rules:**
- Used by Service and Repository layers only
- MUST NOT be referenced in API layer

---

### 5. DTO Layer (Data Transfer Objects)

- Project Name: `Shopping.Web.DTO`

**Responsibilities:**
- Define DTOs for API communication

**Rules:**
- Used by API and Service layers
- MUST NOT be referenced in Repository or Entities layer
- Used for request/response models only

---

### 6. Test Project

- Project Name: `Shopping.Web.Test`

**Responsibilities:**
- Unit and integration testing

**Structure:**
- Separate folders:
  - API
  - Service
  - Repository

**Naming Conventions:**
- Controller tests → `{ControllerName}.Test.cs`
- Service tests → `{ServiceName}.Test.cs`
- Repository tests → `{RepositoryName}.Test.cs`

---

## Technology Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Azure SQL Database
- Azure Services:
  - App Service
  - Key Vault
  - Storage Account
  - Service Bus

---

## Repository & Work Management

- Source Control: GitHub
- Task Tracking: Azure DevOps (ADO)

**Rule:**
- All development must be based on ADO User Stories

---

## Development Guidelines

### Coding Standards

- Write clean, readable, maintainable, and secure code
- Follow **SOLID principles**
- Use **Dependency Injection (DI)** for all layers
- Maintain strict separation of concerns

---

### Security

- This is a **public-facing application**
- Validate all inputs
- Avoid hardcoding secrets
- Use secure coding practices

---

## Design Patterns

### Factory Pattern (MANDATORY for Payments)

Use Factory Pattern to select payment type.

**Supported Payment Methods:**
- Credit Card
- UPI
- Net Banking

**Requirement:**
- Must support easy addition of new payment methods

---

## Data Access Rules

- Use **Entity Framework Core**
- Call **Azure SQL Stored Procedures** where required
- DO NOT use any mapping libraries (e.g., AutoMapper)

---

## Middleware & Authentication

- Implement **Global Exception Handling Middleware**
- Implement **Authentication using Azure AD**

---

## Architecture Constraints

- API Layer → MUST NOT contain business logic
- Service Layer → MUST contain all business logic
- Repository Layer → Data access only
- Layers must not violate dependency rules

---

## Quality Checks

Before completing any task:

- Ensure project builds successfully
- Ensure no architecture violations
- Ensure proper dependency flow
- Follow naming conventions
