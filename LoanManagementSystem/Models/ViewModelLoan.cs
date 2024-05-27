namespace LoanManagementSystem.Models
{
    public class ViewModelLoan
    {
        public Loan Loan { get; set; }
        public List<LoanDetail> LoanDetail { get; set; }
        public Customer Customer { get; set; }
        public CreditOfficer CreditOfficer { get; set; }
        public Collateral Collateral { get; set; }
        public CollateralType CollateralType { get; set; }
    }
}
