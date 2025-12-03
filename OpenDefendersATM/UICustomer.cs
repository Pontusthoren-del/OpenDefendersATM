using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UICustomer
    {
        public static void ViewAccounts(Customer c)
        {
            if (c.CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
                Console.ReadLine();
                return;
            }
            int selectedIndex = Backup.ReadInt("Välj konto att hantera (nummer): ") - 1;
            if (selectedIndex < 0 || selectedIndex >= c.CustomerAccounts.Count)
            {
                Console.WriteLine("Felaktigt val.");
                Console.ReadLine();
                return;
            }
            Account selectedAccount = c.CustomerAccounts[selectedIndex];
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
                Console.WriteLine();
                PrintAccounts(c);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Sätt in pengar");
                Console.WriteLine("2. Ta ut pengar");
                Console.WriteLine("3. Tillbaka");
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
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
            }  
        }
        public static void PrintAccounts(Customer c)    
        {
            if (c.CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
                return;
            }
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("| Nr | Typ            | KontoID   | Namn                 | Saldo      | Valuta |");
            Console.WriteLine("----------------------------------------------------------------------------------");
            for (int i = 0; i < c.CustomerAccounts.Count; i++)
            {
                var acc = c.CustomerAccounts[i];
                string type = acc is SavingsAccount ? "Sparkonto" : "Vanligt konto";
                Console.WriteLine($"| {i + 1,-2} | {type,-14} | {acc.GetAccountID(),-9} | {acc.Name,-20} | {acc.GetBalance(),-10} | {acc.GetCurrency(),-6} |");
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
        }
        public static void RenameAccount(Account acc)
        {
            Console.WriteLine($"Nuvarande namn: {acc.Name}");
            Console.Write("Ange nytt namn: ");
            string newName = Console.ReadLine() ?? acc.Name;
            acc.Name = newName;
            Console.WriteLine($"Kontot har bytt namn till {newName}.");
        }
        public static void OpenAccount(Customer c)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
                Console.WriteLine();
                PrintAccounts(c);
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
        public static void HandleAccounts(Customer c)
        {
            Console.Clear();
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.WriteLine();
            PrintAccounts(c);
            Console.WriteLine("Välj ett alternativ.");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("1. Döp om befintligt konto");
            Console.WriteLine("2. Öppna nytt konto.");
            Console.WriteLine("3. Återgå.");

            int input = Backup.ReadInt("Ditt val: ");
            switch (input)
            {
                case 1:
                    Console.WriteLine("Välj konto att döpa om:");
                    for (int i = 0; i < c.CustomerAccounts.Count; i++)
                        Console.WriteLine($"{i + 1}. {c.CustomerAccounts[i].Name}");
                    int val = Backup.ReadInt("Ditt val: ") - 1;
                    if (val < 0 || val >= c.CustomerAccounts.Count)
                    {
                        Console.WriteLine("Felaktigt val.");
                        break;
                    }
                    RenameAccount(c.CustomerAccounts[val]);
                    break;
                case 2:
                    OpenAccount(c);
                    break;
                case 3:
                    return;
                default:
                    break;
            }
            Console.Clear();
        }
        public static void CustomerMenu(User user)
        {
            bool running = true;
            Customer? customer = user as Customer;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"\t[KUND] Inloggad som " + user.Name);
                Console.WriteLine();
                PrintAccounts(customer);
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Sätt in / Ta ut.");
                Console.WriteLine("2. Hantera konto / Öppna nytt.");
                Console.WriteLine("3. Överföring.");
                Console.WriteLine("4. Lån.");
                Console.WriteLine("5. Transaktionslog.");
                Console.WriteLine("6. Logga ut.");
                Console.WriteLine("7. Avsluta applikationen.");
                Console.WriteLine(new string('*', 30));

                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        ViewAccounts(customer);
                        break;
                    case 2:
                        HandleAccounts(customer);
                        break;
                    case 3:
                        UI.TransferMenu(customer);
                        break;
                    case 4:
                        customer?.RequestLoan();
                        break;
                    case 5:
                        if (customer?.CustomerAccounts.Count > 0)
                        {

                            customer?.CustomerAccounts[0].ViewAllTransactions();
                        }
                        else
                        {
                            Console.WriteLine("Du har inga transaktioner.");
                        }
                        break;
                    case 6:
                        UI.RunBankApp();
                        break;
                    case 7:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
            }
        }

        public static void TransferToOtherCustomers(Customer c)
        {
            Account receiverAccount = null;
            while (receiverAccount == null)
            {
                int targetAccountID = Backup.ReadInt("\nAnge KontoID att föra över till: ");
                foreach (User user in BankSystem.Users)
                {
                    if (user is Customer cus)
                    {
                        receiverAccount = cus.CustomerAccounts.FirstOrDefault(a => a.GetAccountID() == targetAccountID);
                        if (receiverAccount != null) break;
                    }
                }
                if (receiverAccount == null)
                {
                    Console.WriteLine("Kontot hittades inte. Försök igen.");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("\nDina konton:");
            for (int i = 0; i < c.CustomerAccounts.Count; i++)
            {
                var acc = c.CustomerAccounts[i];
                Console.WriteLine($"{i + 1}. {acc.Name} | Saldo: {acc.GetBalance()} {acc.GetCurrency()} | KontoID: {acc.GetAccountID()}");
            }
            int fromChoice = Backup.ReadInt("\nVälj konto att föra över från (nummer): ") - 1;
            if (fromChoice < 0 || fromChoice >= c.CustomerAccounts.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFelaktigt val.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Account senderAccount = c.CustomerAccounts[fromChoice];
            decimal amount = Backup.ReadDecimal("Ange summa att föra över: ");
            if (amount <= 0 || senderAccount.GetBalance() < amount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFelaktigt belopp.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            //senderAccount.NewWithdrawl(amount);
            //receiverAccount.NewDeposit(amount);
            senderAccount.NewUserTransaction(amount, senderAccount, receiverAccount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nÖverföring av {amount} {senderAccount.GetCurrency()} till konto {receiverAccount.GetAccountID()} genomförd.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
