using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class BankSystem
    {
        private static Dictionary<string, decimal> _exchangeRates;  // Private dictionary
        public static List<User> Users { get; set; } = new()
        {
         new Admin("Petter", "Admin", 1234),
         new Customer("Pontus", "Customer", 1111,1000),
         new Customer("Bella", "Customer", 2222,1000),
         new Customer("Robin", "Customer", 3333,1000),
         new Customer("Julia", "Customer", 4444,1000),
         new Customer("Kalle", "Customer", 5555,1000)
        };
        public static List<Account> Accounts { get; set; } = new List<Account>();
        //private List<Transaction> _transactions { get; set; }  // Do we need this one?
        public static Dictionary<string, decimal> ExchangeRates { get; } // Visual dictionary

        public static void InitializeStartAccounts()
        {
            foreach (User user in Users)
            {
                if (user is Customer c && c.CustomerAccounts.Count == 0)
                {
                    Random random = new Random();
                    int newID;
                    do
                    {
                        newID = random.Next(100000, 999999);
                    } while (AllAccounts().Any(a => a.GetAccountID() == newID));

                    Account startAccount = new Account(newID, "SEK", "Privatkonto");
                    startAccount.NewDeposit(c.StartBalance,false);
                    c.CustomerAccounts.Add(startAccount);
                }
            }
        }
        public void ProcessScheduleTransaction()// Bankens system behöver en metod som kollar transaktionerna (en kontrollmetod som loopar igenom alla transaktioner i pending-listan)
        {
            if (DateTime.Now >= transaction.EarliestExecution)
            {
                run.transaction

            }
            else
            {

            }


        }

        //A list with AllAccounts, but we just sorting out the customers.
        public static List<Account> AllAccounts()
        {
            return Users
            .OfType<Customer>() //Only Customers
            .SelectMany(c => c.CustomerAccounts) //Adding all accounts to a list.
            .ToList();
        }

        // Method for adding exchange rates to private dictionary _exchangeRates
        public static void AddExchangeRate(string currencyCode, decimal value)
        {
            if (value <= 0)
            {
                // - "Värdet måste vara över 0."
            }
            if (currencyCode.Length != 3)
            {
                // - "En landskod måste ha 3 tecken." 
            }
            _exchangeRates.Add(currencyCode, value);
        }
    }
}
