using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class DonationCause
    {
        [Key]
        public int DonationCauseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CauseName { get; set; } 

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}