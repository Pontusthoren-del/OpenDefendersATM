using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class BankSystem
    {
        // Private dictionary that holds the banks exchange rates:
        private static Dictionary<string, decimal> _exchangeRates = new Dictionary<string, decimal> 
        {
            { "SEK", 1m }, // base currency
            { "USD", 11m },
            { "EUR", 11.50m }
        };

        // Method that shows the users Total Balance within all accounts, in SEK
        public static decimal AccountTotalBalanceSEK(Customer c)
        {
            decimal totalAmountInSEK = 0;
            foreach (var userAccount in c.CustomerAccounts)
            {
                totalAmountInSEK += ExchangeConverter(userAccount.Currency, userAccount.Balance, "SEK");
            }
            return totalAmountInSEK;
        }

        // Method that works with Converting the Exchange Rate: using the currency the user starts off with, applying the amount, and choosing which currency the user wishes to convert to
        public static decimal ExchangeConverter(string fromCurrency, decimal Amount, string toCurrency)
        {
            decimal amountInSEK = Amount * _exchangeRates[fromCurrency];
            return amountInSEK / _exchangeRates[toCurrency];
        }

        public static void PrintAllUsers() //metod som låter admin se alla användare
        {
            Console.WriteLine("Visar alla användare & antal konton:");
            Console.WriteLine();
            int customerCount = 0;
            foreach (var user in Users)
            {
                if (user is Customer customer)
                {
                    customerCount++;

                    Console.WriteLine($"{customerCount}. {customer.Name}");
                    Console.WriteLine($"Antal konton: {customer.CustomerAccounts.Count}");
                    Console.WriteLine();
                    

                }
            }

            if (customerCount == 0)
            {
                Console.WriteLine("Inga kunder hittades.");
            }
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
                    new Account(33331, 1500, "SEK", "Privat Konto"),
                    new Account(33332, 300, "USD", "Tradingkonto"),
                    new Account(33333, 2000, "EUR", "Buffertkonto")
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
                    new Account(55551, 400, "SEK", "Privat Konto"),
                    new Account(55552, 2500, "USD", "Semesterkonto"),
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
}
