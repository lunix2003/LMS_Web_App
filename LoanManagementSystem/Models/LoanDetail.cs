using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class LoanDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanDetailId { get; set; }
        [Required, ForeignKey(nameof(Loan))]
        public int LoadId { get; set; }
        public Loan? Loan { get; set;}
        public int PeriodNo { get; set; }
        public double BeginningBalance { get; set; }
        public double Principle { get; set; }
        public double Interest { get; set; }
        public double Payment { get; set; }
        public double EndingBalance { get; set; }
        public int IsPaid { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }
        public string? Note { get; set; }

    }
}
