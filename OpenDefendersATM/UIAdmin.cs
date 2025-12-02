using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class UIAdmin
    {
        
        public static void AdminMenu(User user) //meny för admin
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
                        CreateCustomerUI();
                        break;
                    case 2:
                        CreateAdminUI(); 
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

        private static void CreateCustomerUI()
        {
            Console.WriteLine("Skapa ny kund");
            Console.WriteLine(new string('*', 30));

        } 

        private static void CreateAdminUI()
        {
            Console.WriteLine("Skapa ny admin");
            Console.WriteLine(new string('*', 30));

        } 

        
    }
}
