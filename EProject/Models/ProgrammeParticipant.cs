using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProject.Models
{
    public class ProgrammeParticipant
    {
        [Key]
        public int ProgrammeParticipantId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        public int ProgrammeId { get; set; }

        [ForeignKey("ProgrammeId")]
        public virtual Programme? Programme { get; set; }

        public DateTime ParticipationDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Interested";
    }
}