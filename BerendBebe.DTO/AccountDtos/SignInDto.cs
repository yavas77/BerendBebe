using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.DTO.AccountDtos
{
    public class SignInDto
    {
        [Display(Name = "Kullanıcı Adı ")]
        public string UserName { get; set; }

        [Display(Name = "Şifre ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla ")]
        public bool IsPersistent { get; set; }
    }
}
