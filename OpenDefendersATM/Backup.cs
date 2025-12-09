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
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
        }
        public static float ReadFloat(string message)
        {
            float number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (float.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
        }
        public static decimal ReadDecimal(string message)
        {
            decimal number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out number))
                {
                    return number;
                }
                Console.WriteLine("Felaktigt val.");
            }
        }

        public static string ReadString(string message)
        {
            decimal number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Felaktigt val.");
            }
        }
    }
}
