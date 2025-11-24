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

        public SavingsAccount(int accountID, string currency, float interestRate = 0.02f,string name="Nytt sparkonto") : base(accountID, currency,name)
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
        public override float Deposit()
        {
            float amount = base.Deposit();
            float interest = CalculateInterest(amount);

            Console.WriteLine($"Du satte in {amount} - {GetCurrency()}.");
            Console.WriteLine($"Med denna insättning kommer du tjäna {interest} {GetCurrency()} i ränta per år. ");
            return amount;
        }
    }
}
