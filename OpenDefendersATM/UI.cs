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
        }
    }
}

