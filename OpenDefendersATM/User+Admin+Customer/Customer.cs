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
            foreach (var acc in CustomerAccounts)
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
                return;
            }
            Console.Write("Dina konton.");
            foreach (var acc in CustomerAccounts)
            {
                Console.WriteLine($"KontoID: {acc.GetAccountID()} | Saldo:{acc.GetBalance()} {acc.GetCurrency()}. ");
            }
        }
        public void TransferBetweenAccounts()
        {
            //Flytta pengar mellan egna konton
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
            } while (BankSystem.AllAccounts().Any(a => a.GetAccountID() == newID)); //Keep generating a new ID **as long as** it already exists in the bank
            Account newAccount = new Account(newID, "SEK");
            CustomerAccounts.Add(newAccount);
            Console.WriteLine($"Nytt konto har skapats med KontoID: {newID}.");
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
        public static void LockedOut()
        {
            //Utelåst från kontot, vänta på att admin ska låsa upp det.
        }
    }
}
