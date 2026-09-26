using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProject.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CauseCategory { get; set; }

        [Required(ErrorMessage = "Card number is required")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be exactly 16 digits")]
        public string DummyCardNumber { get; set; }

        [Required(ErrorMessage = "Expiry format MM/YY required")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be exactly 3 digits")]
        public string CVV { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required]
        public int NGOId { get; set; }

        [ForeignKey("NGOId")]
        public virtual NGO? NGO { get; set; }
    }
}