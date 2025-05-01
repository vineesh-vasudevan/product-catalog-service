# Product Catalog Service

A lightweight **Microservice** for managing products in an online shopping platform.  
Built with **.NET 8**, **PostgreSQL**, and modern development practices, following **CQRS** architecture.

---

## 📚 Features

#### Product Catalog microservice includes:

- ASP.NET Core Minimal APIs utilizing the latest features of .NET 8 and C# 12
- **Vertical Slice Architecture**: Each feature is isolated in its own folder; handler, validator, and response classes are grouped together in single `.cs` files for better cohesion and maintainability
- **CQRS** Pattern: Implemented using **MediatR**, separating read and write operations
- **Validation Pipelines**: Request validation using **FluentValidation** integrated via **MediatR** pipeline behaviors
- **Persistence with Marten**: Leveraging Marten for document database and event sourcing capabilities over PostgreSQL
- **Routing via Carter**: A modular and clean way to define Minimal API endpoints
- **Cross-Cutting Concerns**: Built-in Logging with **Serilog**, centralized Global Exception Handling, and comprehensive Health Checks

---

## 🚀 Tech Stack

- **.NET 8** Minimal APIs with C# 12
- **PostgreSQL** (via **Marten** as document/event store)
- **CQRS** (Command Query Responsibility Segregation) with **MediatR**
- **Vertical Slice Architecture** using Feature folders and single .cs files combining handlers, validators, and models
- **CQRS Validation Pipeline Behaviors** with **FluentValidation**
- **AutoMapper** (Object-Object Mapping)
- **CSharpFunctionalExtensions** (Functional programming helpers)
- **Carter** (Routing and modularity for Minimal APIs)
- **Serilog** (Logging)
- **Docker** (Containerization)
- **Health Checks** (Application monitoring)
- **Correlation ID Generator** (Distributed tracing)
- **Cross-cutting concerns**: Global Exception Handling, Logging, Health Monitoring

## 📡 API Endpoints

| Method | Endpoint         | Description          |
| :----: | :--------------- | :------------------- |
|  GET   | `/products`      | Get all products     |
|  GET   | `/products/{id}` | Get product by ID    |
|  POST  | `/products`      | Create a new product |
|  PUT   | `/products/{id}` | Update a product     |
| DELETE | `/products/{id}` | Delete a product     |

---

## 🛠️ Local Development

### Prerequisites

- .NET 8 SDK

- Docker & Docker Compose

- PostgreSQL (Optional if running outside Docker)

## 🔍 Health Checks

#### Health Endpoint:

```bash

GET /health
```

Returns the health status of the service and its dependencies (e.g., PostgreSQL).

## 📜 Notes

- All requests and responses are validated with FluentValidation.

- Correlation IDs are automatically generated and returned in responses for better request tracking.

- Logging is centralized and structured via Serilog, making it easy to integrate with observability platforms like Seq, ElasticSearch, or Grafana.

- Global Exception Handling provides consistent error responses.

## 📈 Planned Improvements

- Add authentication and authorization (JWT )

- Implement Retry and Circuit Breaker policies with Polly

- Increase Unit Testing Coverage
  
- Add missing integration Tests
