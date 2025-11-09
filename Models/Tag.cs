using System.ComponentModel.DataAnnotations;

namespace DevPortfolioAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
