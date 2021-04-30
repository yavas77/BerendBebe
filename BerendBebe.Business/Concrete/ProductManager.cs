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
    public class ProductManager : BaseManager<Product>, IProductService
    {
        private readonly IBaseDal<Product> _baseDal;
        private readonly IProductDal _productDal;

        public ProductManager(IBaseDal<Product> baseDal, IProductDal productDal) : base(baseDal)
        {
            _baseDal = baseDal;
            _productDal = productDal;
        }

        public async Task<List<Product>> GetAllByActiveAsync()
        {
            return await _productDal.GetAllAsync(x => x.IsDelete == true && x.IsActive == true, x => x.Id);
        }

        public async Task<List<Product>> GetAllByCategoryIdIsActiveAsync(int categoryId)
        {
            return await _productDal.GetAllAsync(x => x.IsDelete == true && x.IsActive == true && x.CategoryId == categoryId, x => x.Id);
        }

        public async Task<List<Product>> GetAllByFilterInProductNameAsync(string s)
        {
            return await _productDal.GetAllAsync(x => x.IsDelete == true && x.IsActive == true && x.ProductName.Contains(s.ToLower()), x => x.Id);
        }

        public async Task<List<Product>> GetAllNotDeletedAsync()
        {
            return await _productDal.GetAllAsync(x => x.IsDelete == true, x => x.Id);
        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _productDal.GetAllWithCategoryAsync();
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            var productInDb = await _baseDal.GetAsync(x => x.IsDelete == true && x.Id == id);
            return productInDb == null ? false : true;
        }

        public async Task<bool> ProductExistsAsync(string productName)
        {
            var productInDb = await _baseDal.GetAsync(x => x.IsDelete == true && x.ProductName == productName);
            return productInDb == null ? false : true;
        }

        public async Task<bool> ProductExistsAsync(string productName, int categoryId)
        {
            var productInDb = await _baseDal.GetAsync(x => x.IsDelete == true && x.ProductName == productName && x.CategoryId == categoryId);
            return productInDb == null ? false : true;
        }

        public async Task<bool> ProductExistsByActiveAsync(int id)
        {
            var productInDb = await _baseDal.GetAsync(x => x.IsDelete == true && x.IsActive == true && x.Id == id);
            return productInDb == null ? false : true;
        }

        public async Task<bool> ProductUnitInStokCountControlAsync(int id, int count)
        {
            return await _baseDal.GetAsync(x => x.Id == id && x.UnitInStok >= count) == null ? false : true;
        }
    }
}
