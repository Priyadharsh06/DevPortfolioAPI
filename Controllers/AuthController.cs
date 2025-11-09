using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolioAPI.Data;
using DevPortfolioAPI.Models;
using DevPortfolioAPI.DTOs;
using DevPortfolioAPI.Services;
using Microsoft.Extensions.Configuration;

namespace DevPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IJwtTokenService _jwt;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IJwtTokenService jwt, IConfiguration config)
        {
            _db = db;
            _jwt = jwt;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            if (await _db.Users.AnyAsync(u => u.Email == req.Email))
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                Name = req.Name,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = "User"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var token = _jwt.GenerateToken(user);
            return Created("", new AuthResponse(token, _getExpiryMinutes()));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwt.GenerateToken(user);
            return Ok(new AuthResponse(token, _getExpiryMinutes()));
        }

        private int _getExpiryMinutes()
        {
            return _config.GetSection("Jwt").GetValue<int>("ExpiryMinutes");
        }
    }
}
