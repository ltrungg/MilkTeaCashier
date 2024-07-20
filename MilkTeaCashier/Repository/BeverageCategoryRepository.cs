using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    public class BeverageCategoryRepository
    {
        private MilkTeaCashierContext _context;

        public List<BeverageCategory> GetAll()
        {
            _context = new MilkTeaCashierContext();
            return _context.BeverageCategories.ToList();
        }
        public void Add(BeverageCategory x)
        {
            _context = new();
            _context.BeverageCategories.Add(x);
            _context.SaveChanges();
        }
        public void Update(BeverageCategory x)
        {
            _context = new();
            _context.BeverageCategories.Update(x);
            _context.SaveChanges();
        }
        public void Delete(BeverageCategory x)
        {
            _context = new();
            _context.BeverageCategories.Remove(x);
            _context.SaveChanges();

        }
    }
}
