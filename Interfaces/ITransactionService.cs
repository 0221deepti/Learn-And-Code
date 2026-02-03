using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ITransactionService
    {
        void Deposit(Account account, decimal amount);
        void Withdraw(Account account, decimal amount);
        void Transfer(Account fromAccount, Account toAccount, decimal amount);
    }
}
