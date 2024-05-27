using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class CollateralType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollateralTypeId { get; set; }
        [Required,MaxLength(50)]
        public string CollateralTypeName { get; set; } = string.Empty;
        public ICollection<Collateral>? Collaterals { get; set; }
    }
}
