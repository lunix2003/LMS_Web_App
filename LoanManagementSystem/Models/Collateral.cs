using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class Collateral
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollateralId { get; set; }
        public int IsHidden { get; set; }
        [Required, MaxLength(50)]
        public string? CollateralCode { get; set; }
        [Required,MaxLength(50)]
        public string? OwnerName { get; set; }
        [Required, MaxLength(50)]
        public string? OwnerNationalCardNumber { get; set; }
        [Required, ForeignKey(nameof(CollateralType))]
        public int CollateralTypeId { get; set; }
        public CollateralType? CollateralType { get; set; }
        [MaxLength(500)]
        public string? CollateralDescription { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
