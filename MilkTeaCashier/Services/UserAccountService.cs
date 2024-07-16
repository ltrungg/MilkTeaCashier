using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserAccountService
    {
        UserAccountRepository _repo= new ();
        public Account getUser(string username, string password)
        {
            return _repo.getUser(username, password);
        }
        public void UpdateUser(Account account)
        {
            _repo.Update(account);
        }
    }
}
