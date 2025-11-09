using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolioAPI.Data;
using DevPortfolioAPI.Models;

namespace DevPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) { _db = db; }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users.Select(u => new { u.Id, u.Name, u.Email, u.Role }).ToListAsync();
            return Ok(users);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> AssignRole([FromQuery] int userId, [FromQuery] string role)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return NotFound();
            user.Role = role;
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
