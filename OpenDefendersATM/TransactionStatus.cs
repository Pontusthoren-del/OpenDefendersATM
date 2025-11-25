using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal struct TransactionStatus
    {
        internal static string Pending { get; set; } = "Pending";
        internal static string Declined { get; set; } = "Declined";
        internal static string Complete { get; set; } = "Complete";
    }
}
