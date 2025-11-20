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
        public static List<User> _users { get; set; } = new();
        public static List<Account> _accounts { get; set; }
        private List<Transaction> _transactions { get; set; }
        public static Dictionary<string, decimal> ExchangeRates { get; } // Visual dictionary

        private static List<User> users = new List<User>()
        {
        new User("admin", "Admin", 1234),
        new User("kalle", "Customer", 1111)
        };
        public BankSystem(List<User> users)
        {
            _users = users;
        }
        public void ProcessScheduleTransaction()
        {

        }
        public void LogTransaction()
        {

        }
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
