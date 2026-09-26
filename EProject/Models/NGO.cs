using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EProject.Models
{
    public class NGO
    {
        [Key]
        public int NGOId { get; set; }

        [Required]
        [StringLength(50)]

        public string NGOName { get; set; }

        [Required]
        public string  MissionStatement { get; set; }

        [Required]
        public string LogoPath { get; set; }

        public string ContactEmail { get; set; }
    }
}
