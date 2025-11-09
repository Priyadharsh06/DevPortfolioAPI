using System.ComponentModel.DataAnnotations;

namespace DevPortfolioAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        // Role property to support role-based auth ("User" or "Admin")
        [Required]
        public string Role { get; set; } = "User";
    }
}
