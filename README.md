# DevPortfolio API

A clean and secure **RESTful Portfolio Management API** built using **ASP.NET Core**, **SQL Server**, **JWT Authentication**, and **Role-Based Authorization**.  
The application enables user registration, login, and portfolio project management, while restricting project modification operations to admin users.

---

## ✨ Key Features

### 1. User Authentication
- Secure password hashing using **BCrypt**
- JWT token generation for authenticated access

### 2. Role-Based Authorization
- Roles: **User** and **Admin**
- **Admin** users can create, update, and delete projects
- **User** role can view project listings

### 3. Portfolio Project Management
- CRUD operations for portfolio entries
- Follows REST API best practices

### 4. SQL Server Integration
- **Entity Framework Core** for migrations and data persistence
- Automatic **Admin user seeding** on first run

### 5. Swagger API Documentation
- Interactive UI for testing endpoints
- Supports JWT token authorization directly in Swagger

---

## 🛠️ Technology Stack

| Layer | Technology |
|------|------------|
| Backend | ASP.NET Core Web API (.NET 7) |
| Database | SQL Server / LocalDB |
| ORM | Entity Framework Core |
| Security | JWT + BCrypt |
| CI/CD | GitHub Actions |

---

## 🚀 Getting Started

### Prerequisites
Make sure the following are installed:
- .NET 7 SDK
- SQL Server (LocalDB or full)
- Visual Studio / VS Code

---

## ⚙️ Configuration

Update database connection and seed admin details in `appsettings.json`:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DevPortfolioDb;Trusted_Connection=True;"
},
"Seed": {
  "AdminEmail": "admin@example.com",
  "AdminPassword": "AdminPassword123!"
}
## 🗄️ Database Setup

Run the database migrations to create the schema:
dotnet ef database update

## ▶️ Running the API
Start the application:
dotnet run
Swagger UI will be available at:
https://localhost:<port>/swagger

## 🔐 Using Swagger with JWT Authentication

## 1. Register or Log In

Use the following endpoints:

| Method | Endpoint             | Purpose                            |
|--------|----------------------|------------------------------------|
| POST   | `/api/auth/register` | Create a new user                  |
| POST   | `/api/auth/login`    | Authenticate and receive JWT token |


## 2. Copy the Returned Token

Example login response:

{
  "token": "your_jwt_token_here",
  "expiresInMinutes": 60
}
Copy the value of "token".

## 3. Authorize in Swagger

Open Swagger UI

Click Authorize

Enter your token in this exact format:

Bearer your_token_here

Click Authorize → You are now authenticated.

## 📌 Example Endpoints

| Method | Endpoint               | Description                       | Role Required |
|--------|------------------------|-----------------------------------|---------------|
| POST   | `/api/auth/register`   | Register a new user               | Public        |
| POST   | `/api/auth/login`      | Log in and receive JWT            | Public        |
| GET    | `/api/projects`        | Retrieve all portfolio projects   | User / Admin  |
| POST   | `/api/projects`        | Create a new project              | Admin only    |
| PUT    | `/api/projects/{id}`   | Update a project                  | Admin only    |
| DELETE | `/api/projects/{id}`   | Delete a project                  | Admin only    |
