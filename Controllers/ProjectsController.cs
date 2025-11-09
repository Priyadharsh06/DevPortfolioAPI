using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolioAPI.Data;
using DevPortfolioAPI.Models;
using System.Security.Claims;

namespace DevPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProjectsController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? tag, [FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = _db.Projects.Include(p => p.Tags).Include(p => p.Links).AsQueryable();

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(p => p.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || (p.Description ?? string.Empty).Contains(search));
            }

            var total = await query.CountAsync();
            var items = await query.OrderBy(p => p.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    Tags = p.Tags.Select(t => t.Name).ToList(),
                    Links = p.Links.Select(l => new { l.Id, l.Title, l.Url }).ToList()
                })
                .ToListAsync();

            var result = new { total, page, pageSize, items };
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetProjectById")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.Include(p => p.Tags).Include(p => p.Links).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create([FromBody] Project model)
        {
            // Set owner to logged-in user
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            model.OwnerId = userId;
            _db.Projects.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetProjectById", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}"), Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Project update)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var isAdmin = User.IsInRole("Admin");

            if (project.OwnerId != userId && !isAdmin)
                return Forbid();

            project.Title = update.Title;
            project.Description = update.Description;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
