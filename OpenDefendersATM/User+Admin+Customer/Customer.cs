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

        public List<Loan> CustomerLoans = new List<Loan>();

        public Customer(string name, string role, string pin, decimal startbalance) : base(name, role, pin)
        {
            
            this.StartBalance = startbalance;

        }

        public void AddLoanToList(Loan loan)
        {
            CustomerLoans.Add(loan);
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

        //Method to open a new account with a unique account ID and the print it. // (sätt in valuta)
        public void OpenRegularAccount()
        {
            //Generate a random unique AccountID and put it in newID.
            Console.WriteLine("Ange namn för ditt konto: ");
            string? accountName = Console.ReadLine() ?? "Nytt konto";
            Console.WriteLine("Ange valuta för kontot: ");
            Console.WriteLine("1. SEK");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. USD");
            bool inputOK = false;
            string? currencyInput = null;
            while (!inputOK)
            {
                currencyInput = Console.ReadLine();
                if (currencyInput == "1" || currencyInput == "2" || currencyInput == "3")
                {
                    inputOK = true;
                }
                else
                {
                    Console.WriteLine("\nFelaktigt val.");
                    inputOK = false;
                }
            }


                string currency = null;
            if (currencyInput == "1")
            {
                currency = "SEK";
            }

            
            if (currencyInput == "2")
            {
                currency = "EUR";
            }

            if (currencyInput == "3")
            {
                currency = "USD";
            }


            Random random = new Random();
            int newID;
            do
            {
                newID = random.Next(10000, 99999);
            }
            while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            Account newAccount = new Account(newID,0, currency, accountName);
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt konto har skapats med KontoID: {newID} och i valören {currency}.");
            Console.ReadKey();
        }
        public void OpenSavingsAccount()
        {
            Console.WriteLine("Ange namn för ditt sparkonto: ");
            string? accountName = Console.ReadLine() ?? "Nytt sparkonto";
            bool inputOK = false;
            string currencyInput = null;
            Console.WriteLine("\nAnge valuta för kontot: ");
            Console.WriteLine("1. SEK ");
            Console.WriteLine("2. USD ");
            Console.WriteLine("3. EUR ");
            while (!inputOK)
            {
                
                currencyInput = Console.ReadLine();


                if (currencyInput == "1" || currencyInput == "2" || currencyInput == "3")
                {
                    
                    inputOK = true;
                }
                else
                {
                    Console.WriteLine("Felaktigt val.");
                    inputOK = false;
                }
            }
            
            string currency = null;
            if (currencyInput == "1")
            {
                currency = "SEK";
            }
            if (currencyInput == "2")
            {
                currency = "USD";
            }
            if (currencyInput == "3")
            {
                currency = "EUR";
            }
            Random random = new Random();
            int newID;
            do
            {
                newID = random.Next(10000, 99999);
            }
            while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            SavingsAccount newAccount = new SavingsAccount(newID, 0, currency,0.02f,"Nytt sparkonto");
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt sparkonto har skapats med KontoID: {newID} och i valören {currency}.");
            Console.WriteLine($"Räntan: {newAccount.GetInterestRate() * 100}% per år.");
            Console.ReadKey();
        }
      
        public void RequestLoan()
        {
            //Begär ett lån
        }
    }
}




