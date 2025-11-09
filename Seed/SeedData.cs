using DevPortfolioAPI.Data;
using DevPortfolioAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevPortfolioAPI.Seed
{
    public class SeedData
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        public SeedData(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task SeedAdminIfMissing()
        {
            await _db.Database.MigrateAsync();
            var adminEmail = _config.GetSection("Seed").GetValue<string>("AdminEmail");
            var adminPassword = _config.GetSection("Seed").GetValue<string>("AdminPassword");

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword)) return;

            var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
            if (existing != null) return;

            var admin = new User
            {
                Name = "Administrator",
                Email = adminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                Role = "Admin"
            };

            _db.Users.Add(admin);
            await _db.SaveChangesAsync();
        }
    }
}
