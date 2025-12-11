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
        public void PrintLoanInfo(Customer c)
        {
            Console.WriteLine($"Lån-ID: {LoanID}");
            Console.WriteLine($"Lånebelopp: {Amount} SEK");
            Console.WriteLine($"Årlig ränta: {GetInterestRate(c, Amount)} %");
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
        public static decimal GetInterestRate(Customer c, decimal input)
        {
            decimal amount = 0;
            if (input < 9999)
            {
                amount = 5.0m;
            }
            if (input >= 10000 && input < 29999)
            {
                amount = 3.8m;
            }
            if (input >= 30000)
            {
                amount = 2.5m;
            }
            return amount;
        }
    }
}
