using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public int IsHidden { get; set; }
        [Required, MaxLength(50)]
        public string CustomerName { get; set; } = default!;
        [MaxLength(1)]
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [MaxLength(500)]
        public string? POB { get; set; }
        [MaxLength(200)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }

        public ICollection<Loan>? Loans { get; set; }
        public List<Address>? Addresses { get; set; } = new List<Address>();

    }
}
