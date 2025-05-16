# 🧩 Microservice-Based Product API

A scalable, modular microservice application built with .NET Minimal APIs. This project demonstrates a clean architecture approach with JWT-based authentication, service communication using a message queue, and advanced tools like MediatR, FluentValidation, and Mapster.

---

## 🏗️ Technologies Used

- **.NET 8 Minimal API**
- **Microservices Architecture**
- **JWT Authentication + ASP.NET Identity**
- **MediatR** – for request/command handling
- **FluentValidation** – for validating incoming requests
- **Mapster** – for efficient object mapping
- **Message Queue** – (e.g., RabbitMQ or Kafka for inter-service communication)
- **Entity Framework Core**
- **SQL Server/MySQL**

---

## 📦 Microservices Structure

- **Auth Service**
  - Handles user registration, login, email confirmation, password reset
  - Generates JWT tokens
- **Product Service**
  - Manages product catalog (CRUD)
- **Order Service**
  - Manages user orders and order tracking
- **Notification Service**
  - Consumes messages from a queue and sends notifications (e.g., email/SMS)

Each service is independent and communicates via HTTP or asynchronous messaging using a message broker.

---

## 🔐 Authentication & Authorization

- ASP.NET Core Identity is used to manage users and roles.
- JWT is used for secure and stateless authentication.
- Authenticated routes are protected using `[Authorize]` attribute.

---

## 💬 CQRS & MediatR

- Commands and Queries are separated.
- MediatR handles the routing of requests to the appropriate handlers.

---

## ✅ Validation

- FluentValidation is used to validate all incoming requests.
- Validators are automatically wired up via DI.

---

## 🔁 Object Mapping

- Mapster is used instead of AutoMapper for faster and cleaner object transformations.

---

## 📡 Message Queue (Event-Driven)

- Events like `OrderPlaced`, `UserRegistered`, etc., are published to a message queue.
- Other services subscribe to relevant events to react (e.g., NotificationService sending an email after user registration).

---

## 🚀 Getting Started

1. **Clone the Repository**

   ```bash
   git clone https://github.com/lotfi029/Product-Management.git
