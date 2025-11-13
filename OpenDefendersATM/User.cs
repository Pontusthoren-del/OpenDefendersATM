using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class User
    {
        public string Name { get; set; }
        public int Role { get; set; }
        private int _pin { get; set; }

        public static bool Login()
        {
            //Login metod
            return true;
        }

        public void LogOut()
        {
            //Logga ut
        }
    }
}
