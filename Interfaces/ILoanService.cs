using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ILoanService
    {
        Loan CreateLoan(int loanId, Customer borrower, decimal amount, double interestRate, int years);
    }
}
