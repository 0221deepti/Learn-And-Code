using System;

namespace BankingSystem.Models
{
    public class Account
    {
        public int AccountNumber { get; }
        public Customer Owner { get; }
        public decimal Balance { get; private set; }

        public Account(int accountNumber, Customer owner)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            ValidateAmount(amount);
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            ValidateAmount(amount);

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance.");

            Balance -= amount;
        }

        private void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
        }
    }
}
