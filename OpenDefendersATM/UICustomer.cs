using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UICustomer
    {
        public void ViewAccounts()
        {
            if (CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
                return;
            }
            PrintAccounts(CustomerAccounts);
            int selectedID = Backup.ReadInt("Ange KontoID du vill hantera: ");
            Account? selectedAccount = null;
            foreach (var acc in CustomerAccounts)
            {
                if (acc.GetAccountID() == selectedID)
                {
                    selectedAccount = acc;
                    break;
                }
            }
            if (selectedAccount == null)
            {
                Console.WriteLine("Kontot hittades inte.");
                return;
            }
            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ");
                Console.WriteLine("1. Sätt in pengar.");
                Console.WriteLine("2. Ta ut pengar.");
                Console.WriteLine("3. Döp om ett konto.");
                Console.WriteLine("4. Tillbaka.");
                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        UI.DepositInteraction(selectedAccount);
                        break;
                    case 2:
                        UI.WithdrawInteraction(selectedAccount);
                        break;
                    case 3:
                        RenameAccount(selectedAccount);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
            }
        }

    }
}
