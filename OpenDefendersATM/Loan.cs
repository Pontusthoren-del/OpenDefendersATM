using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Loan
    {
        public decimal MaxLoan { get; set; }
        public int LoanID { get; set; }
        public static int LoanIDCounter = 1;
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int customerID { get; set; }
        public DateTime Timestamp { get; private set; }

        public Loan(decimal amount, decimal interestRate )
        {
            LoanID = LoanIDCounter;
            LoanIDCounter++;
            Amount = amount;
            InterestRate = interestRate;
            Timestamp = DateTime.Now;
        }
        public void GetLoanID()
        {
            
        }
        public void PrintLoanInfo(Customer c)
        {
            Console.WriteLine($"Lån-ID: {LoanID}");
            Console.WriteLine($"Lånebelopp: {Amount}");
            Console.WriteLine($"Att betala tillbaka: {Amount * GetInterestRate(c)} SEK");
            Console.WriteLine($"Tidpunkt för lån: {Timestamp}\n");
        }
        public static decimal GetMaxLoanAmount(Customer c)
        {
            return BankSystem.AccountTotalBalanceSEK(c) * 5;
        }
        public decimal CalculatePayment(Customer c)
        {
            if (GetMaxLoanAmount(c) < 9999)
            {
                InterestRate = 1.5m;
            }
            if (GetMaxLoanAmount(c) >= 10000 && GetMaxLoanAmount(c) < 29999)
            {
                InterestRate = 1.3m;
            }
            if (GetMaxLoanAmount(c) >= 30000)
            {
                InterestRate = 1.15m;
            }
            return InterestRate * GetMaxLoanAmount(c);
        }
        public static decimal GetInterestRate(Customer c)
        {
            decimal amount = 0;
            if (GetMaxLoanAmount(c) < 9999)
            {
                amount = 1.5m;
            }
            if (GetMaxLoanAmount(c) >= 10000 && GetMaxLoanAmount(c) < 29999)
            {
                amount = 1.3m;
            }
            if (GetMaxLoanAmount(c) >= 30000)
            {
                amount = 1.15m;
            }
            return amount;
        }


        //public void CheckLoanLimit(Customer customer)
        //{
        //    Console.WriteLine("Ange det lönebelopp du vill låna:");
        //    decimal loanLimit = customer.TotalBalance();
        //    MaxLoan = loanLimit * 5;

        //    decimal userInput;
        //    while (!decimal.TryParse(Console.ReadLine(), out userInput) || userInput <= 0 || userInput > MaxLoan)
        //    {
        //        Console.WriteLine("Du kan inte låna ett negativt belopp. Vänligen ange din önskad lånesumma." +
        //            "Du får låna max 5 gånger ditt totala saldo");
        //    }

        //}
    }
}
