using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Customer : User
    {
        public int StartBalance = 0;
        public List<Account> CustomerAccounts { get; set; } = new();

        public Customer(string name, string role, int pin, int startbalance) : base(name, role, pin)
        {
            this.StartBalance = startbalance;
        }

        // Method to bring/show a specific user's TOTAL balance within their account
        public decimal TotalBalance() // lämna kvar denna funktionella
        {
            decimal totalBalance = 0;
            foreach (var acc in CustomerAccounts)
            {
                totalBalance += acc.GetBalance();
            }
            return totalBalance;
        }
        //Method to show our accounts.
        //Method to transfer betweeen our own accounts
        //public void TransferBetweenAccounts(decimal amount, Account fromAccount, Account toAccount)
        //{
            
        //    fromAccount.AddTransaction(amount, fromAccount, toAccount);

        //}

        //Method to open a new account with a unique account ID and the print it.
        public void OpenRegularAccount()
        {
            //Generate a random unique AccountID and put it in newID.
            Console.WriteLine("Ange namn för ditt konto: ");
            string? accountName = Console.ReadLine() ?? "Nytt konto";

            Random random = new Random();
            int newID;
            do
            {
                newID = random.Next(100000, 999999);
            }
            while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            Account newAccount = new Account(newID,0, "SEK", accountName);
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt konto har skapats med KontoID: {newID} och i valören SEK.");
            Console.ReadKey();
        }
        public void OpenSavingsAccount()
        {
            Console.WriteLine("Ange namn för ditt sparkonto: ");
            string? accountName = Console.ReadLine() ?? "Nytt sparkonto";
            Random random = new Random();
            int newID;
            do
            {
                newID = random.Next(100000, 999999);
            }
            while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            SavingsAccount newAccount = new SavingsAccount(newID, 0, "SEK",0.02f,"Nytt sparkonto");
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt sparkonto har skapats med KontoID: {newID} och i valören SEK.");
            Console.WriteLine($"Räntan: {newAccount.GetInterestRate() * 100}% per år.");
            Console.ReadKey();
        }
      
        public void RequestLoan()
        {
            //Begär ett lån
        }
        public void TransferToOtherCustomers()
        {
            Account receiverAccount = null;
            while (receiverAccount == null)
            {
                int targetAccountID = Backup.ReadInt("Ange KontoID att föra över till: ");
                foreach (User user in BankSystem.Users)
                {
                    if (user is Customer c)
                    {
                        receiverAccount = c.CustomerAccounts.FirstOrDefault(a => a.GetAccountID() == targetAccountID);
                        if (receiverAccount != null) break;
                    }
                }
                if (receiverAccount == null)
                {
                    Console.WriteLine("Kontot hittades inte. Försök igen.");
                }
            }
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < CustomerAccounts.Count; i++)
            {
                var acc = CustomerAccounts[i];
                Console.WriteLine($"{i + 1}. {acc.Name} | Saldo: {acc.GetBalance()} {acc.GetCurrency()} | KontoID: {acc.GetAccountID()}");
            }
            int fromChoice = Backup.ReadInt("Välj konto att föra över från (nummer): ") - 1;
            if (fromChoice < 0 || fromChoice >= CustomerAccounts.Count)
            {
                Console.WriteLine("Felaktigt val.");
                return;
            }
            Account senderAccount = CustomerAccounts[fromChoice];
            decimal amount = Backup.ReadDecimal("Ange summa att föra över: ");
            if (amount <= 0 || senderAccount.GetBalance() < amount)
            {
                Console.WriteLine("Felaktigt belopp.");
                return;
            }
            senderAccount.NewWithdrawl(amount);
            receiverAccount.NewDeposit(amount);
            Console.WriteLine($"Överföring av {amount} {senderAccount.GetCurrency()} till konto {receiverAccount.GetAccountID()} genomförd.");
            Console.ReadKey();
        }
    }
}




