using System.ComponentModel.DataAnnotations;

namespace DevPortfolioAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<Link> Links { get; set; } = new List<Link>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
