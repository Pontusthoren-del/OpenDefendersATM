using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Loan
    {
        public float MaxLoan { get; set; }
        public int LoanID { get; set; }
        public float Amount { get; set; }
        public float InterestRate { get; set; }
        public int customerID { get; set; }

        public void CalculatePayment()
        {

        }


        public void CheckLoanLimit(Customer customer)
        {
            Console.WriteLine("Ange det lönebelopp du vill låna:");
            float loanLimit = customer.TotalBalance();
            MaxLoan = loanLimit * 5;

            float userInput;
            while (!float.TryParse(Console.ReadLine(), out userInput) || userInput <= 0 || userInput > MaxLoan)
            {
                Console.WriteLine("Du kan inte låna ett negativt belopp. Vänligen ange din önskad lånesumma." +
                    "Du får låna max 5 gånger ditt totala saldo");
            }

        }
    }
}
}
}
