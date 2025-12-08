using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                Console.WriteLine("1. Se låsta konton");
                Console.WriteLine("2. Skapa ny användare");
                Console.WriteLine("3. Aktuell växlingskurs");
                Console.WriteLine("4. Logga ut.");
                Console.WriteLine(new string('*', 30));
                
                


                int input = Backup.ReadInt("Ditt val: ");
                Admin? admin = user as Admin;
                switch (input)
                {
                    ////case 1:
                    ////    admin? här ska det eventuellt gå att se alla låsta konton som admin
                    //    break;
                    case 2:
                        CreateCustomerUI(user); //skapa ny användare
                        break;
                    case 3:
                        admin?.ExChangeRate(); //se växlingskurs
                        break;
                    case 4:
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
        public static void UnlockUsers()
        {
            Console.WriteLine("Låsta anvädare: ");

            for (int i = 0; i < BankSystem.LockedOutUsers.Count; i++)
            {
                Console.WriteLine($"{i+1}. {BankSystem.LockedOutUsers[0].Name}.");
            }
            if (BankSystem.LockedOutUsers.Count == 0)
            {
                Console.WriteLine("Inga låsta konton.");
                return;
            }
            int index = Backup.ReadInt("Välj användare att låsa upp: ");
            if (index < 0 || index >= BankSystem.LockedOutUsers.Count)
            {
                Console.WriteLine("Felaktigt val.");
                return; 
            }
            var user = BankSystem.LockedOutUsers[index];
            user.IsLocked = false;
            user.FailedAttempts = 0;
            BankSystem.LockedOutUsers.Remove(user);
            Console.WriteLine($"{user.Name} är nu upplåst.");
        }





       

        private static void CreateCustomerUI(User user) //skapa ny användare som admin
        {
            Console.Clear();
            Console.WriteLine($"\t[ADMIN] Inloggad som " + user.Name);
            Console.WriteLine();
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("Skapa ny användare");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine();


            //skapa nytt användarnamn
            string name = Backup.ReadString("Lägg till användarnamn: ");
            Console.WriteLine();


            //kontrollera om användarnamn redan finns
            foreach (var u in BankSystem.Users)
            {
                if (u.Name == name)
                {
                    Console.WriteLine("En användare med det användarnamnet finns redan!");
                    return;
                }
            }

            //skapa ny PIN-kod
            int pin = ReadValidPinCode();
            Console.WriteLine();

            //startbalans
            decimal startbalance = Backup.ReadDecimal("Startbelopp SEK: ");
            Console.WriteLine();

            // skapa unikt kontoID (tar högsta befintliga +1)
            int newAccountID = 10000;
            foreach (var u in BankSystem.Users)
            {
                if (u is Customer c)
                {
                    foreach (var acc in c.CustomerAccounts)
                    {
                        if (acc.GetAccountID() >= newAccountID)
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
                CustomerAccounts = new List<Account> { firstAccount }
            };

            //lägg till i listan 
            BankSystem.Users.Add(newCustomer);

            Console.WriteLine($"Ny kund skapad med användarnamn {name} och startbelopp {startbalance} SEK.");
            Console.WriteLine($"Privatkonto med KontoID: {newAccountID}.");
            Console.WriteLine();
            
        }

        // hjälpmetod för att pin-kod ska bli rätt
        private static int ReadValidPinCode()
        {
            int pin;
            bool isValid;

            do
            {
                pin = Backup.ReadInt("Lägg in PIN-kod (4 siffror mellan 1000 och 9999): ");
                isValid = pin >= 1000 && pin <= 9999;

                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig PIN-kod!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("Försök igen.");
                    Console.WriteLine();
                    
                }
            } while (!isValid);

            return pin;
        }
    }
}








