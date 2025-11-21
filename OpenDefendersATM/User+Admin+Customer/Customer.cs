using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Customer : User
    {
        public List<Account> CustomerAccounts { get; set; } = new();

        public Customer(string name, string role, int pin) : base(name, role, pin)
        {

        }

        // Method to bring/show a specific user's TOTAL balance within their account
        public float TotalBalance()
        {
            float totalBalance = 0;
            foreach (var acc in CustomerAccount)
            {
                totalBalance += acc.GetBalance();
            }
            return totalBalance;
        }
        //Method to show our accounts.
        public void ViewAccounts()
        {
            if (CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
            }
            Console.Write("Dina konton: ");
            foreach (var acc in CustomerAccounts)
            {
                Console.WriteLine($"KontoID: {acc.GetAccountID()} | Saldo:{acc.GetBalance()} {acc.GetCurrency()}. ");
            }
        }
        public void TransferBetweenAccounts()
        {
            if (CustomerAccounts.Count < 2)
            {
                Console.WriteLine("Du måste ha minst två konton för att föra över mellan dina egna konton.");
                return;
            }

            Console.WriteLine("Dina konton:");
            foreach (var acc in CustomerAccounts)
            {
                Console.WriteLine($"KontoID: {acc.GetAccountID()} | Saldo: {acc.GetBalance()} {acc.GetCurrency()}");
            }

            // Välj avsändarkonto
            int fromID = Backup.ReadInt("Ange KontoID du vill överföra FRÅN: ");
            Account? fromAccount = CustomerAccounts.FirstOrDefault(a => a.GetAccountID() == fromID);

            if (fromAccount == null)
            {
                Console.WriteLine("Avsändarkontot hittades inte.");
                return;
            }

            // Anropa AddTransaction() på det valda kontot
            fromAccount.AddTransaction();
        }

        //Method to open a new account with a unique account ID and the print it.
        public void OpenAccount()
        {
            //Generate a random unique AccountID and put it in newID.
            Random random = new Random();
            int newID;
            do
            {
                newID = random.Next(100000, 999999);
            }
            while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            Account newAccount = new Account(newID, "SEK");
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt konto har skapats med KontoID: {newID} och i valören SEK.");
            Console.ReadKey();
        }
        public void OpenSavingsAccount()
        {
            //Öppnar ett nytt sparkonto
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
            //Flytta pengar till en annan kund
        }
        public void TransferMenu()
        {
            Console.WriteLine("Välj ett alternativ.");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("1. Överföring mellan egna konton.");
            Console.WriteLine("2. Överföring till annan kund. ");
            Console.WriteLine("3. Återgå.");
            string inputStr = Console.ReadLine() ?? "";
            int input;
            if (int.TryParse(inputStr, out input))
            {

                switch (input)
                {
                    case 1:
                        TransferBetweenAccounts();
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
            }
        }
        public static void LockedOut()
        {
            //Utelåst från kontot, vänta på att admin ska låsa upp det.
        }
    }
}




