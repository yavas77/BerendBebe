using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IProductImageService : IBaseService<ProductImage>
    {
        Task<List<ProductImage>> GetAllByProductId(int id);
        Task<ProductImage> GetWithProductByIdAsync(int id);
        Task<bool> ProductImageExistsAsync(int id);
    }
}
