# 🛒 Survey Basket API

A scalable **ASP.NET Core Web API** built using **Clean Architecture principles**, designed for managing surveys, polls, users, and voting systems.

---

## 🚀 Features

* 🔐 Authentication & Authorization (JWT + Refresh Tokens)
* 👥 User & Role Management
* 📊 Polls & Voting System
* ✅ Validation using FluentValidation
* 🧠 Clean Architecture (Core / Infrastructure / API)
* ⚡ Result Pattern for consistent responses
* 🧩 Modular & maintainable design

---

## 🏗 Architecture

The project follows **Clean Architecture** with clear separation of concerns:

### 🔹 Core Layer

* Domain Entities
* Value Objects
* Application DTOs
* Interfaces (Contracts)
* Business Logic
* Validation Rules

### 🔹 Infrastructure Layer

* Entity Framework Core (DbContext)
* Fluent API Configurations
* Identity (UserManager, Roles)
* JWT Authentication
* External Services

### 🔹 API Layer

* Controllers
* Middleware
* Dependency Injection
* Endpoints

---

## 📂 Project Structure

```
src/
├── Core/
│   ├── Domain/
│   ├── Application/
│
├── Infrastructure/
│   ├── Persistence/
│   ├── Identity/
│   ├── Services/
│
├── API/
│   ├── Controllers/
│   ├── Extensions/
```

---

## 🛠 Technologies

* ASP.NET Core
* Entity Framework Core
* SQL Server
* JWT Authentication
* FluentValidation
* Mapster
* Clean Architecture

---

## 🔐 Authentication Flow

* Login returns:

  * Access Token (JWT)
  * Refresh Token
* Access token is used for API requests
* Refresh token is used to renew expired access tokens

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/e-ahmed-mahmoud/Survey-Basket-ahmed6618.git
cd Survey-Basket-ahmed6618
```

---

### 2. Update Connection String

In `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "your_sql_server_connection"
}
```

---

### 3. Run Migrations

```bash
dotnet ef database update
```

---

### 4. Run the project

```bash
dotnet run
```

---

## 📬 API Documentation

Swagger available at:

```
https://localhost:<port>/swagger
```

---

## 🧠 Design Decisions

* No Generic Repository (to avoid unnecessary abstraction)
* Direct query projection for performance
* Separation between business logic and infrastructure
* DTO-based communication between layers

---

## 👨‍💻 Author

Ahmed Mahmoud

mailto:ahmed.mahmoud.6618@gmail.com
