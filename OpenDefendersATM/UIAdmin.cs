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
        public static bool AdminMenu(User user) //meny för admin
        {
            bool loggedin = true;
            while (loggedin)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\t[KUND] Inloggad som " + user.Name);
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Välj ett alternativ.");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("1. Skapa ny användare.");
                Console.WriteLine("2. Uppdatera växelkurs.");
                Console.WriteLine("3. Lås upp låsta konton.");
                Console.WriteLine("4. Visa alla konton.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("5. Logga ut.");
                Console.ResetColor();
                Console.WriteLine(new string('*', 30));
                int input = Backup.ReadInt("Ditt val: ");
                Console.WriteLine();
                Admin? admin = user as Admin;
                switch (input)
                {
                    case 1:
                        CreateCustomerUI(user); //Create new user
                        break;
                    case 2:
                        BankSystem.UpdateRate();//Update rates
                        break;
                    case 3:
                        UnlockUsers(); //Unlock locked users.
                        break;
                    case 4:
                        BankSystem.PrintAllUsers(); //Print all users
                        break;
                    case 5:
                        UI.LogOut(user);
                        return false; //Return to login
                    default:
                        Console.WriteLine("Felaktigt val.");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Tryck Enter för att fortsätta...");
                Console.ReadLine();
            }
            return false;
        }
        public static void UnlockUsers()
        {
            Console.WriteLine("Låsta anvädare: ");
            for (int i = 0; i < BankSystem.LockedOutUsers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {BankSystem.LockedOutUsers[i].Name}.");
            }
            if (BankSystem.LockedOutUsers.Count == 0)
            {
                Console.WriteLine("Inga låsta konton.");
                return;
            }
            int index = Backup.ReadInt("Välj användare att låsa upp: ") - 1;
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
        private static void CreateCustomerUI(User user) //Create new customers as admin
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t[KUND] Inloggad som " + user.Name);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("Skapa ny användare");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine();
            string name = Backup.ReadString("Lägg till användarnamn: ");
            Console.WriteLine();
            foreach (var u in BankSystem.Users)
            {
                if (u.Name == name)
                {
                    Console.WriteLine("En användare med det användarnamnet finns redan!");
                    return;
                }
            }
            //Create new pin
            string pin = ReadValidPinCode();
            Console.WriteLine();
            //Startbalance
            decimal startbalance = Backup.ReadDecimal("Startbelopp SEK: ");
            Console.WriteLine();
            //Create a unique account.
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
            var firstAccount = new Account(newAccountID, startbalance, "SEK", "Privatkonto");
            var newCustomer = new Customer(name, "Customer", pin, startbalance)
            {
                CustomerAccounts = new List<Account> { firstAccount }
            };
            BankSystem.Users.Add(newCustomer);
            Console.WriteLine($"Ny kund skapad med användarnamn {name} och startbelopp {startbalance} SEK.");
            Console.WriteLine($"Privatkonto med KontoID: {newAccountID}.");
            Console.WriteLine();
        }
        private static string ReadValidPinCode()
        {
            string pin;
            bool isValid;
            do
            {
                pin = Backup.ReadString("Lägg in PIN-kod (4 siffror): ");
                isValid = pin.Length == 4 && pin.All(char.IsDigit);
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