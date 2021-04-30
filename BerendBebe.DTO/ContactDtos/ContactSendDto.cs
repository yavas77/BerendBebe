using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.ContactDtos
{
    public class ContactSendDto
    {
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adres")]
        public string EmailAdress { get; set; }

        [Display(Name = "Başlık")]
        public string Subject { get; set; }

        [Display(Name = "Mesaj")]
        public string Message { get; set; }
    }
}
