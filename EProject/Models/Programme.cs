using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProject.Models
{
    public class Programme
    {
        [Key]
        public int ProgrammeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledDate { get; set; }

        public int NGOId { get; set; }

        [ForeignKey("NGOId")]
        public virtual NGO? NGO { get; set; }
    }
}