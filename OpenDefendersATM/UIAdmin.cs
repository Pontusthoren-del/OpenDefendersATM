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
                Console.Clear();
                Console.WriteLine($"\t[ADMIN] Inloggad som " + user.Name);
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Skapa ny användare");
                Console.WriteLine("2. Aktuell växlingskurs");
                Console.WriteLine("3. Logga ut.");
                Console.WriteLine(new string('*', 30));
                

                int input = Backup.ReadInt("Ditt val: ");
                Admin? admin = user as Admin;
                switch (input)
                {
                    case 1:
                        CreateCustomerUI(user);
                        break;

                    case 2:
                        admin?.ExChangeRate();
                        break;
                    case 3:
                        UI.RunBankApp(); //ändrat så inte applikationen stängs ner
                        break;
                    //loggedin = false;
                    //break;

                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
            }

           
        }

        private static void CreateCustomerUI(User user) //skapa ny användare som admin
        {
            Console.WriteLine($"\t[ADMIN] Inloggad som " + user.Name);
            Console.Clear();
            Console.WriteLine("Skapa ny användare");
            Console.WriteLine(new string('*', 30));


            //skapa nytt användarnamn
            string name = Backup.ReadString("Lägg till användarnamn: ");
            

            //kontrollera om användarnamn redan finns
            foreach (var u in BankSystem.Users)
            {
                if (u.Name == name)
                {
                    Console.WriteLine("En användare med det användarnamnet finns redan!");
                    return;
                }
            }
            
            //pin-kod
            int pin = Backup.ReadInt("Lägg in PIN-kod. 4 siffror: ");

            //startbalans
            decimal startbalance = Backup.ReadDecimal("Startbelopp SEK: ");

            // skapa unikt kontoID (tar högsta befintliga +1)
            int newAccountID = 10000;
            foreach(var u in BankSystem.Users)
            {
                if(u is Customer c)
                {
                    foreach (var acc in c.CustomerAccounts)
                    {
                        if(acc.GetAccountID() >= newAccountID)
                        {
                            newAccountID = acc.GetAccountID() + 1;
                        }
                    }
                }


            }

            //skapa konto
            var firstAccount = new Account(newAccountID, startbalance, "SEK", "Privatkonto");

            //skapa kund
            var newCustomer = new Customer(name, "Customer", pin, startbalance)
            {
                CustomerAccounts = new List<Account> {firstAccount}
            };

            //lägg till i listan 
            BankSystem.Users.Add(newCustomer);

            Console.WriteLine($"Ny kund skapad med användarnamn {name} och startbelopp {startbalance} SEK.");
        } 
       



            

        private static void CreateAdminUI()
        {
            Console.WriteLine("Skapa ny admin");
            Console.WriteLine(new string('*', 30));

            string name = Backup.ReadString("Lägg till användarnamn: ");
            int pin = Backup.ReadInt("Lägg in PIN-kod. 4 siffror: ");

        } 

        
    }
}
