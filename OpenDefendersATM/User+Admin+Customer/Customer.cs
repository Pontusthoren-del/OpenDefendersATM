using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Customer : User
    {
        public decimal StartBalance = 0;
        public List<Account> CustomerAccounts { get; set; } = new();

        public Customer(string name, string role, int pin, decimal startbalance) : base(name, role, pin)
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
        public void ViewTransaction()
        {
            //Visar en transaktionslista
        }
        public void TransferToOtherCustomers()// 2025-12-02 (bella hemifrån): gänget, kan vi fundera kring om inte denna metod ska istället finnas under transactions.cs.
        {
            //Show all customers with a number 
            int counter = 1;
            foreach (User user in BankSystem.Users)
            {
                if (user is Customer c && c != this)
                {
                    Console.WriteLine($"{counter}. {c.Name}.");
                    counter++;
                }
            }
            if (counter == 1) return; //no customers

            int choice = Backup.ReadInt("Välj kund att föra över till: ");
            if (choice <= 0 || choice >= counter) return;
            //Find the right customer through this loop again.
            int current = 1;
            Customer reciever = null;
            foreach (User user in BankSystem.Users)
            {
                if (user is Customer c)
                {
                    if (current == choice)
                    {
                        reciever = c;
                        break;
                    }
                    current++;
                }
            }

            // Select which account you wanna transfer money from.
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < CustomerAccounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CustomerAccounts[i].Name} | Saldo: {CustomerAccounts[i].GetBalance()} {CustomerAccounts[i].GetCurrency()}");
            }

            int fromChoice = Backup.ReadInt("Välj konto att föra över från: ") - 1;
            if (fromChoice < 0 || fromChoice >= CustomerAccounts.Count) return;

            Account senderAccount = CustomerAccounts[fromChoice];

            // Reciver account will always be there top account(for now)
            if (reciever.CustomerAccounts.Count == 0) return;
            Account receiverAccount = reciever.CustomerAccounts[0];

            // Amount
            decimal amount = Backup.ReadDecimal("Ange summa att föra över: ");
            if (amount <= 0 || senderAccount.GetBalance() < amount) return;

            //Logging the transaction
            senderAccount.NewWithdrawl(amount);
            receiverAccount.NewDeposit(amount);
            Console.ReadKey();
        }
    }
}




