using BankingSystem.Interfaces;
using BankingSystem.Models;

namespace BankingSystem.Services
{
    public class TransactionService : ITransactionService
    {
        public void Deposit(Account account, decimal amount)
        {
            account.Deposit(amount);
        }

        public void Withdraw(Account account, decimal amount)
        {
            account.Withdraw(amount);
        }

        public void Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }
    }
}
