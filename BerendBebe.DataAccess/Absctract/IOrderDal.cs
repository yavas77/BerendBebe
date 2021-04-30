using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DataAccess.Absctract
{
    public interface IOrderDal : IBaseDal<Order>
    {
        Task<Order> GetByIdWithPaymentTypeAsync(int id);
    }
}
