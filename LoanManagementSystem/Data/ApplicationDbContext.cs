using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Collateral> Collateral { get; set; }
        public DbSet<CollateralType> CollateralType { get; set;}
        public DbSet<CreditOfficer> CreditOfficer { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanDetail> LoadDetails { get; set; }
        public DbSet<RegisterModel> RegisterModel { get; set; } = default!;
        public DbSet<LoginModel> LoginModel { get; set; } = default!;
    }
}
