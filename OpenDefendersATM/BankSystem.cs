using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class BankSystem
    {
        public List<User> users { get; set; }
        public List<Account> accounts { get; set; }
        public List<Transaction> transactions { get; set; }
        public List<ExchangeRate> exchangeRates { get; set; }
        public void ProcessScheduleTransaction()
        {

        }
        public void LogTransaction()
        {

        }
    }
}
