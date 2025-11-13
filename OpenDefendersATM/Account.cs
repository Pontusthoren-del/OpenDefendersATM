using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Account
    {
        // List that logs user transactions:
        private List<Transaction> transactionLog = new List<Transaction>();

        private int AccountID { get; set; }
        private float Balance { get; set; }
        private string Currency { get; set; } = "Unknown";
        

        public void Deposit()
        {

        }

        public void Withdraw()
        {

        }

        public void AddTransaction()
        {

        }
    }
}
