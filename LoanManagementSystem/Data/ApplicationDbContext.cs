using LoanManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var passwordhash = new PasswordHasher<string>();
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Administrator",ConcurrencyStamp = "1",NormalizedName = "Administrator" },
                new IdentityRole { Name = "Loan Auditor", ConcurrencyStamp = "2",NormalizedName = "Loan Auditor" },
                new IdentityRole { Name = "Collections Agent", ConcurrencyStamp = "3",NormalizedName = "Collections Agent" }
           );
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser { Email = "admin@lms.com",EmailConfirmed = true,UserName = "admin@lms.com" ,PasswordHash =  passwordhash.HashPassword("admin@lms.com","P@$$w0rd"),ConcurrencyStamp = "1" }

            );
            
        }
    }
}
