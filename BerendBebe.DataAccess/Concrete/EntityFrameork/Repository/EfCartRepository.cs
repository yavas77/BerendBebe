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
    public class EfCartRepository : EfBaseRepository<Cart>, ICartDal
    {
        public async Task<List<Cart>> GetAllWithProductAsync(string customer)
        {
            using var context = new BerendBebeContext();
            return await context.Carts.Where(x => x.Customer == customer).Include(x => x.Product).ToListAsync();
        }
    }
}
