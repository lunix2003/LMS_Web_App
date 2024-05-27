using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class LoginModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50), MinLength(6)]
        public string? Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}
