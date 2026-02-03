using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface IAccountService
    {
        Account CreateAccount(int accountNumber, Customer owner);
    }
}
