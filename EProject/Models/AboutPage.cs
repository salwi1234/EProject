using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class AboutPage
    {
        [Key]
        public int AboutPageId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } 

        [Required]
        public string Content { get; set; } 

        public string? ImagePath { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}