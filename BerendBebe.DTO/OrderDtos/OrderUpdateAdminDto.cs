using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.OrderDtos
{
    public class OrderUpdateAdminDto
    {
        public int Id { get; set; }

        [Display(Name="Ad")]
        public string Name { get; set; }

        [Display(Name="Soyad")]
        public string LastName { get; set; }

        [Display(Name="Telefon")]
        public string Phone { get; set; }

        [Display(Name="Eamil Adres")]
        public string Email { get; set; }

        [Display(Name="Adres")]
        public string Address { get; set; }

        [Display(Name="Şehir")]
        public string City { get; set; }

        [Display(Name="Sipariş Notu")]
        public string Note { get; set; }

        [Display(Name="Ödeme Türü")]
        public int PaymentTypeId { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }


    }
}
