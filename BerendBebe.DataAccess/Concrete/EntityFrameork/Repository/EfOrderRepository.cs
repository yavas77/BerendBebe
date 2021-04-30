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
    public class EfOrderRepository : EfBaseRepository<Order>, IOrderDal
    {
        public async Task<Order> GetByIdWithPaymentTypeAsync(int id)
        {
            using var context = new BerendBebeContext();
            return await context.Orders.Where(x => x.Id == id).Include(x => x.PaymentType).FirstOrDefaultAsync();
        }
    }
}
