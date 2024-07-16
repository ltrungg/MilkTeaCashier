using Azure.Identity;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    
    public class UserAccountRepository
    {
        MilkTeaCashierContext _context;
        public Account getUser(string userName, string password)
        {
            _context = new MilkTeaCashierContext();
            return _context.Accounts.FirstOrDefault(x=>x.UserName==userName && x.PassWord==password);
        }
        public void Update(Account account) 
        { 
            _context= new MilkTeaCashierContext();
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }

    }
}
