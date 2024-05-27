using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class CreditOfficer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditOfficerId { get; set; }
        public int IsHidden { get; set; } = 0;
        [Required, MaxLength(50)]
        public string CreditOfficerName { get; set; } = default!;
        [MaxLength(1)]
        public string Sex { get; set; } = "M";
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [MaxLength(500)]
        public string? POB { get; set; }
        [MaxLength(200)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
