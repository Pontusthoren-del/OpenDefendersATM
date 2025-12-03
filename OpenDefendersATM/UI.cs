using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            int loggedInTries = 0;
            while (loggedIn && loggedInTries < 3)
            {
                User? user = User.Login(BankSystem.Users);
                if (user != null)
                {
                    ShowMainMenu(user);
                    return;
                }
                else
                {
                    loggedInTries++;
                    Console.WriteLine($"Felaktigt försök: {loggedInTries} av 3.");
                    Console.ReadLine();
                    if (loggedInTries >= 3)
                    {
                        Console.WriteLine(new string('-', 30));
                        Console.WriteLine("För många försök. Ditt konto har låsts.");
                        loggedIn = false;
                    }

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
            Console.Clear();
            while (true)
            {

                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Överföring mellan egna konton.");
                Console.WriteLine("2. Överföring till annan kund. ");
                Console.WriteLine("3. Återgå.");
                int input = Backup.ReadInt("Ditt val: ");
                switch (input)
                {
                    case 1:
                        TransferInteraction(customer.CustomerAccounts);
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
            Console.WriteLine("=====|| Uttag ||=====\n");

            decimal withdrawl = Backup.ReadDecimal("Ange summa du vill ta ut:");

            // Create withdrawl-transaction
            account.NewWithdrawl(withdrawl);

        }
        public static void DepositInteraction(Account account)
        {
            Console.WriteLine("=====|| Insättning ||=====\n");

            decimal deposit = Backup.ReadDecimal("Ange summa du vill sätta in (max 50 000):");

            // Create deposit-transaction:
            account.NewDeposit(deposit);
        }
        public static decimal PrintTransactionInfo(decimal deposit, int accountID, string currency, decimal balance)
        {
            SuccessMessage();
            Console.WriteLine($"Till konto: {accountID}");
            Console.WriteLine($"{deposit} - {currency}");
            Console.WriteLine($"Nytt saldo: {balance} {currency}.");

            //Console.WriteLine($"Tidpunkt: {trans.Timestamp}");
            return deposit;
        }
        public static void TransferInteraction(List<Account> customerAccounts)
        {
            if (customerAccounts.Count < 2)
            {
                Console.WriteLine("Du måste ha minst två konton för att föra över mellan dina egna konton.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("Dina konton:");
            foreach (var acc in customerAccounts)
            {
                Console.WriteLine($"KontoID: {acc.GetAccountID()} | Saldo: {acc.GetBalance()} {acc.GetCurrency()}");
            }

            // Välj avsändarkonto
            int fromID = Backup.ReadInt("Ange KontoID du vill överföra FRÅN: ");
            Account? fromAccount = customerAccounts.FirstOrDefault(a => a.GetAccountID() == fromID);

            if (fromAccount == null)
            {
                Console.WriteLine("Avsändarkontot hittades inte.");
                Console.ReadKey();
                return;
            }

            // Välj mottagarkonto
            int toID = Backup.ReadInt("Ange KontoID du vill överföra TILL: ");
            Account? toAccount = customerAccounts.FirstOrDefault(a => a.GetAccountID() == toID);

            if (toAccount == null)
            {
                Console.WriteLine("Avsändarkontot hittades inte.");
                Console.ReadKey();
                return;
            }

            if (fromAccount == toAccount)
            {
                Console.WriteLine("Du kan inte göra en överföring till samma konto.");
                Console.ReadKey();
                return;
            }

            decimal amount = Backup.ReadDecimal("\nAnge summa du vill föra över:");
            if (amount > fromAccount.Balance)
            {
                Console.WriteLine("Du har inte tillräckligt på ditt konto.");
                Console.ReadKey();
                return;
            }
            if (amount < 1)
            {
                Console.WriteLine("Minsta belopp för överföring är 1 kr.");
                Console.ReadKey();
                return;
            }
            fromAccount.NewUserTransaction(amount, fromAccount, toAccount);

            SuccessMessage();
            Console.WriteLine($"{amount} kr");
            Console.WriteLine($"Från konto: {fromAccount.AccountID} - Nytt saldo: {fromAccount.Balance}");
            Console.WriteLine($"Till konto: {toAccount.AccountID} - Nytt saldo: {toAccount.Balance}");
            Console.ReadKey();
        }

        public static void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Transaction misslyckades.");
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

