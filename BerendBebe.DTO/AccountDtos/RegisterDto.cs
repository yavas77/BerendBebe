using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.AccountDtos
{
    public class RegisterDto
    {
        [Display(Name = "Adı :")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadı :")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Display(Name = "Şifre :")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar :")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Kullanım Koşulları")]
        public bool Agree { get; set; }

        [Display(Name = "Kullanım Koşulları")]
        public int Gender { get; set; }
    }
}
