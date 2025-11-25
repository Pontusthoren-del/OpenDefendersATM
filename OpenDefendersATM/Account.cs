using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        // When you make a deposit, it is stored from account "CashDeposit" (00000000):
        private static int CashDeposit = 00000000;
        // When you make a withdrawl, it is stored from account "CashWithdrawl" (00000001):
        public static int CashWithdrawl = 00000001;
        public Account(int accountID, string currency, string name = "Nytt Konto.")
        {
            AccountID = accountID;
            Balance = 10000;
            Currency = currency;
            Name = name;
        }

        // Method that adds transaction to transactionLog:
        public void LogTransaction(Transaction trans) => _transactionLog.Add(trans);

        // Method to add new transaction:
        public void AddTransaction()
        {
            //amount, currency, status, Timestamp
            Console.WriteLine("=====|| Ny överföring ||=====");
            // User enters amount:
            Console.Write("Summa: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0 || amount > Balance)
            {
                Console.WriteLine("Du måste ange en possitiv summa som inte överstiger ditt saldo.");
            }
            Console.WriteLine($"Från konto: {AccountID}");
            Console.Write($"Till konto: ");
            int toAccount;
            bool success = false;
            while (!success)
            {
                while (!int.TryParse(Console.ReadLine(), out toAccount))
                {
                    foreach (var acc in BankSystem._accounts)
                    {
                        if (acc.AccountID != toAccount)
                        {
                            Console.WriteLine("Det här kontot hittades inte.");
                        }
                        else
                        {
                            // Add the transaction to transactionLog:
                            Transaction trans = new Transaction(amount, AccountID, toAccount, Currency);
                            LogTransaction(trans);
                            // Print transaction info;
                            Console.WriteLine("Transaktion genomfördes:");
                            Console.WriteLine($"Från konto: {AccountID}");
                            Console.WriteLine($"{amount} - {Currency}");
                            Console.WriteLine($"Till konto: {toAccount}");
                            trans.GetTransactionStatus();
                            Console.WriteLine($"Tidpunkt: {trans.Timestamp}");
                            success = true;
                        }
                    }
                }
            }
        }
        public void ViewAllTransactions()
        {
            if (_transactionLog.Count == 0)
            {
                Console.WriteLine($"Konto {AccountID} har ingen historik.");
            }
            Console.WriteLine($"Transaktionsloggen för {AccountID}");
            Console.WriteLine(new string('*', 30));
            foreach (var t in _transactionLog)
            {
                t.GetTransactionStatus();
                Console.WriteLine($"Tidpunkt: {t.Timestamp}.");
                Console.WriteLine(new string('*', 30));
            }
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
        public void NewWithdrawl(decimal withdrawl)
        {
            Transaction trans = new Transaction(withdrawl, AccountID, CashWithdrawl, Currency);

            if (withdrawl > Balance || withdrawl <1)
            {
                // Print fail-info;
                Console.WriteLine("\nUttag misslyckades:");
                trans.TransactionDeclined();
                trans.GetTransactionStatus();
            }
            else
            {
                // Add deposit to account balance:
                Balance -= withdrawl;
                // Log transaction
                LogTransaction(trans);

                // Print transaction info
                UI.PrintTransactionInfo(withdrawl, AccountID, Currency, Balance);

                // Set status to complete
                trans.TransactionComplete();
                trans.GetTransactionStatus();
            }
        }
        public void NewDeposit(decimal deposit)
        {
            // Create transaction:
            var trans = new Transaction(deposit, CashDeposit, AccountID, Currency);
            
            // If the user puts in unvalid numbers, transaction status = declined:
            if (deposit <= 0 || deposit > 50000)
            {
                // Print fail-info;
                Console.WriteLine("\nInsättning misslyckades:");
                trans.TransactionDeclined();
                trans.GetTransactionStatus();
            }
            else
            {
                // Add deposit to account balance:
                Balance += deposit;
                // Log transaction
                LogTransaction(trans);
                
                // Print transaction info
                UI.PrintTransactionInfo(deposit, AccountID, Currency, Balance);
                
                // Set status to complete
                trans.TransactionComplete();
                trans.GetTransactionStatus();
            }
        }

    }
}
