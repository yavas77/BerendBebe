using BerendBebe.Business.Abstract;
using BerendBebe.DataAccess.Absctract;
using BerendBebe.DataAccess.Concrete.EntityFrameork.Context;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.Business.Concrete
{
    public class OrderManager : BaseManager<Order>, IOrderService
    {
        private readonly IBaseDal<Order> _baseDal;
        private readonly IOrderDal _orderDal;

        public OrderManager(IBaseDal<Order> baseDal, IOrderDal orderDal) : base(baseDal)
        {
            _baseDal = baseDal;
            _orderDal = orderDal;
        }

        public async Task<List<Order>> GetAllSortedByIdAsync(OrderStatus orderStatus)
        {
            return await _baseDal.GetAllAsync(x => x.Status == orderStatus, x => x.CreateDate);
        }

        public async Task<List<Order>> GetAllCompletedAsync()
        {
            //using var context = new BerendBebeContext();
            return await _baseDal.GetAllAsync(x => x.Status == OrderStatus.Confirmed, x => x.CreateDate);
        }

        public async Task<List<Order>> GetAllGettingReadyAsync()
        {
            //using var context = new BerendBebeContext();
            return await _baseDal.GetAllAsync(x => x.Status == OrderStatus.Ready);
        }

        public async Task<List<Order>> GetAllReadyOrInCargoSortedByIdAsync()
        {
            return await _baseDal.GetAllAsync(x => x.Status == OrderStatus.Ready || x.Status == OrderStatus.InCargo, x => x.CreateDate);
        }

        public async Task<Order> GetByIdWithPaymentTypeAsync(int id)
        {
            return await _orderDal.GetByIdWithPaymentTypeAsync(id);
        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            return await _baseDal.FindByIdAsync(id) != null;
        }

        public async Task<List<Order>> GetAllOrderDescById()
        {
            return await _baseDal.GetAllAsync(x => x.Id);
        }
    }
}
