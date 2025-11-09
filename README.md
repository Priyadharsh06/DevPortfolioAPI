# DevPortfolio API

A clean and secure **RESTful Portfolio Management API** built using **ASP.NET Core**, **SQL Server**, **JWT Authentication**, and **Role-Based Authorization**.  
The application allows users to register, log in, and manage portfolio projects, with admin-only access for project management operations.

---

## ✨ Key Features

### 1. User Authentication
- Secure password hashing using **BCrypt**
- Login returns a **JWT token** for authenticated requests

### 2. Role-Based Authorization
- Roles: **User** and **Admin**
- **Admin** can create, update, and delete projects
- **User** can view projects

### 3. Portfolio Project Management
- Create, update, delete, and view portfolio entries
- Fully RESTful endpoint design

### 4. SQL Server Integration
- **Entity Framework Core** used for Migrations and Data Access
- Automatic **Admin account seeding** on first run

### 5. Built-in Swagger UI
- Interactive API testing environment
- Supports JWT token authorization inside Swagger

---

## 🛠️ Technology Stack

| Layer | Technology |
|------|------------|
| Backend | ASP.NET Core Web API (.NET 7) |
| Database | SQL Server / LocalDB |
| ORM | Entity Framework Core |
| Authentication | JWT + BCrypt |
| CI Pipeline | GitHub Actions |

---

## 🚀 Getting Started

### Prerequisites
Ensure the following are installed:
- .NET 7 SDK
- SQL Server (LocalDB or Full)
- Visual Studio / VS Code

---

### Configuration

In `appsettings.json`, verify database connection and optionally update seed admin credentials:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DevPortfolioDb;Trusted_Connection=True;"
},
"Seed": {
  "AdminEmail": "admin@example.com",
  "AdminPassword": "AdminPassword123!"
}

## 🗄️ Database Setup

Run the Entity Framework migration to create the database schema:
dotnet ef database update

▶️ Run the API

Start the application:
dotnet run

After the API starts, Swagger UI will be available at:
https://localhost:<port>/swagger

🔐 Using Swagger with JWT Authentication

1. Register or Log in

Use the following endpoints to create or authenticate a user:


| Method | Endpoint             | Purpose                            |
| ------ | -------------------- | ---------------------------------- |
| POST   | `/api/auth/register` | Register a new user                |
| POST   | `/api/auth/login`    | Authenticate and receive JWT token |

2. Copy the Token

The login endpoint returns a JSON object like:
{
  "token": "your_jwt_token_here",
  "expiresInMinutes": 60
}
Copy the "token" value.

3. Authorize in Swagger

Click the Authorize button in Swagger UI.

Enter your token in this format:
Bearer your_token_here
Click Authorize and start making authenticated requests.

## 📌 Example Endpoints

| Method | Endpoint               | Description                       | Role Required |
|--------|------------------------|-----------------------------------|---------------|
| POST   | `/api/auth/register`   | Create a new user                 | Public        |
| POST   | `/api/auth/login`      | Log in and receive JWT            | Public        |
| GET    | `/api/projects`        | Retrieve all portfolio projects   | User / Admin  |
| POST   | `/api/projects`        | Create a new project              | Admin only    |
| PUT    | `/api/projects/{id}`   | Update a project                  | Admin only    |
| DELETE | `/api/projects/{id}`   | Delete a project                  | Admin only    |
