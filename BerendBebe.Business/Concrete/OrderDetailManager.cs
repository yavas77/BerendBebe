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
    public class OrderDetailManager : BaseManager<OrderDetail>, IOrderDetailService
    {
        private readonly IBaseDal<OrderDetail> _baseDal;

        public OrderDetailManager(IBaseDal<OrderDetail> baseDal) : base(baseDal)
        {
            _baseDal = baseDal;
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(int id)
        {
            return await _baseDal.GetAllAsync(x => x.OrderId == id);
        }

        public async Task<bool> OrderDetailExistsAsync(int OrderId, int OrderDetailId)
        {
            return await _baseDal.GetAsync(x => x.Id == OrderDetailId && x.OrderId == OrderId) != null;
        }
    }
}
