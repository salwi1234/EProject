using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Full Name is required")]
        [StringLength(50, ErrorMessage = "NGO Name cannot exceed 50 characters")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public  string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        
        public string password {  get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits")]
        public string  PhoneNumber { get; set; }

        [Required]
        public string ProfessionalBackground { get; set; }

        public string Role { get; set; } = "User";

    }
}
