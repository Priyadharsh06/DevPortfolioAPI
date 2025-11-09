# DevPortfolioAPI — RESTful Portfolio API (SQL Server) + Role-based Auth
This project extends the RESTful API with **role-based authorization** (User/Admin). Highlights:
- JWT tokens include role claims (ClaimTypes.Role)
- Authorization policies and `[Authorize(Roles = "Admin")]` usage
- Seeded Admin user for development: see `appsettings.json` -> Seed.AdminEmail

## Seed admin (development only)
On startup the app will ensure an admin user exists using credentials in `appsettings.json:Seed`.
Change the password before publishing or manage users through an Admin panel.

## Admin endpoints
- `GET /api/admin/users` — list users (Admin only)
- `POST /api/admin/roles` — assign roles (Admin only)

## Resume bullet suggestion
- Implemented role-based authorization in a RESTful ASP.NET Core API using JWT role claims and authorization policies; created admin seeding and protected admin endpoints.

Follow previous quickstart steps from the parent README to run locally.
