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
    public class EfProductRepository : EfBaseRepository<Product>, IProductDal
    {
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            using var context = new BerendBebeContext();
            return await context.Products.Where(x => x.IsDelete == true).OrderByDescending(x => x.Id).Include(x => x.Category).ToListAsync();
        }


    }
}
