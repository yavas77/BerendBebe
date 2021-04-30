using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.DTO.OrderDtos
{
    public class OrderAdminListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }




    }
}
