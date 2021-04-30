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
    public class ProductImageManager : BaseManager<ProductImage>, IProductImageService
    {
        private readonly IBaseDal<ProductImage> _baseDal;
        private readonly IProductImageDal _productImageDal;
        public ProductImageManager(IBaseDal<ProductImage> baseDal, IProductImageDal productImageDal) : base(baseDal)
        {
            _baseDal = baseDal;
            _productImageDal = productImageDal;
        }

        public async Task<List<ProductImage>> GetAllByProductId(int id)
        {
            var productImage = await _baseDal.GetAllAsync(x => x.ProductId == id);
            return productImage;
        }

  

        public async Task<ProductImage> GetWithProductByIdAsync(int id)
        {
            return await _productImageDal.GetWithProductByIdAsync(id);
        }

        public async Task<bool> ProductImageExistsAsync(int id)
        {
            return await _baseDal.FindByIdAsync(id) == null ? false : true;
        }
    }
}
