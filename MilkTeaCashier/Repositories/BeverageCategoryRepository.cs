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

        public List<BeverageCategory> GetBeverageCategories()
        {
            _context = new MilkTeaCashierContext();
            return _context.BeverageCategories.ToList();
        }
    }
}
