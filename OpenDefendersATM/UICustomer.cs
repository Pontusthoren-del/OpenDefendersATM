using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            int selectedIndex = Backup.ReadInt("Välj kontonummer att hantera: ") - 1;
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
                Console.ResetColor();
                Console.WriteLine();
                PrintAccounts(c);
                Console.WriteLine();
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Sätt in pengar.");
                Console.WriteLine("2. Ta ut pengar.");
                Console.WriteLine("3. Tillbaka.");
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
                Console.WriteLine($"| {i + 1,-2} | {type,-14} | {acc.GetAccountID(),-9} | {acc.Name,-20} | {acc.GetBalance(),-10:F2} | {acc.GetCurrency(),-6} |");
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
                Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();
            PrintAccounts(c);
            Console.WriteLine("Välj ett alternativ.");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("1. Döp om befintligt konto.");
            Console.WriteLine("2. Öppna nytt konto.");
            Console.WriteLine("3. Återgå.");

            int input = Backup.ReadInt("Ditt val: ");
            switch (input)
            {
                case 1:
                    Console.WriteLine("Välj konto att döpa om: ");
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t[KUND] Inloggad som " + customer.Name);
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("7. Avsluta applikationen.");
                Console.ResetColor();
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
                        LoanInteraction(customer);
                        //customer?.RequestLoan();
                        break;
                    case 5:
                        if (customer?.CustomerAccounts.Count > 0)
                        {
                            ChooseTransactionLogAccount(customer);
                        }
                        else
                        {
                            Console.WriteLine("Du har inga transaktioner.");
                        }
                        break;
                    case 6:
                        user.LogOut(user);
                        UI.RunBankApp();
                        break;
                    case 7:
                        Console.WriteLine("\nProgrammet avslutas...\n");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
            }
        }
        public static void ChooseTransactionLogAccount(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();
            PrintAccounts(c);
            Console.WriteLine();
            
            if (c.CustomerAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga öppna konton.");
                Console.ReadLine();
                return;
            }
            int selectedIndex = Backup.ReadInt("Välj kontonummer du vill se kontohistorik för: ") - 1;
            if (selectedIndex < 0 || selectedIndex >= c.CustomerAccounts.Count)
            {
                Console.WriteLine("Felaktigt val.");
                Console.ReadLine();
                return;
            }
            Account selectedAccount = c.CustomerAccounts[selectedIndex];

            c.CustomerAccounts[selectedIndex].ViewAllTransactions(c.CustomerAccounts[selectedIndex]);
        }

        public static void TransferToOtherCustomers(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();
            PrintAccounts(c);
            Console.WriteLine();
            Account receiverAccount = null;
            while (receiverAccount == null)
            {
                int targetAccountID = Backup.ReadInt("\nAnge KontoID att föra över till: \nTryck 0 för att gå tillbaka...");
                if (targetAccountID == 0)
                {
                    return;
                }
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
            
            int fromChoice = Backup.ReadInt("\nVälj kontonummer att föra över från: ") - 1;
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
            // VÄXELKURS
            string fromCurrency = senderAccount.Currency;
            string toCurrency = receiverAccount.Currency;
            decimal finalAmount = amount;
            // Konvertera endast om valutorna skiljer sig
            if (fromCurrency != toCurrency)
            {
                finalAmount = BankSystem.ExchangeConverter(fromCurrency, amount, toCurrency);
            }
            senderAccount.NewUserTransaction(amount, senderAccount, receiverAccount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nÖverföring av {amount} {senderAccount.GetCurrency()} till konto {receiverAccount.GetAccountID()} genomförd.");
            Console.ResetColor();
            Console.WriteLine("\nTryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
        }

        public static void LoanInteraction(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Lån");
            Console.WriteLine("mina lån, ansök om lån, visa mitt totala saldo ");
            Console.WriteLine("1. Mina Lån");
            Console.WriteLine("2. Ansök om Lån");
            Console.WriteLine("3. Visa mitt Totala Saldo");

            int input = Backup.ReadInt("Ditt val: ");
            switch (input)
            {
                case 1:
                    // Mina Lån(customer);
                    break;
                case 2:
                    LoanApplication(c);
                    break;
                case 3:
                    ShowTotalBalanceInSEK(c);
                    break;
                default:
                    break;


            }

        }

        // Method to show user's total balance in SEK. Keeping principle "you may borrow a total balance of 5 times your own SALDO
        // Method done by Bella (bellas dator fick flip i VS med GitHub och vi orkade inte ta reda på hur man fixar detta, så vi frågar Petter om detta på Måndag
        // istället
        public static void ShowTotalBalanceInSEK(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine($"Ditt totala saldo är {BankSystem.AccountTotalBalanceSEK(c)} kr.");
            Console.WriteLine($"ditt maximala lånebelopp är {BankSystem.AccountTotalBalanceSEK(c) * 5} kr.");
            Console.ReadKey();
        }

        // Method to Apply for a Loan
        // Deciding as we code: void or none-void
        public static void LoanApplication(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + c.Name);
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine($"Ditt totala saldo är {BankSystem.AccountTotalBalanceSEK(c)} kr.");

            decimal userInput = Backup.ReadDecimal("\nDu kan låna max 5 gånger ditt saldobelopp och minst 1000 kr. \n\nAnge summa vill du låna: ");
            if (userInput > BankSystem.AccountTotalBalanceSEK(c) * 5 || userInput < 1000)
            {
                Console.WriteLine("Lånebeloppet får ej vara mer än 5 gånger ditt totala saldo eller under 1000 kr.");
                Console.ReadKey();
                return;
            }
        }
    }
}
