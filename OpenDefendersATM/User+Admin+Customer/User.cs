using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }

        private int _pin;

        public User(string name, string role, int pin)
        {
            Name = name;
            Role = role;
            _pin = pin;
        }

        public void SetPin(int pin)
        {
            _pin = pin;
            Console.WriteLine("PIN ändrad.");
            //Skapa en ny pin här också kanske?
        }
        public bool CheckPin(int enteredPin)
        {
            //If your account is locked, you get kicked out diretcly.
            if (IsLocked) return false;

            if (enteredPin == _pin)
            {
                FailedAttempts = 0;
                return true;
            }
            FailedAttempts++;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFel PIN! Försök {FailedAttempts} av 3.");
            Console.ResetColor();

            IsLocked = FailedAttempts >= 3;
            if (IsLocked)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kontot är låst.");
                Console.ResetColor();
                BankSystem.LockedOutUsers.Add(this);
            }
            return false;
        }
        public static User? Login(List<User> users)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t" + new string('*', 30));
            Console.WriteLine("*****VÄLKOMNA TILL OPEN DEFENDERS ATM*****");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Användarnamn: ");
            string? name = Console.ReadLine();
            Console.Write("PIN: ");
            //A do/while for out pincode. Will show * instead of the numbers you put in.. 
            string? pinInput = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && pinInput.Length > 0)
                {
                    pinInput = pinInput.Substring(0, pinInput.Length - 1);
                    Console.WriteLine("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pinInput += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            if (!int.TryParse(pinInput, out int pin))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFelaktigt format på PIN!");
                Console.ResetColor();
                return null;
            }
            User? user = users.FirstOrDefault(u => u.Name == name);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAnvändaren finns inte.");
                Console.ResetColor();
                return null;
            }
            else if (user.CheckPin(pin))
            {
                Console.WriteLine($"Välkommen {user.Name}");
                return user;
            }
            return null;
        }
        public void LogOut(User user)
        {
            Console.WriteLine($"\n{Name} har loggats ut.");
            Console.ReadKey();
        }
    }
}
