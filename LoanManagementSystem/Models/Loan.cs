using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class Loan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoadId { get; set; }
        public int IsHidden { get; set; } = 0;
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(Collateral))]
        public int CollateralId { get; set; }
        public Collateral? Collateral { get; set; }
        [ForeignKey(nameof(CreditOfficer))]
        public int CreditOfficerId { get; set; }
        public CreditOfficer? CreditOfficer { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime? LoanDate { get; set; }
        [Required, MaxLength(50)]
        public string? LoanCode { get; set; }
        public int LoanAmount { get; set; }
        [Required, MaxLength(1)]
        public string? Currency { get; set; }
        public double InterestRate { get; set; }
        public int PaymentFrequencyCode { get; set; }
        [MaxLength(500)]
        public string? Memo { get; set; }
        public List<LoanDetail>? LoanDetails { get; set; } = new();
    }
    
}
