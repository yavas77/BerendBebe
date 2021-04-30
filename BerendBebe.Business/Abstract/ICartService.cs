using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface ICartService:IBaseService<Cart>
    {
        Task<List<Cart>> GetAllWithProductAsync(string customer);
        Task<bool> CartExistsAsync(int id);
        Task<bool> ProductExistsInCartAsync(int productId, string customer);
        Task<Cart> GetCartByProductWithCustomerAsync(int productId, string customer);
    }
}
