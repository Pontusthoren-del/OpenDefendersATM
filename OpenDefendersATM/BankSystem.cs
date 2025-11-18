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
        public Dictionary<string, User> Users { get; set; } = new();
        public static List<Account> _accounts { get; set; }
        public List<Transaction> Transactions { get; set; }
        public static Dictionary<string, decimal> ExchangeRates { get; } // Visual only dictionary
        public void ProcessScheduleTransaction()
        {

        }
        public void LogTransaction()
        {

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
