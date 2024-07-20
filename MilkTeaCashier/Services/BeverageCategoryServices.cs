using Repositories;
using Repositories.Entities;

namespace Services
{
    public class BeverageCategoryServices
    {
        public BeverageCategoryRepository _repo = new();

        public List<BeverageCategory> GetCategories()
        {
            return _repo.GetAll().Where(x => x.Status.Equals("active")).ToList();
        }

        public void AddCategory(BeverageCategory x)
        {
            _repo.Add(x);
        }
        public void UpdateCategory(BeverageCategory x)
        {
            _repo.Update(x);
        }
        public void DeleteCategory(BeverageCategory x)
        {
            x.Status = "inactive";
            _repo.Delete(x);
        }
        public List<BeverageCategory> SearchByCategoryName(string name)
        {
            name = name.ToLower();
            return _repo.GetAll().Where(x => x.Name.ToLower().Contains(name) && x.Status.Equals("active")).ToList();
        }
    }
}
