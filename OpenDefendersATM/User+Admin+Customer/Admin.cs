using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Admin : User
    {
        public Admin(string name, string role, int pin) : base(name, role, pin)
        {
        }

        public void CreateNewUser()
        {
            //Skapar en ny kontohavare
        }
        public void CreateNewAdmin()
        {
            //Skapar en ny Admin
        }
        public void ExChangeRate()
        {
            //Växlningskursen
        }
    }
}
