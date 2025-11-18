using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Transaction
    {
        
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int TransactionID { get; set; }

        public int TransactionIDCounter = 1;
        public float Amount { get; set; }
        public string Currency { get; set; }

        public DateTime Timestamp { get; set; }   
        public string Status { get; set; }

        public Transaction(float amount, int fromAccount, int toAccount, string currency)
        {
            TransactionID = TransactionIDCounter;
            TransactionIDCounter++;
            Amount = amount;
            Currency = currency;
            Status = "Pending"; // pending, complete, declined
            FromAccount = fromAccount;
            ToAccount = toAccount;
        }

        public void GetStatus()
        {
            Console.WriteLine($"Status: {Status}");
        }

        public void Execute()
        {

        }
        public void ScheduleExecute()
        {

        }
    }
}
