using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<List<Product>> GetAllNotDeletedAsync();
        Task<List<Product>> GetAllByActiveAsync();
        Task<List<Product>> GetAllByCategoryIdIsActiveAsync(int categoryId);
        Task<List<Product>> GetAllByFilterInProductNameAsync(string s);
        Task<bool> ProductExistsAsync(int id);
        Task<bool> ProductExistsAsync(string productName);
        Task<bool> ProductExistsAsync(string productName, int categoryId);
        Task<bool> ProductExistsByActiveAsync(int id);
        Task<bool> ProductUnitInStokCountControlAsync(int id, int count);
    }
}
