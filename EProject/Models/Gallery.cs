using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string ImagePath { get; set; } 

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
