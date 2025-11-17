using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDefendersATM
{
    internal class Customer:User
    {
        List<Account> account = new List<Account>();

        public void ViewAccounts()
        {
            //Visar kundens alla konton
        }
        public void TransferBetweenAccounts()
        {
            //Flytta pengar mellan egna konton
        }
        public void OpenAccount()
        {
            //Öppna ett nytt konto
        }
        public void OpenSavingsAccount()
        {
            //Öppnar ett nytt sparkonto
        }
        public void RequestLoan()
        {
            //Begär ett lån
        }
        public void ViewTransaction()
        {
            //Visar en transaktionslista
        }
        public void TransferToOtherCustomers()
        {
            //Flytta pengar till en annan kund
        }
        public void LockedOut()
        {
            //Utelåst från kontot, vänta på att admin ska låsa upp det.
        }
    }
}
