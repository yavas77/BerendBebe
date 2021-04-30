using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.OrderDtos
{
    public class OrderDetailUpdateAdminDto
    {
        public int Id { get; set; }

        [Display(Name = "Ürün Miktarı")]
        public int Quantity { get; set; }
    }
}
