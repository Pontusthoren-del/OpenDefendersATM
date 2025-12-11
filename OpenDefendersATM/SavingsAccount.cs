using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class SavingsAccount : Account
    {
        private float interestRate { get; set; }
        public SavingsAccount(int accountID, decimal balance, string currency, float interestRate = 0.02f, string name = "Nytt sparkonto")
            : base(accountID, balance, currency, name)
        {
            this.interestRate = interestRate;
        }
        public float GetInterestRate()
        {
            return interestRate;
        }
        public float CalculateInterest(float amount)
        {
            return amount * interestRate;
        }
    }
}
