using System.ComponentModel.DataAnnotations;

namespace DevPortfolioAPI.Models
{
    public class Link
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        public string? Title { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
