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
            Account newAccount = new Account(newID, "SEK", accountName);
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
            SavingsAccount newAccount = new SavingsAccount(newID, "SEK", 0.02f, accountName);
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt sparkonto har skapats med KontoID: {newID} och i valören SEK.");
            Console.WriteLine($"Räntan: {newAccount.GetInterestRate() * 100}% per år.");
            Console.ReadKey();
        }
        public void OpenAccount()
        {
            while (true)
            {

                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Öppna ett vanligt konto.");
                Console.WriteLine("2. Öppna ett sparkonto med 2% ränta. ");
                Console.WriteLine("3. Återgå.");

                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        OpenRegularAccount();
                        break;
                    case 2:
                        OpenSavingsAccount();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        public void RequestLoan()
        {
            //Begär ett lån
        }
        public void ViewTransaction()
        {
            //Visar en transaktionslista
        }
        public void TransferToOtherCustomers()
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
            Customer reciver = null;
            foreach (User user in BankSystem.Users)
            {
                if (user is Customer c)
                {
                    if (current == choice)
                    {
                        reciver = c;
                        break;
                    }
                    current++;
                }
            }

            // Select your account which you wanna transfer money from.
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < CustomerAccounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CustomerAccounts[i].Name} | Saldo: {CustomerAccounts[i].GetBalance()} {CustomerAccounts[i].GetCurrency()}");
            }

            int fromChoice = Backup.ReadInt("Välj konto att föra över från: ") - 1;
            if (fromChoice < 0 || fromChoice >= CustomerAccounts.Count) return;

            Account senderAccount = CustomerAccounts[fromChoice];

            // Reciver account will always be there top account(for now)
            if (reciver.CustomerAccounts.Count == 0) return;
            Account receiverAccount = reciver.CustomerAccounts[0];

            // Amount
            decimal amount = Backup.ReadDecimal("Ange summa att föra över: ");
            if (amount <= 0 || senderAccount.GetBalance() < amount) return;

            //Logging the transaction
            senderAccount.NewWithdrawl(amount);
            receiverAccount.NewDeposit(amount);


        }
        public void TransferMenu()
        {
            while (true)
            {

                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Överföring mellan egna konton.");
                Console.WriteLine("2. Överföring till annan kund. ");
                Console.WriteLine("3. Återgå.");
                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        UI.TransferInteraction(CustomerAccounts);
                        break;
                    case 2:
                        TransferToOtherCustomers();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        private void PrintAccounts(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("\nDu har inga öppna konton.\n");
                return;
            }
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("| Typ           | KontoID   | Namn                | Saldo      | Valuta |");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var acc in accounts)
            {
                string type = acc is SavingsAccount ? "Sparkonto" : "Vanligt konto";
                Console.WriteLine($"| {type,-14} | {acc.GetAccountID(),-9} | {acc.Name,-20} | {acc.GetBalance(),-10} | {acc.GetCurrency(),-6} |");
            }

            Console.WriteLine("---------------------------------------------------------------\n");

            Console.WriteLine("-------------------------------------------------\n");
        }
        public void RenameAccount(Account acc)
        {
            Console.WriteLine($"Nuvarande namn: {acc.Name}");
            Console.Write("Ange nytt namn: ");
            string newName = Console.ReadLine() ?? acc.Name;
            acc.Name = newName;
            Console.WriteLine($"Kontot har bytt namn till {newName}.");
        }
        public static void LockedOut()
        {
            //Utelåst från kontot, vänta på att admin ska låsa upp det.
        }
    }
}




