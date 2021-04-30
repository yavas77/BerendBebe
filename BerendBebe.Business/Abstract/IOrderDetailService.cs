using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.Abstract
{
    public interface IOrderDetailService : IBaseService<OrderDetail>
    {
        Task<List<OrderDetail>> GetByOrderIdAsync(int id);
        Task<bool> OrderDetailExistsAsync(int OrderId, int OrderDetailId);

    }
}
