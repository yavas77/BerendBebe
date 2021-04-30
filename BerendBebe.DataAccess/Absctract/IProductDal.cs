using BerendBebe.Entities.Concrete;
using BerendBebe.DataAccess.Absctract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DataAccess.Absctract
{
    public interface IProductDal:IBaseDal<Product>
    {
        Task<List<Product>> GetAllWithCategoryAsync();
    }
}
