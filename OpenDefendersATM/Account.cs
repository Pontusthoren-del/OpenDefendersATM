using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    public class Account
    {
        // Ide till när konto skapas: skapa en random som genererar tex 8 siffror och sätt så att den inte kan generera ett nummer som finns.

        // List that logs user transactions:
        private List<Transaction> transactionLog = new List<Transaction>();

        private int AccountID { get; set; }
        private float Balance { get; set; }
        private string Currency { get; set; } = "Unknown";


        // When you make a deposit, it is stored from account "CashDeposit" (00000000):
        private static int CashDeposit = 00000000;
        public static int CashWithdrawl = 00000001;
        public Account(int accountID, string currency)
        {
            AccountID = accountID;
            Balance = 0;
            Currency = currency;
        }
        // Deposit method:
        public void Deposit()
        {
            Console.WriteLine("=====|| Insättning ||=====\n");
            Console.WriteLine("Ange summa du vill sätta in (max 50 000):");
            // decimal deposit stores the users input:
            float deposit;
            while (!float.TryParse(Console.ReadLine(), out deposit) || deposit <= 0 || deposit > 50000)
            {
                Console.WriteLine("Ogiltig inmatning.");
            }
            // Add deposit to account balance:
            deposit += Balance;
            // Create transaction:
            var trans = new Transaction(deposit, CashDeposit, AccountID, Currency);
            // Add transaction
            transactionLog.Add(trans);
            // Print deposit info;
            Console.WriteLine("\nInsättning genomfördes:");
            Console.WriteLine($"Till konto: {AccountID}");
            Console.WriteLine($"{deposit} - {Currency}");
            Console.WriteLine($"Nytt saldo: {Balance} {Currency}.");
            trans.GetTransactionStatus();
            Console.WriteLine($"Tidpunkt: {trans.Timestamp}");



        }

        // Withdraw method:
        public void Withdraw()
        {
            Console.WriteLine("Welcome to Widrawl:");
            float withdrawl;
            while (!float.TryParse(Console.ReadLine(), out withdrawl) || withdrawl > Balance || withdrawl <= 0)
            {
                Console.WriteLine("Ogilitg inmatning, försök igen");
                withdrawl -= Balance; // abreviation (simple 'minus' mathematics for withdrawl - balance: we want to update the balance from withdrawl) by using this line of code

            }

            Transaction trans = new Transaction(withdrawl, AccountID, CashWithdrawl, Currency); // skapas en transaction
            transactionLog.Add(trans);
            Console.WriteLine("\nUttag genomfördes:");
            Console.WriteLine($"Från konto: {AccountID}");
            Console.WriteLine($"{withdrawl} - {Currency}");
            Console.WriteLine($"Nytt saldo: {Balance} {Currency}.");
            trans.GetTransactionStatus();
            Console.WriteLine($"Tidpunkt: {trans.Timestamp}");
        }


        // Method to add new transaction:
        public void AddTransaction()
        {
            //amount, currency, status, Timestamp
            Console.WriteLine("=====|| Ny överföring ||=====");
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
        public void ViewAllTransactions()
        {
            if (transactionLog.Count == 0)
            {
                Console.WriteLine($"Konto {AccountID} har ingen historik.");
            }
            Console.WriteLine($"Transaktionsloggen för {AccountID}");
            Console.WriteLine(new string('*', 30));
            foreach (var t in transactionLog)
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
