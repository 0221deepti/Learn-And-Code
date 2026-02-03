namespace BankingSystem.Models
{
    public class Loan
    {
        public int LoanId { get; }
        public Customer Borrower { get; }
        public decimal Principal { get; }
        public double AnnualInterestRate { get; }
        public int DurationInYears { get; }

        public Loan(int loanId, Customer borrower, decimal principal, double interestRate, int durationInYears)
        {
            LoanId = loanId;
            Borrower = borrower;
            Principal = principal;
            AnnualInterestRate = interestRate;
            DurationInYears = durationInYears;
        }

        public decimal CalculateTotalPayable()
        {
            return Principal + (Principal * (decimal)AnnualInterestRate * DurationInYears);
        }
    }
}
