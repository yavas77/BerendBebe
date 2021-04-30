using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<List<Category>> GetAllNotDeletedAsync(string categoryName);
        Task<List<Category>> GetAllByActiveAsync();
        Task<bool> CategoryExistsAsync(int id);
        Task<bool> CategoryExistsAsync(string CategoryName);
    }
}
