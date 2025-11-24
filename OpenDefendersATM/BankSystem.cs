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
        public static List<User> _users { get; set; } = new()
        {
         new Admin("admin", "Admin", 1234),
         new Customer("kalle", "Customer", 1111)
        };
        public static List<Account> _accounts { get; set; }
        //private List<Transaction> _transactions { get; set; }  // Do we need this one?
        public static Dictionary<string, decimal> ExchangeRates { get; } // Visual dictionary

        public void ProcessScheduleTransaction()
        {

        }
        
        //A list with AllAccounts, but we just sorting out the customers.
        public static List<Account> AllAccounts()
        {
            return _users
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
