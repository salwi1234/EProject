using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class Partner
    {
        [Key]
        public int PartnerId { get; set; }

        [Required]
        [StringLength(100)]
        public string PartnerName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? LogoPath { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}