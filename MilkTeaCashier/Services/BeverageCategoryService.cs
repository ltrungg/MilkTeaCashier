using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BeverageCategoryService
    {

        private BeverageCategoryRepository _repository = new BeverageCategoryRepository();


        public List<BeverageCategory> GetCategories()
        {
            return _repository.GetBeverageCategories();
        }
    }
}
