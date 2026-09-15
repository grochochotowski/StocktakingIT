# StocktakingIT

StocktakingIT is a team-developed inventory and stocktaking management system. It connects companies, departments, users, employees, orders, products, warehouses and stocktaking operations in a single web application.

The project combines a React client with an ASP.NET Core backend and a SQL Server database. It was developed as a local application and is not currently deployed.

## Features

- JWT-based authentication with refresh tokens
- Role-based authorization for employees, moderators and administrators
- Company and department management
- User and employee administration
- Order creation, assignment and status management
- Product catalogue and product image handling
- Stocktaking workflows linked to orders and warehouses
- Assignment of users and employees to business processes
- Warehouse product management
- Stocktaking data export to Excel
- Filtering, sorting and pagination for management views
- Swagger documentation for the backend API

## Technology stack

### Backend

- C# and .NET 8
- ASP.NET Core MVC and Web API
- Entity Framework Core with SQL Server
- JWT authentication and ASP.NET Core authorization
- AutoMapper
- FluentValidation
- EPPlus for Excel export
- Swagger / OpenAPI

### Frontend

- React 18
- React Router
- Axios
- Vite
- CSS

## Architecture

The backend follows a controller-and-service structure, with DTOs separating API contracts from the Entity Framework domain model. The React frontend communicates with the REST API and provides dedicated views for companies, orders, users, employees, products and stocktaking operations.

```text
StocktakingIT/
├── KropkaNet/           ASP.NET Core backend and MVC host
└── KropkaNetFrontEnd/   React and Vite frontend
```

## Development team

The contribution areas below are reconstructed from the repository history. They describe each contributor's primary areas rather than exclusive ownership; the team also collaborated on integration and fixes across the application.

### Michał Grochowski — [grochochotowski](https://github.com/grochochotowski)

- Full-stack integration and final application consolidation
- React interface and API integration across the principal management workflows
- Authentication, refresh-token handling and role-based authorization
- Companies, orders, products, users, employees and stocktaking functionality
- Warehouse-product operations and Excel export
- Database model changes, migrations, application configuration and project-wide fixes

### Karina Chilkiewicz — [karichil](https://github.com/karichil)

- Backend development for companies, addresses and departments
- Order, employee and product service/controller functionality
- DTO design and mapping for client-side business entities
- CRUD operations and validation-related backend updates

### Gracjan Czyżewski — [graccz103](https://github.com/graccz103)

- Backend development for stocktaking and warehouse workflows
- User services and controllers
- Stocktaking, warehouse, position and user DTOs
- Domain-model validation and backend corrections

## Project status

This repository presents the final state of a completed student team project. It is maintained as a portfolio project and currently has no hosted production version.

The authentication key included in the configuration is for local development only and must be replaced before any deployment.
