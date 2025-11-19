using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Account
    {
        // List that logs user transactions:
        private List<Transaction> transactionLog = new List<Transaction>();

        private int AccountID { get; set; }
        private float Balance { get; set; }
        private string Currency { get; set; } = "Unknown";
        

        public Account(int accountID, string currency)
        {
            AccountID = accountID;
            Balance = 0;
            Currency = currency;
        }
        // Deposit method:
        public void Deposit()
        {


        }

        // Withdraw method:
        public void Withdraw()
        {

        }

        // Method to add new transaction:
        public void AddTransaction()
        {
            //amount, currency, status, Timestamp
            Console.WriteLine("Ny överföring:");
            // User enters amount:
            Console.Write("Summa: ");
            float amount;
            while (!float.TryParse(Console.ReadLine(), out amount) || amount < 0 || amount > Balance)
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
                            transactionLog.Add(trans);
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
        public int GetAccountID()
        {
            return AccountID;
        }
        public float GetBalance()
        {
            return Balance;
        }
        public string GetCurrency()
        {
            return Currency;
        }
    }
}
