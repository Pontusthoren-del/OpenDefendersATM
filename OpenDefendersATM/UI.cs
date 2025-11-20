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
                User? user = User.Login(BankSystem._users);
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
                        //Customer.LockedOut();
                        loggedIn = false;

                    }

                }
            }

        }
        static void ShowMainMenu(User loggedinUser)
        {
            bool runMenu = true;

            while (runMenu)
            {
                Console.Clear();
                Console.WriteLine("···················································································\r\n: _______  _______  _______  _                                                    :\r\n:(  ___  )(  ____ )(  ____ \\( (    /|                                             :\r\n:| (   ) || (    )|| (    \\/|  \\  ( |                                             :\r\n:| |   | || (____)|| (__    |   \\ | |                                             :\r\n:| |   | ||  _____)|  __)   | (\\ \\) |                                             :\r\n:| |   | || (      | (      | | \\   |                                             :\r\n:| (___) || )      | (____/\\| )  \\  |                                             :\r\n:(_______)|/       (_______/|/    )_)                                             :\r\n:                                                                                 :\r\n: ______   _______  _______  _______  _        ______   _______  _______  _______ :\r\n:(  __  \\ (  ____ \\(  ____ \\(  ____ \\( (    /|(  __  \\ (  ____ \\(  ____ )(  ____ \\:\r\n:| (  \\  )| (    \\/| (    \\/| (    \\/|  \\  ( || (  \\  )| (    \\/| (    )|| (    \\/:\r\n:| |   ) || (__    | (__    | (__    |   \\ | || |   ) || (__    | (____)|| (_____ :\r\n:| |   | ||  __)   |  __)   |  __)   | (\\ \\) || |   | ||  __)   |     __)(_____  ):\r\n:| |   ) || (      | (      | (      | | \\   || |   ) || (      | (\\ (         ) |:\r\n:| (__/  )| (____/\\| )      | (____/\\| )  \\  || (__/  )| (____/\\| ) \\ \\__/\\____) |:\r\n:(______/ (_______/|/       (_______/|/    )_)(______/ (_______/|/   \\__/\\_______):\r\n:                                                                                 :\r\n: _______ _________ _______                                                       :\r\n:(  ___  )\\__   __/(       )                                                      :\r\n:| (   ) |   ) (   | () () |                                                      :\r\n:| (___) |   | |   | || || |                                                      :\r\n:|  ___  |   | |   | |(_)| |                                                      :\r\n:| (   ) |   | |   | |   | |                                                      :\r\n:| )   ( |   | |   | )   ( |                                                      :\r\n:|/     \\|   )_(   |/     \\|                                                      :\r\n···················································································\n");
                Console.WriteLine($"Välkommen {loggedinUser.Name}!");
                Console.WriteLine("Välj ett alternativ:");
                if (loggedinUser.Role == "Admin")
                    Console.WriteLine("1. Admin-meny"); // senare kan du lägga fler alternativ
                else
                    Console.WriteLine("1. Customer-meny");

                Console.WriteLine("0. Logga ut");

                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1":
                        if (loggedinUser.Role == "Admin")
                            AdminMenu(loggedinUser);
                        else
                            CustomerMenu(loggedinUser);
                        break;
                    case "0":
                        runMenu = false; // avsluta loop och logga ut
                        Console.WriteLine("Du har loggats ut.");
                        break;
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
            }
        }



        //meny för admin
        static void AdminMenu(User user)
        {

        }

        //meny för customer 
        static void CustomerMenu(User user)
        {
            Console.WriteLine($"[KUND] Inloggad som " + user.Name);
            Console.WriteLine("Välj ett alternativ.");
            Console.WriteLine(new string('*',30));
            Console.WriteLine("1. Visa konton.");
            Console.WriteLine("2. Öppna nytt konto.");
            Console.WriteLine("3. Överföring.");
            Console.WriteLine("4. Lån.");
            Console.WriteLine("5. Transaktionslog.");
            Console.WriteLine("6. Återgå till huvudmeny.");
            Console.WriteLine(new string('*',30));

            string inputStr = Console.ReadLine() ?? "";
            int input;
            if (!int.TryParse(inputStr, out input))
            {
                switch (input)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;

                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Felaktigt val.");
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
                Console.ReadKey();
                return;
            }
        }
    }
}

