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

        public void CreateNewCustomer()
        {
            
            
        }
        public Admin CreateNewAdmin(string name, string role, int pin)
        {
            return new Admin(name, role, pin);
        }
        public void ExChangeRate()
        {
            //Växlningskursen
        }
    }
}
