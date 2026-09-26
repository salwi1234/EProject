using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class Query
    {
        [Key]
        public int QueryId { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string MessageContent { get; set; }

        public string AdminReply { get; set; }

        public bool IsResolved { get; set; } = false;
    }
}
