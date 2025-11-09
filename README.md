DevPortfolio API

This project is a RESTful Portfolio Management API built with ASP.NET Core, SQL Server, and JWT-Based Authentication.
It allows users to register, log in, and manage portfolio projects. Role-based access control ensures only administrators can perform secure management operations.

Key Features

User Authentication

Register and log in with secure password hashing

Generates JWT tokens for authenticated access

Role-Based Authorization

Standard User and Admin roles

Admins have permissions to create, update, and delete projects

Portfolio Project Management

Add, edit, fetch, and delete portfolio items

Clean, REST-style endpoints following best practices

SQL Server Database Integration

Entity Framework Core for migrations and data access

Automatic admin account seeding on first run

API Documentation Using Swagger

Built-in Swagger UI for testing endpoints

Supports JWT token authentication inside Swagger

Technology Stack
Component	Technology
Backend Framework	ASP.NET Core Web API (.NET 7)
Authentication	JWT + BCrypt password hashing
Database	SQL Server (LocalDB or full instance)
ORM	Entity Framework Core
DevOps	GitHub Actions CI
Getting Started
Prerequisites

.NET 7 SDK

SQL Server or LocalDB

Visual Studio / VS Code

Setup Instructions

Update the database connection in appsettings.json if required.

Run migrations:

dotnet ef database update


Start the API:

dotnet run

Seeding Admin User

The admin account is created automatically based on the values in appsettings.json:

"Seed": {
  "AdminEmail": "admin@example.com",
  "AdminPassword": "AdminPassword123!"
}


You can modify these before the first run.

Using Swagger Authentication

Start the API and open Swagger.

Authenticate using /api/auth/login

Copy the returned token.

Click Authorize in Swagger and paste:

Bearer <your token>

Example Endpoints
Method	Endpoint	Description	Roles
POST	/api/auth/register	Register a new user	Public
POST	/api/auth/login	Log in and receive JWT token	Public
GET	/api/projects	View projects	User / Admin
POST	/api/projects	Add new project	Admin only