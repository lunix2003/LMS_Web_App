using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class RegisterModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string RoleName { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50), MinLength(6)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50), MinLength(6)]
        [Compare("Password", ErrorMessage = "the {0} is not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
