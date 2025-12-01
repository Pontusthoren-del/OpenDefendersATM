using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UICustomer
    {
        public void ViewAccounts(Customer c)
        {
            if (c.CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
                return;
            }
            PrintAccounts(c.CustomerAccounts);
            int selectedID = Backup.ReadInt("Ange KontoID du vill hantera: ");
            Account? selectedAccount = null;
            foreach (var acc in c.CustomerAccounts)
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
        private void PrintAccounts(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("\nDu har inga öppna konton.\n");
                return;
            }
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("| Typ           | KontoID   | Namn                | Saldo      | Valuta |");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var acc in accounts)
            {
                string type = acc is SavingsAccount ? "Sparkonto" : "Vanligt konto";
                Console.WriteLine($"| {type,-14} | {acc.GetAccountID(),-9} | {acc.Name,-20} | {acc.GetBalance(),-10} | {acc.GetCurrency(),-6} |");
            }

            Console.WriteLine("---------------------------------------------------------------\n");

            Console.WriteLine("-------------------------------------------------\n");
        }
        public void RenameAccount(Account acc)
        {
            Console.WriteLine($"Nuvarande namn: {acc.Name}");
            Console.Write("Ange nytt namn: ");
            string newName = Console.ReadLine() ?? acc.Name;
            acc.Name = newName;
            Console.WriteLine($"Kontot har bytt namn till {newName}.");
        }

        public void OpenAccount(Customer c)
        {
            while (true)
            {

                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Öppna ett vanligt konto.");
                Console.WriteLine("2. Öppna ett sparkonto med 2% ränta. ");
                Console.WriteLine("3. Återgå.");

                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        c.OpenRegularAccount();
                        break;
                    case 2:
                        c.OpenSavingsAccount();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
