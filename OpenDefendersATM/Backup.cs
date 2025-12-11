using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OpenDefendersATM
{
    internal class Backup
    {
        public static int ReadInt(string message)
        {

            int number;
            bool isValid = true;
            while (isValid)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
            return 0;
        }
        public static float ReadFloat(string message)
        {
            float number;
            bool isValid = true;
            while (isValid)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (float.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
            return 0f;
        }
        public static decimal ReadDecimal(string message)
        {
            decimal number;
            bool isValid = true;
            while (isValid)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
            return 0m;
        }
        public static string ReadString(string message)
        {
            bool isValid = true;
            while (isValid)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Felaktigt val.");
            }
            return "";
        }
    }
}