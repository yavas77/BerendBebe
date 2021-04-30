using BerendBebe.DTO.AccountDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.AccountValidotors
{
    public class SignInValidator : AbstractValidator<SignInDto>
    {
        public SignInValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş geçilemez!")
                .MaximumLength(10)
                .WithMessage("Kullanıcı adı en fazla 10 karakter olabilir!");

            RuleFor(p => p.Password)
               .NotEmpty()
               .WithMessage("Şifre boş bırakılamaz!")
               .MaximumLength(10)
               .WithMessage("Şifre en fazla 30 karakter olabilir!");

        }
    }
}