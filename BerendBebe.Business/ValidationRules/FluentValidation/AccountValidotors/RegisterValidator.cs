using BerendBebe.DTO.AccountDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.AccountValidotors
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş geçilemez!")
                .MaximumLength(10)
                .WithMessage("Kullanıcı adı en fazla 10 karakter olabilir!");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Adı alanı boş geçilemez!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı boş geçilemez!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email alanı boş geçilemez!")
                .MaximumLength(150)
                .WithMessage("Email en fazla 150 karakter olabilir!")
                .Must(p => p != null && Regex.IsMatch(p, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                .WithMessage("Eposta uygun biçimde girilmemiş!");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Şifre boş bırakılamaz!")
                .MaximumLength(10)
                .WithMessage("Şifre en fazla 30 karakter olabilir!")
                .Equal(p => p.ConfirmPassword)
                .WithMessage("Şifre ve şifre tekrar alanları uyuşmuyor!");

            RuleFor(p => p.ConfirmPassword)
              .NotEmpty()
              .WithMessage("Şifre tekrar alanı boş bırakılamaz!")
              .MaximumLength(10)
              .WithMessage("Şifre tekrar alanı en fazla 30 karakter olabilir!");


            RuleFor(p => p.Agree)
                .Must(isCheched => isCheched == true)
                .WithMessage("Kullanıcı koşullarını kabul etmelisiniz!");

        }
    }
}
