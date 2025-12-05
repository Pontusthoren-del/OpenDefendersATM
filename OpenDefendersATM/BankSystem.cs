using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class BankSystem
    {
        // Private dictionary
        private static Dictionary<string, decimal> _exchangeRates = new Dictionary<string, decimal> //här skapar vi en ny dictionary och den heter ExchangeRates
        {
            { "SEK", 1m }, // base currency
            { "USD", 11m },
            { "EUR", 11.50m }
        };

        public static decimal AccountTotalBalanceSEK(Customer c)
        {
            decimal totalAmountInSEK = 0;
            foreach (var userAccount in c.CustomerAccounts)
            {
                totalAmountInSEK = ExchangeConverter(userAccount.Currency, userAccount.Balance, "SEK");
            }
            return totalAmountInSEK;
        }
        public static decimal ExchangeConverter(string fromCurrency, decimal Amount, string toCurrency)
        {
            decimal amountInSEK = Amount * _exchangeRates[fromCurrency];
            return amountInSEK / _exchangeRates[toCurrency];
        }

        public static List<User> Users { get; set; } = new()
        {
            new Admin("Petter", "Admin", 1234),

            new Customer("Pontus", "Customer", 1111, 1000)
            {
                CustomerAccounts = new List<Account>
                {
                    new Account(11111, 1000, "SEK", "Privat Konto"),
                    new SavingsAccount(11112, 500, "USD", 0.02f, "Savings"),
                    new Account(11113, 250, "EUR", "Resekonto")
                }
            },

            new Customer("Bella", "Customer", 2222, 1000)
            {
                CustomerAccounts = new List<Account>
                {
                    new Account(22221, 800, "SEK", "Privat Konto"),
                    new SavingsAccount(22222, 1200, "SEK", 0.02f, "Sparkonto")
                }
            },

            new Customer("Robin", "Customer", 3333, 1000)
            {
                CustomerAccounts = new List<Account>
                {
                    new Account(33331, 1500, "EUR", "Privat Konto"),
                    new Account(33332, 300, "USD", "Tradingkonto"),
                    new Account(33333, 2000, "SEK", "Buffertkonto")
                }
            },

            new Customer("Julia", "Customer", 4444, 1000)
            {
                CustomerAccounts = new List<Account>
                {
                    new Account(44441, 900, "SEK", "Privat Konto"),
                    new SavingsAccount(44442, 700, "EUR", 0.02f, "Sparkonto")
                }
            },

            new Customer("Kalle", "Customer", 5555, 1000)
            {
                CustomerAccounts = new List<Account>
                {
                    new Account(55551, 400, "USD", "Privat Konto"),
                    new Account(55552, 2500, "SEK", "Semesterkonto"),
                    new SavingsAccount(55553, 1100, "EUR", 0.02f, "Utlandskonto"),
                    new Account(55554, 50, "SEK", "Spelkonto")
                }
            }
        };
        public static List<User> LockedOutUsers { get; set; } = new();

        public static List<Account> Accounts { get; set; } = new List<Account>();

        //private List<Transaction> _transactions { get; set; }  // Do we need this one?

        public static Dictionary<string, decimal> ExchangeRates { get; } // Visual dictionary

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

    //private static readonly object _exchangeRatesLock = new();
    //ABOVE: private dictionary(back store) and lock for thread-safety

    //BELOW: public read-ony view of the exchange rates
    //public static IReadOnlyDictionary<string, decimal> ExchangeRates
    //{
    //    get
    //    {
    //        lock (_exchangeRatesLock)
    //        {
    //            //return a snapshot to avoid exposing internal mutable dictionary
    //            return new ReadOnlyDictionary<string, decimal>(new Dictionary<string, decimal>(_exchangeRates, StringComparer.OrdinalIgnoreCase));
    //        }
    //    }
    //}


    //public class MyExchangeRateProvider : IExchangeRateProvider // ha i baktänka att vi kanskse kan lägga in sen som en 'real-time' xchange rate thing typ.
    //{
    //    decimal toCurrency { get; set; }
    //    decimal fromCurrency { get; set; }

    //    GetExchangeRates( decimal fromCurrency, decimal toCurrency)
    //    {
    //        Console.WriteLine();
    //    }
    //    return something
    //}


    //Seeding default rates(relative to base currency, e.g.SEK = 1)// hämtad från google/gpt
    //static BankSystem()
    //{
    //    _exchangeRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
    //    {
    //        {"SEK", 1m }, // base currency
    //        {"USD", 0.09m },
    //        {"EUR", 0,087m }
    //    };
    //}

}
