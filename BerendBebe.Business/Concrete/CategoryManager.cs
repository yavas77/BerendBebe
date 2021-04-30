using BerendBebe.Business.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Concrete
{
    public class CategoryManager : BaseManager<Category>, ICategoryService
    {
        private readonly IBaseDal<Category> _baseDal;
        public CategoryManager(IBaseDal<Category> baseDal) : base(baseDal)
        {
            _baseDal = baseDal;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            var category = await _baseDal.GetAsync(x => x.IsDelete == true && x.Id == id);
            return category == null ? false : true;
        }

        public async Task<bool> CategoryExistsAsync(string CategoryName)
        {
            var category = await _baseDal.GetAsync(x => x.IsDelete == true && x.CategoryName == CategoryName);
            return category == null ? false : true;
        }

        public async Task<List<Category>> GetAllByActiveAsync()
        {
            var categoriesInDb = await _baseDal.GetAllAsync(x => x.IsActive == true && x.IsDelete == true);
            return categoriesInDb;
        }

        public async Task<List<Category>> GetAllNotDeletedAsync(string categoryName)
        {
            if (categoryName != null)
                return await _baseDal.GetAllAsync(x => x.IsDelete == true && x.CategoryName.Contains(categoryName.ToLower()));


            return await _baseDal.GetAllAsync(x => x.IsDelete == true);

        }
    }
}
