using BerendBebe.DataAccess.Absctract;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Context;
using BerendBebe.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DataAccess.Concrete.EntityFrameork.Repository
{
    public class EfProductImageRepository : EfBaseRepository<ProductImage>, IProductImageDal
    {
        public async Task<ProductImage> GetWithProductByIdAsync(int id)
        {
            using var context = new BerendBebeContext();
            return await context.ProductImages.Where(x => x.Id == id).Include(x => x.Product).FirstOrDefaultAsync();
        }
    }
}
