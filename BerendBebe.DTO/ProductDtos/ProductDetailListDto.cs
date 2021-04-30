using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.ProductDtos
{
    public class ProductDetailListDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ShowImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int UnitInStok { get; set; }
    }
}
