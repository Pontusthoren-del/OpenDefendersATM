using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UIAdmin
    {
        //meny för admin
        public static void AdminMenu(User user)
        {
            bool loggedin = true;
            while (loggedin)
            {
                Console.WriteLine($"\t[ADMIN] Inloggad som " + user.Name);
                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Skapa ny användare");
                Console.WriteLine("2. Skapa ny admin");
                Console.WriteLine("3. Aktuell växlingskurs");
                Console.WriteLine("4. Logga ut.");
                Console.WriteLine(new string('*', 30));

                int input = Backup.ReadInt("Ditt val: ");
                Admin? admin = user as Admin;
                switch (input)
                {
                    case 1:
                        admin?.CreateNewCustomer();
                        break;
                    case 2:
                        admin?.CreateNewAdmin();
                        break;
                    case 3:
                        admin?.ExChangeRate();
                        break;
                    case 4:
                        loggedin = false;
                        break;

                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
            }
        }

    }
}
