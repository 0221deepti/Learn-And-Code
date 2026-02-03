using BankingSystem.Interfaces;
using BankingSystem.Models;
using BankingSystem.Services;
using System;
using System.Collections.Generic;

namespace BankingSystem
{
    class Program
    {
        private static readonly IAccountService _accountService = new AccountService();
        private static readonly ITransactionService _transactionService = new TransactionService();
        private static readonly ILoanService _loanService = new LoanService();

        private static readonly Dictionary<int, Account> _accounts = new Dictionary<int, Account>();

        private static int _nextAccountNumber = 1001;
        private static int _nextCustomerId = 1;
        private static int _nextLoanId = 1;

        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                DisplayMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateAccount();
                            break;
                        case "2":
                            Deposit();
                            break;
                        case "3":
                            Withdraw();
                            break;
                        case "4":
                            Transfer();
                            break;
                        case "5":
                            CreateLoan();
                            break;
                        case "6":
                            ViewBalance();
                            break;
                        case "7":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine();
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("===== BANKING SYSTEM =====");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Create Loan");
            Console.WriteLine("6. View Balance");
            Console.WriteLine("7. Exit");
        }

        private static void CreateAccount()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            var customer = new Customer(_nextCustomerId, name);
            var account = _accountService.CreateAccount(_nextAccountNumber, customer);

            _accounts.Add(account.AccountNumber, account);

            Console.WriteLine("Account created successfully.");
            Console.WriteLine("Customer ID: " + customer.Id);
            Console.WriteLine("Account Number: " + account.AccountNumber);

            _nextCustomerId++;
            _nextAccountNumber++;
        }

        private static void Deposit()
        {
            Account account = GetAccountFromUser();

            Console.Write("Enter amount to deposit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            _transactionService.Deposit(account, amount);
            Console.WriteLine("Deposit successful.");
            Console.WriteLine("New Balance: " + account.Balance);
        }

        private static void Withdraw()
        {
            Account account = GetAccountFromUser();

            Console.Write("Enter amount to withdraw: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            _transactionService.Withdraw(account, amount);
            Console.WriteLine("Withdrawal successful.");
            Console.WriteLine("New Balance: " + account.Balance);
        }

        private static void Transfer()
        {
            Console.Write("Enter sender account number: ");
            if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter receiver account number: ");
            if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            Console.Write("Enter amount to transfer: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            if (!_accounts.ContainsKey(fromAccountNumber) || !_accounts.ContainsKey(toAccountNumber))
                throw new Exception("One or both accounts not found.");

            _transactionService.Transfer(
                _accounts[fromAccountNumber],
                _accounts[toAccountNumber],
                amount
            );

            Console.WriteLine("Transfer successful.");
        }

        private static void CreateLoan()
        {
            Account account = GetAccountFromUser();

            Console.Write("Enter loan amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            Console.Write("Enter annual interest rate (e.g. 0.05): ");
            if (!double.TryParse(Console.ReadLine(), out double interestRate))
            {
                Console.WriteLine("Invalid interest rate.");
                return;
            }

            Console.Write("Enter duration in years: ");
            if (!int.TryParse(Console.ReadLine(), out int years))
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            var loan = _loanService.CreateLoan(
                _nextLoanId,
                account.Owner,
                amount,
                interestRate,
                years
            );

            Console.WriteLine("Loan created successfully.");
            Console.WriteLine("Total Payable: " + loan.CalculateTotalPayable());

            _nextLoanId++;
        }

        private static void ViewBalance()
        {
            Account account = GetAccountFromUser();
            Console.WriteLine("Current Balance: " + account.Balance);
        }

        private static Account GetAccountFromUser()
        {
            Console.Write("Enter account number: ");
            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
                throw new Exception("Invalid account number.");

            if (!_accounts.ContainsKey(accountNumber))
                throw new Exception("Account not found.");

            return _accounts[accountNumber];
        }
    }
}
