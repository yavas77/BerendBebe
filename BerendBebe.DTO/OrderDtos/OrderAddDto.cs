using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.OrderDtos
{
    public class OrderAddDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public int PaymentTypeId { get; set; }

        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShowImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal CartTotal { get; set; }

        public List<Cart> Carts { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
    }
}
