using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UI
    {
        public static void RunBankApp()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                User? user = User.Login(BankSystem.Users);
                if (user != null)
                {
                    ShowMainMenu(user);
                    loggedIn = false;
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }
        //Console.WriteLine("···················································································\r\n: _______  _______  _______  _                                                    :\r\n:(  ___  )(  ____ )(  ____ \\( (    /|                                             :\r\n:| (   ) || (    )|| (    \\/|  \\  ( |                                             :\r\n:| |   | || (____)|| (__    |   \\ | |                                             :\r\n:| |   | ||  _____)|  __)   | (\\ \\) |                                             :\r\n:| |   | || (      | (      | | \\   |                                             :\r\n:| (___) || )      | (____/\\| )  \\  |                                             :\r\n:(_______)|/       (_______/|/    )_)                                             :\r\n:                                                                                 :\r\n: ______   _______  _______  _______  _        ______   _______  _______  _______ :\r\n:(  __  \\ (  ____ \\(  ____ \\(  ____ \\( (    /|(  __  \\ (  ____ \\(  ____ )(  ____ \\:\r\n:| (  \\  )| (    \\/| (    \\/| (    \\/|  \\  ( || (  \\  )| (    \\/| (    )|| (    \\/:\r\n:| |   ) || (__    | (__    | (__    |   \\ | || |   ) || (__    | (____)|| (_____ :\r\n:| |   | ||  __)   |  __)   |  __)   | (\\ \\) || |   | ||  __)   |     __)(_____  ):\r\n:| |   ) || (      | (      | (      | | \\   || |   ) || (      | (\\ (         ) |:\r\n:| (__/  )| (____/\\| )      | (____/\\| )  \\  || (__/  )| (____/\\| ) \\ \\__/\\____) |:\r\n:(______/ (_______/|/       (_______/|/    )_)(______/ (_______/|/   \\__/\\_______):\r\n:                                                                                 :\r\n: _______ _________ _______                                                       :\r\n:(  ___  )\\__   __/(       )                                                      :\r\n:| (   ) |   ) (   | () () |                                                      :\r\n:| (___) |   | |   | || || |                                                      :\r\n:|  ___  |   | |   | |(_)| |                                                      :\r\n:| (   ) |   | |   | |   | |                                                      :\r\n:| )   ( |   | |   | )   ( |                                                      :\r\n:|/     \\|   )_(   |/     \\|                                                      :\r\n···················································································\n");
        public static void ShowMainMenu(User loggedinUser)
        {
            bool runMenu = true;

            while (runMenu)
            {
                if (loggedinUser.Role == "Admin")
                {
                    UIAdmin.AdminMenu(loggedinUser);
                    break;
                }
                else
                {
                    UICustomer.CustomerMenu(loggedinUser);
                    break;
                }
            }
        }
        public static void TransferMenu(Customer customer)
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
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
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("=====|| Uttag ||=====\n");

            decimal withdrawl = Backup.ReadDecimal("Ange summa du vill ta ut:");

            // Create withdrawl-transaction
            account.NewWithdrawl(withdrawl);

        }
        public static void DepositInteraction(Account account)
        {
            Console.WriteLine("=====|| Insättning ||=====\n");
            Console.WriteLine(new string('*', 30));
            decimal deposit = Backup.ReadDecimal("Ange summa du vill sätta in (max 50 000):");

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
        }
        public static void SuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTransaction lyckades.");
            Console.ResetColor();
        }
    }
}

