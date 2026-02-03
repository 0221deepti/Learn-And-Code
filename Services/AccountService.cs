using BankingSystem.Interfaces;
using BankingSystem.Models;
using System.Collections.Generic;

namespace BankingSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> _accounts = new List<Account>();

        public Account CreateAccount(int accountNumber, Customer owner)
        {
            var account = new Account(accountNumber, owner);
            _accounts.Add(account);
            return account;
        }
    }
}
