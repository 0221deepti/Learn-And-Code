using BankingSystem.Interfaces;
using BankingSystem.Models;
using System.Collections.Generic;

namespace BankingSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly List<Loan> _loans = new List<Loan>();

        public Loan CreateLoan(int loanId, Customer borrower, decimal amount, double interestRate, int years)
        {
            var loan = new Loan(loanId, borrower, amount, interestRate, years);
            _loans.Add(loan);
            return loan;
        }
    }
}
