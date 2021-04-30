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
    public class CartManager : BaseManager<Cart>, ICartService
    {
        private readonly IBaseDal<Cart> _baseDal;
        private readonly ICartDal _cartDal;

        public CartManager(IBaseDal<Cart> baseDal, ICartDal cartDal) : base(baseDal)
        {
            _baseDal = baseDal;
            _cartDal = cartDal;
        }

        public async Task<bool> CartExistsAsync(int id)
        {
            var cartInDb = await _baseDal.FindByIdAsync(id);
            return cartInDb == null ? false : true;
        }

        public async Task<List<Cart>> GetAllWithProductAsync(string customer)
        {
            return await _cartDal.GetAllWithProductAsync(customer);
        }

        public async Task<Cart> GetCartByProductWithCustomerAsync(int productId, string customer)
        {
            return await _baseDal.GetAsync(x => x.ProductId == productId && x.Customer == customer);
        }

        public async Task<bool> ProductExistsInCartAsync(int productId, string customer)
        {
            var cartInDb = await _baseDal.GetAsync(x => x.ProductId == productId && x.Customer == customer);
            return cartInDb == null ? false : true;
        }
    }
}
