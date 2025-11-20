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
        public float Amount { get; set; }
        public float InterestRate { get; set; }
        public int customerID { get; set; }

        public void CalculatePayment()
        {

        }

        public void CheckLoanLimit()
        {
            // customerID, account, amount, InterestRate, 
            // if sats + uträkning
            Console.WriteLine("--LÅN--");
            Console.WriteLine("Ange summna du vill låna: ");

            float balance;

            decimal loan;
            while(!decimal.TryParse(Console.ReadLine(), out loan)

           
        }
    }
}
