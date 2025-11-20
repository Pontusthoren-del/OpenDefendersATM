using System;
using System.Collections.Generic;
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
                    loggedInTries = 0;
                    //ShowMainMenu();
                    Console.Clear();
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

            static void ShowMainMenu() //Header och inloggningsmeny (JK)
            {
                bool RunBankApp = true;
                while (RunBankApp)
                {
                    Console.WriteLine("···················································································\r\n: _______  _______  _______  _                                                    :\r\n:(  ___  )(  ____ )(  ____ \\( (    /|                                             :\r\n:| (   ) || (    )|| (    \\/|  \\  ( |                                             :\r\n:| |   | || (____)|| (__    |   \\ | |                                             :\r\n:| |   | ||  _____)|  __)   | (\\ \\) |                                             :\r\n:| |   | || (      | (      | | \\   |                                             :\r\n:| (___) || )      | (____/\\| )  \\  |                                             :\r\n:(_______)|/       (_______/|/    )_)                                             :\r\n:                                                                                 :\r\n: ______   _______  _______  _______  _        ______   _______  _______  _______ :\r\n:(  __  \\ (  ____ \\(  ____ \\(  ____ \\( (    /|(  __  \\ (  ____ \\(  ____ )(  ____ \\:\r\n:| (  \\  )| (    \\/| (    \\/| (    \\/|  \\  ( || (  \\  )| (    \\/| (    )|| (    \\/:\r\n:| |   ) || (__    | (__    | (__    |   \\ | || |   ) || (__    | (____)|| (_____ :\r\n:| |   | ||  __)   |  __)   |  __)   | (\\ \\) || |   | ||  __)   |     __)(_____  ):\r\n:| |   ) || (      | (      | (      | | \\   || |   ) || (      | (\\ (         ) |:\r\n:| (__/  )| (____/\\| )      | (____/\\| )  \\  || (__/  )| (____/\\| ) \\ \\__/\\____) |:\r\n:(______/ (_______/|/       (_______/|/    )_)(______/ (_______/|/   \\__/\\_______):\r\n:                                                                                 :\r\n: _______ _________ _______                                                       :\r\n:(  ___  )\\__   __/(       )                                                      :\r\n:| (   ) |   ) (   | () () |                                                      :\r\n:| (___) |   | |   | || || |                                                      :\r\n:|  ___  |   | |   | |(_)| |                                                      :\r\n:| (   ) |   | |   | |   | |                                                      :\r\n:| )   ( |   | |   | )   ( |                                                      :\r\n:|/     \\|   )_(   |/     \\|                                                      :\r\n···················································································\n");
                    Console.WriteLine("Välkommen till Open Defenders ATM\n");
                    Console.WriteLine("Var vänligen gör ett val mellan nedan alternativ:");
                    Console.WriteLine("- Logga in som admin: 1");
                    Console.WriteLine("- Logga in som kund: 2");
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int choice))
                    {

                    }

                }
            }
        }
    }
}

