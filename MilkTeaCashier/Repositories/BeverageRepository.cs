using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories
{
    public class BeverageRepository
    {
        private MilkTeaCashierContext _context;

        public List<Beverage> GetBeverages()
        {
            _context = new MilkTeaCashierContext(); 
            return _context.Beverages.Include("IdCategoryNavigation").Where(x => x.Status.Equals("active")).ToList();
        }

        public void AddBeverage(Beverage beverage)
        {
            _context = new MilkTeaCashierContext();
            _context.Add(beverage);
            _context.SaveChanges();
        }

        public void DeleteBevrage(Beverage beverage)
        {
            _context = new MilkTeaCashierContext();
            _context.Update(beverage);
            _context.SaveChanges();
        }

        public void UpdateBeverage(Beverage beverage)
        {
            _context = new MilkTeaCashierContext();
            _context.Update(beverage);
            _context.SaveChanges();
        }
    }
}
