using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Account
    {
        // Ide till när konto skapas: skapa en random som genererar tex 8 siffror och sätt så att den inte kan generera ett nummer som finns.

        // List that logs user transactions:
        private List<Transaction> transactionLog = new List<Transaction>();

        private int AccountID { get; set; }
        private float Balance { get; set; }
        private string Currency { get; set; } = "Unknown";

        // When you make a deposit, it is stored from account "CashDeposit" (00000000):
        private static int CashDeposit = 00000000;

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
            // Display deposit info:
            Console.WriteLine($"Du satte in {deposit} {Currency} till konto {AccountID}");
            // Add deposit to account balance:
            deposit += Balance;
            Console.WriteLine($"kontots saldo är nu {Balance} {Currency}.");

            // Create transaction:
            var trans = new Transaction(deposit, CashDeposit, AccountID, Currency);
            // Add transaction
            transactionLog.Add(trans);
        }

        // Withdraw method:
        public void Withdraw()
        {

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
                            trans.GetStatus();
                            Console.WriteLine($"Tidpunkt: {DateTime.Now}");
                            success = true;
                        }
                    }
                }
            }
            

            

            
        }
    }
}
