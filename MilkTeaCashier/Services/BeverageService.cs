using Repositories;
using Repositories.Entities;

namespace Services
{
    public class BeverageService
    {
        private BeverageRepository _repository = new();



        public List<Beverage> GetBeverages()
        {
            return _repository.GetBeverages();
        }



        public void DeleteBeverage(Beverage beverage)
        {
            _repository.DeleteBevrage(beverage);
        }

        public void UpdateBeverage(Beverage beverage)
        {
            _repository.UpdateBeverage(beverage);
        }
    }
}
