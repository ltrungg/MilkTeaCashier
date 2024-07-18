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

        public void AddBeverage(Beverage beverage)
        {
            _repository.AddBeverage(beverage);
        }


        public void DeleteBeverage(Beverage beverage)
        {
            beverage.Status = "inactive";
            _repository.DeleteBevrage(beverage);
        }

        public void UpdateBeverage(Beverage beverage)
        {
            _repository.UpdateBeverage(beverage);
        }

        public List<Beverage> SearchBeverage(string name)
        {
            return _repository.GetBeverages().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}