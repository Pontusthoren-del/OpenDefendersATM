using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Transaction
    {
        public int transactionID { get; set; }
        public List<Account> fromAccount { get; set; }
        public List<Account> toAccount { get; set; }

        public float amount { get; set; }
        public string currency { get; set; }
        public DateTime timestamp { get; set; }
        public string status { get; set; }

        public void Execute()
        {

        }
        public void ScheduleExecute()
        {

        }
    }
}
