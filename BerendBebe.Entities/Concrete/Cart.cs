using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class Cart : IEntity
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public string Customer { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
