using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RevenueService
    {
        private RevenueRepository _repo = new();

        public List<BillInfo> GetAllBills()
        {
            return _repo.GetAll();
        }

        public void RemoveBill(BillInfo x)
        {
            _repo.Delete(x);
        }
    }
}
