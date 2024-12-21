using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_Aanchal_NoteBook.Models
{
    public class User
    {
        [Key] // Marks it as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity column (auto-increment)
        public int UserId { get; set; }


        [Required(ErrorMessage = "Code is required")]
        [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Code must be 1 to 4 digits")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")] // Makes Name a required field
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Husband's Name cannot exceed 50 characters")]
        public string HusbandName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")] // Ensures valid email format
        [StringLength(50, ErrorMessage = "Email ID cannot exceed 50 characters")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")] // Validates phone number format
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
        public string PhoneNumber { get; set; }
        

        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }

        public string DairyName { get; set; }
        public string DairyLocation { get; set; }
        public string SachiveName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now; // Default value as current date-time

        [StringLength(50, ErrorMessage = "Created By cannot exceed 50 characters")]
        public string CreatedBy { get; set; }

        [StringLength(20, ErrorMessage = "OTP cannot exceed 20 characters")]
        public string Otp { get; set; }

        public DateTime? OtpCreatedOn { get; set; } // Nullable in case OTP is not generated

        public DateTime? OtpEndsOn { get; set; }
    }
}
