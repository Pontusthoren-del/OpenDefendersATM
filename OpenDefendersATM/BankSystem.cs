using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class BankSystem
    {
        public Dictionary<string, User> Users { get; set; } = new();
        public List<Account> Accounts { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }
        public void ProcessScheduleTransaction()
        {

        }
        public void LogTransaction()
        {

        }
    }
}
