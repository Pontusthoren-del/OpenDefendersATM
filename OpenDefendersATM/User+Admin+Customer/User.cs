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

        private string _pin;

        public User(string name, string role, string pin)
        {
            Name = name;
            Role = role;
            _pin = pin;
        }
        public void SetPin(string pin)
        {
            _pin = pin;
            Console.WriteLine("PIN ändrad.");
        }
        public bool CheckPin(string enteredPin)
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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"                          \tFel PIN! Försök {FailedAttempts} av 3.");
            Console.ResetColor();
            IsLocked = FailedAttempts >= 3;
            if (IsLocked)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("                               \t...Kontot är nu låst...");
                Console.ResetColor();
                BankSystem.LockedOutUsers.Add(this);
            }
            return false;
        }    
    }
}
