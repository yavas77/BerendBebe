using BerendBebe.DataAccess.Absctract;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.Business.Abstract
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<List<Order>> GetAllOrderDescById();
        Task<List<Order>> GetAllGettingReadyAsync();
        Task<List<Order>> GetAllReadyOrInCargoSortedByIdAsync();
        Task<List<Order>> GetAllSortedByIdAsync(OrderStatus orderStatus);
        Task<List<Order>> GetAllCompletedAsync();
        Task<Order> GetByIdWithPaymentTypeAsync(int id);
        Task<bool> OrderExistsAsync(int id);
    }
}
