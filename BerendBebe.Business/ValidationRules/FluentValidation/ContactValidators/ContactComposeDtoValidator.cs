using BerendBebe.DTO.ContactDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.ContactValidators
{
    public class ContactComposeDtoValidator : AbstractValidator<ContactComposeDto>
    {
        public ContactComposeDtoValidator()
        { 

            RuleFor(x => x.Subject)
               .NotEmpty()
               .WithMessage("Başlık alanı boş geçilemez!")
               .MaximumLength(20)
               .WithMessage("Başlık en fazla 20 karakter olabilir");

            RuleFor(x => x.Message)
               .NotEmpty()
               .WithMessage("Mesaj alanı boş geçilemez!")
               .MaximumLength(300)
               .WithMessage("Mesaj en fazla 300 karakter olabilir");

            RuleFor(x => x.EmailAdress)
                .NotEmpty()
                .WithMessage("Email alanı boş geçilemez!")
                .MaximumLength(150)
                .WithMessage("Email en fazla 150 karakter olabilir!")
                .Must(p => p != null && Regex.IsMatch(p, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                .WithMessage("Eposta uygun biçimde girilmemiş!");
        }
    }
}
