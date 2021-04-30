using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class ProductImage:IEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTitle { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
