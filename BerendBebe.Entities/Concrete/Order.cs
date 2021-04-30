using BerendBebe.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Entities.Concrete
{
    public class Order : Loggable, IEntity
    {

        public enum OrderStatus
        {
            [Display(Name = "Hazırlanıyor")]
            Ready = 1,

            [Display(Name = "Kargoya Verildi")]
            InCargo = 2,

            [Display(Name = "Teslim Edildi")]
            Confirmed = 3,

            [Display(Name = "İptal Edildi")]
            Cancelled = 4,

            [Display(Name = "İade Edildi")]
            Returned = 5
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public string CargoName { get; set; }
        public string CargoNo { get; set; }
        public string CargoMessage { get; set; }
        public OrderStatus Status { get; set; }
        public string CancelDescription { get; set; }


        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }


        public Order()
        {
            Status = OrderStatus.Ready;
        }





    }
}
