using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string ShowImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
