using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.ContactDtos
{
    public class ContactDetailAdminDto
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }

        [Display(Name = "Başlık")]
        public string Subject { get; set; }

        [Display(Name = "Mesaj")]
        public string Message { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name = "Tarih")]
        public DateTime CreateDate { get; set; }

    }
}
