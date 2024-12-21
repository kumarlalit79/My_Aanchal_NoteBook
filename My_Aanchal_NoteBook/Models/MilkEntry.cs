using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_Aanchal_NoteBook.Models
{
    public class MilkEntry
    {
        [Key] // Marks it as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity column (auto-increment)
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters")]
        public string Code { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        [Required(ErrorMessage = "Entry Date is required")]
        public DateTime EntryDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Entry Timing is required")]
        [StringLength(50, ErrorMessage = "Entry Timing cannot exceed 50 characters")]
        public string EntryTiming { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal Fat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal Snf { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "Rate must be between 0.01 and 999999.99")]
        public decimal Bonus { get; set; }

        [StringLength(255, ErrorMessage = "Image path cannot exceed 50 characters")]
        public string Image { get; set; }

        [StringLength(50, ErrorMessage = "Remark cannot exceed 50 characters")]
        public string Remark { get; set; }

        [Required]
        [ForeignKey("User")] // Foreign key to the User table
        public int UserId { get; set; }

        public User User { get; set; } // Navigation property to User table

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
