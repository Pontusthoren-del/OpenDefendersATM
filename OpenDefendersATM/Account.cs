using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OpenDefendersATM
{
    internal class Account
    {
        // List that logs user transactions:
        private List<Transaction> _transactionLog = new List<Transaction>();

        public int AccountID { get; private set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "Unknown";
        public string Name { get; set; }

        // When you make a deposit, it is stored from account "CashDeposit" (0):
        private static int CashDeposit = 0;
        // When you make a withdrawl, it is stored from account "CashWithdrawl" (1):
        private static int CashWithdrawl = 1;
        public Account(int accountID, decimal balance, string currency, string name = "Nytt Konto.")
        {
            AccountID = accountID;
            Balance = balance;
            Currency = currency;
            Name = name;
        }

        // Method that adds transaction to transactionLog:
        public void LogTransaction(Transaction trans) => _transactionLog.Add(trans);

        // Method that adds and logs transaction between users accounts:
        public void NewUserTransaction(decimal amount, decimal recieverAmount, Account fromAccount, Account toAccount)
        {
            fromAccount.Balance -= amount;
            toAccount.Balance += recieverAmount;
            // Add the transaction to transactionLog:
            Transaction trans = new Transaction(amount, fromAccount.AccountID, toAccount.AccountID, Currency);
            trans.TransactionComplete();
            fromAccount.LogTransaction(trans);
            toAccount.LogTransaction(trans);
        }
        // Method that displays all transactions:
        public void ViewAllTransactions(Account account)
        {
            Console.Clear();
            if (account._transactionLog.Count == 0)
            {
                Console.WriteLine($"Konto {account.AccountID} har ingen historik.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Transaktionslogg för {account.AccountID}");
            Console.WriteLine(new string('-', 30));
            foreach (var t in account._transactionLog)
            {
                if (t.FromAccount == 0)
                {
                    Console.WriteLine($"- Insättning -");
                    Console.WriteLine($"Belopp: {t.Amount} {t.Currency}");
                    Console.WriteLine($"Till konto: {t.ToAccount}");
                    t.GetTransactionStatus();
                    Console.WriteLine($"Tidpunkt: {t.Timestamp}.");
                    Console.WriteLine(new string('-', 30));
                }
                if (t.ToAccount == 1)
                {
                    Console.WriteLine($"- Uttag -");
                    Console.WriteLine($"Belopp: {t.Amount} {t.Currency}");
                    Console.WriteLine($"Från konto: {t.FromAccount}");
                    t.GetTransactionStatus();
                    Console.WriteLine($"Tidpunkt: {t.Timestamp}.");
                    Console.WriteLine(new string('-', 30));
                }
                else if (t.FromAccount != 0 && t.ToAccount != 1)
                {
                    Console.WriteLine($"- Överföring -");
                    Console.WriteLine($"Belopp: {t.Amount} {t.Currency}");
                    Console.WriteLine($"Från konto: {t.FromAccount}");
                    Console.WriteLine($"Till konto: {t.ToAccount}");
                    t.GetTransactionStatus();
                    Console.WriteLine($"Tidpunkt: {t.Timestamp}.");
                    Console.WriteLine(new string('-', 30));
                }

            }
            Console.ReadKey();
        }
        // Method that adds and logs withdrawl:
        public void NewWithdrawl(decimal withdrawl)
        {
            Transaction trans = new Transaction(withdrawl, AccountID, CashWithdrawl, Currency);

            if (withdrawl > Balance || withdrawl < 1)
            {
                // Print fail-info;
                UI.ErrorMessage();
                Console.ReadKey();
            }
            else
            {
                // Add deposit to account balance:
                Balance -= withdrawl;
                // Log transaction
                LogTransaction(trans);

                // Print transaction info
                //UI.PrintTransactionInfo(withdrawl, AccountID, Currency, Balance);
                UI.SuccessMessage();
                Console.WriteLine($"Från konto: {AccountID}");
                Console.WriteLine($"{withdrawl:F2} - {Currency}");
                Console.WriteLine($"Nytt saldo: {Balance:F2} {Currency}.");
                Console.ReadKey();

                // Set status to complete
                trans.TransactionComplete();
                trans.GetTransactionStatus();
            }
        }
        // Method that adds and logs deposit:
        public void NewDeposit(decimal deposit, bool showMessage = true)
        {
            // Create transaction:
            var trans = new Transaction(deposit, CashDeposit, AccountID, Currency);

            // If the user puts in unvalid numbers, transaction status = declined:
            if (deposit <= 0 || deposit > 50000)
            {
                if (showMessage)
                {
                    UI.ErrorMessage();
                    Console.ReadKey();
                }
                // Print fail-info;
                trans.TransactionDeclined();
                if (showMessage) trans.GetTransactionStatus();
            }
            else
            {
                // Add deposit to account balance:
                Balance += deposit;
                // Log transaction
                LogTransaction(trans);

                // Print transaction info
                if (showMessage) UI.PrintTransactionInfo(deposit, AccountID, Currency, Balance);

                // Set status to complete
                trans.TransactionComplete();
                if (showMessage) trans.GetTransactionStatus();
            }
        }
        public void LoanDeposit(decimal deposit)
        {
            var trans = new Transaction(deposit, 0, AccountID, Currency);
            // Add deposit to account balance:
            Balance += deposit;
            // Log transaction
            LogTransaction(trans);
            // Set status to complete
            trans.TransactionComplete();
        }
        public int GetAccountID()
        {
            return AccountID;
        }
        public decimal GetBalance()
        {
            return Balance;
        }
        public string GetCurrency()
        {
            return Currency;
        }

    }
}
