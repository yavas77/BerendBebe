using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.ProductDtos
{
    public class ProductImageListDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ShowImageUrl { get; set; }
        public List<ProductImageDetailListDto> ProductImages { get; set; }
    }
}
