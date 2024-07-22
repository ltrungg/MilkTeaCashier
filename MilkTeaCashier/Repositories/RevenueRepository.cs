using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    
    public class RevenueRepository
    {
        private MilkTeaCashierContext _context;

        public List<BillInfo> GetAll()
        {
            _context = new();
            return _context.BillInfos.Include("IdBillNavigation").Include("IdBeverageNavigation").ToList();
        }

        public void Delete(BillInfo x)
        {
            _context = new();
            _context.BillInfos.Remove(x);
            _context.SaveChanges();
        }
    }
}
