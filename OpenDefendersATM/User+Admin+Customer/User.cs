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
        public int Role { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }
        private int _pin;

        public User(string name, int role, int pin)
        {
            Name = name;
            Role = role;
            _pin = pin;
        }
        public void SetPin(int pin)
        {
            _pin = pin;
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
            IsLocked = FailedAttempts >= 3;
            return false;

        }
        public User Login(List<User> users)
        {
            Console.Write("Användarnamn: ");
            string? name = Console.ReadLine();
            Console.Write("PIN: ");
            string? pinInput = Console.ReadLine();

            if (!int.TryParse(pinInput, out int pin))
            {
                Console.WriteLine("Felaktigt format på PIN!");
                return null;
            }
            var user = users.FirstOrDefault(u => u.Name == name);
            if (user == null)
            {
                Console.WriteLine("Användaren finns inte.");
                return null;
            }
            else if (user.CheckPin(pin))
            {
                Console.WriteLine($"Välkommen {user.Name}");
                return user;
            }
            else
            {
                Console.WriteLine(user.IsLocked ? "Kontot är låst." : "Fel PIN!");
                return null;
            }
        }

        public void LogOut()
        {
            Console.WriteLine($"{Name} har loggats ut.");

            FailedAttempts = 0;

            //ShowMainMenu();
        }
    }
}
