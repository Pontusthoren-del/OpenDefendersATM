using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenDefendersATM
{
    internal class UI
    {
        public static void RunBankApp()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                User? user = Login(BankSystem.Users);
                if (user != null)
                {
                    bool continueApp = ShowMainMenu(user);
                    if (!continueApp)
                    {
                        loggedIn = false;
                    }
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }
        public static User? Login(List<User> users)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("_______                    ________     ________           _________                   \r\n__  __ \\______________________  __ \\_______  __/_________________  /___________________\r\n_  / / /__  __ \\  _ \\_  __ \\_  / / /  _ \\_  /_ _  _ \\_  __ \\  __  /_  _ \\_  ___/_  ___/\r\n/ /_/ /__  /_/ /  __/  / / /  /_/ //  __/  __/ /  __/  / / / /_/ / /  __/  /   _(__  ) \r\n\\____/ _  .___/\\___//_/ /_//_____/ \\___//_/    \\___//_/ /_/\\__,_/  \\___//_/    /____/  \r\n       /_/                                                                             \r\n                            ____________________  ___                                  \r\n                            ___    |__  __/__   |/  /                                  \r\n                            __  /| |_  /  __  /|_/ /                                   \r\n                            _  ___ |  /   _  /  / /                                    \r\n                            /_/  |_/_/    /_/  /_/                                     ");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t" + new string('*', 60));
            Console.WriteLine("\t         *****VÄLKOMNA TILL OPEN DEFENDERS ATM*****");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("\t                          Användarnamn: ");
            string? name = Console.ReadLine();
            Console.Write("\t                               PIN: ");
            //A do/while for out pincode. Will show * instead of the numbers you put in.. 
            string? pinInput = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && pinInput.Length > 0)
                {
                    pinInput = pinInput.Substring(0, pinInput.Length - 1);
                    Console.WriteLine("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pinInput += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            if (!int.TryParse(pinInput, out int pin))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("\n                          \tFelaktigt format på PIN!");
                Console.ResetColor();
                return null;
            }
            User? user = users.FirstOrDefault(u => u.Name == name);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("\n                          \tAnvändaren finns inte.");
                Console.ResetColor();
                return null;
            }
            else if (user.CheckPin(pinInput))
            {
                Console.WriteLine($"Välkommen {user.Name}");
                return user;
            }
            return null;
        }
        public static void LogOut(User user)
        {
            Console.WriteLine($"\n{user.Name} har loggats ut.");
            Console.ReadKey();
        }

        public static bool ShowMainMenu(User loggedinUser)
        {
            {
                if (loggedinUser.Role == "Admin")
                {
                    return UIAdmin.AdminMenu(loggedinUser);
                }
                else
                {
                    return UICustomer.CustomerMenu(loggedinUser);
                }
            }
        }
        public static void TransferMenu(Customer customer)
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine($"\t[KUND] Inloggad som " + customer.Name);
                Console.ResetColor();
                Console.WriteLine();
                UICustomer.PrintAccounts(customer);
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Överföring mellan egna konton.");
                Console.WriteLine("2. Överföring till annan kund. ");
                Console.WriteLine("3. Återgå.");
                Console.WriteLine(new string('*', 30));
                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        TransferInteraction(customer);
                        break;
                    case 2:
                        UICustomer.TransferToOtherCustomers(customer);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.Clear();
            }
        }
        public static void WithdrawInteraction(Account account)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("          \t=====|| Uttag ||=====\n");
            Console.ResetColor();

            decimal withdrawl = Backup.ReadDecimal("Ange summa du vill ta ut:");

            // Create withdrawl-transaction
            account.NewWithdrawl(withdrawl);

        }
        public static void DepositInteraction(Account account)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("          \t=====|| Insättning ||=====\n");
            Console.ResetColor();
            decimal deposit = Backup.ReadDecimal("Ange summa du vill sätta in (max 50 000): ");

            // Create deposit-transaction:
            account.NewDeposit(deposit);
        }
        public static decimal PrintTransactionInfo(decimal deposit, int accountID, string currency, decimal balance)
        {
            SuccessMessage();
            Console.WriteLine($"Till konto: {accountID}");
            Console.WriteLine($"{deposit:F2} - {currency}");
            Console.WriteLine($"Nytt saldo: {balance:F2} {currency}.");
            Console.ReadKey();
            //Console.WriteLine($"Tidpunkt: {trans.Timestamp}");
            return deposit;
        }
        public static void TransferInteraction(Customer c)
        {
            if (c.CustomerAccounts.Count < 2)
            {
                Console.WriteLine("Du måste ha minst två konton för att göra en överföring.");
                Console.ReadKey();
                return;
            }
            int fromIndex = Backup.ReadInt("Välj kontonummer att föra över FRÅN: ") - 1;
            if (fromIndex < 0 || fromIndex >= c.CustomerAccounts.Count)
            {
                Console.WriteLine("Felaktigt val.");
                Console.ReadKey();
                return;
            }
            int toIndex = Backup.ReadInt("Välj kontonummer att föra över TILL: ") - 1;
            if (toIndex < 0 || toIndex >= c.CustomerAccounts.Count)
            {
                Console.WriteLine("Felaktigt val.");
                Console.ReadKey();
                return;
            }
            var fromAccount = c.CustomerAccounts[fromIndex];
            var toAccount = c.CustomerAccounts[toIndex];

            if (fromAccount == toAccount)
            {
                Console.WriteLine("Du kan inte överföra till samma konto.");
                Console.ReadKey();
                return;
            }

            decimal amount = Backup.ReadDecimal("Ange summa du vill föra över: ");
            if (amount < 1)
            {
                Console.WriteLine("Minsta belopp är 1.");
                Console.ReadKey();
                return;
            }

            if (amount > fromAccount.Balance)
            {
                Console.WriteLine("Du har inte tillräckligt på kontot.");
                Console.ReadKey();
                return;
            }
            // VÄXELKURS
            string fromCurrency = fromAccount.Currency;
            string toCurrency = toAccount.Currency;
            decimal finalAmount = amount;
            // Konvertera endast om valutorna skiljer sig
            if (fromCurrency != toCurrency)
            {
                finalAmount = BankSystem.ExchangeConverter(fromCurrency, amount, toCurrency);
            }
            // SJÄLVA ÖVERFÖRINGEN – BYTER UT DITT GAMLA
            fromAccount.Balance -= amount;       // Avdrag i FRÅN-kontots valuta
            toAccount.Balance += finalAmount;    // Insättning i TILL-kontots valuta
            // UTSKRIFT
            SuccessMessage();
            Console.WriteLine($"{amount:F2} {fromCurrency} överfört från konto {fromAccount.AccountID}.");
            Console.WriteLine($"Mottagaren fick {finalAmount:F2} {toCurrency}.");
            Console.WriteLine($"Nytt saldo på {fromAccount.AccountID}: {fromAccount.Balance:F2} {fromCurrency}.");
            Console.WriteLine($"Nytt saldo på {toAccount.AccountID}: {toAccount.Balance:F2} {toCurrency}.");
            //fromAccount.NewUserTransaction(amount, fromAccount, toAccount);
            Transaction trans = new Transaction(amount, fromAccount.AccountID, toAccount.AccountID, fromAccount.Currency);
            trans.TransactionComplete();
            fromAccount.LogTransaction(trans);
            toAccount.LogTransaction(trans);
            Console.WriteLine("\nTryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            Console.Clear();
        }
        public static void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nTransaction misslyckades.");
            Console.ResetColor();
            Console.ReadKey();
        }
        public static void SuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTransaction lyckades.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}

