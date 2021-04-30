using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BerendBebe.Entities.Concrete.Order;

namespace BerendBebe.DTO.OrderDtos
{
    public class OrderAdminDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public string CancelDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpDate { get; set; }
        public OrderStatus Status { get; set; }

        [Display(Name = "Kargo Firması :")]
        public string CargoName { get; set; }

        [Display(Name = "Kargo No :")]
        public string CargoNo { get; set; }

        [Display(Name = "Mesaj :")]
        public string CargoMessage { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string MyProperty { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
