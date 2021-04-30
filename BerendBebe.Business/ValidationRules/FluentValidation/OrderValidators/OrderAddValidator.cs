using BerendBebe.DTO.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.OrderValidators
{
    public class OrderAddValidator : AbstractValidator<OrderAddDto>
    {
        public OrderAddValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad alanı boş geçilemez!")
                .MaximumLength(20)
                .WithMessage("Ad en fazla 20 karakter olabilir!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı boş geçilemez!")
                .MaximumLength(20)
                .WithMessage("Soyad en fazla 20 karakter olabilir!");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Telefon alanı boş geçilemez!")
                .MaximumLength(10)
                .WithMessage("Telefon en fazla 10 karakter olabilir!")
                .Must(p => p != null && Regex.IsMatch(p, @"^((?:[0-9]\-?){6,14}[0-9])|((?:[0-9]\x20?){6,14}[0-9])$"))
                .WithMessage("Telefon numarası uygun biçimde girilmemiş!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email alanı boş geçilemez!")
                .MaximumLength(150)
                .WithMessage("Email en fazla 150 karakter olabilir!")
                .Must(p => p != null && Regex.IsMatch(p, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                .WithMessage("Eposta uygun biçimde girilmemiş!");

            RuleFor(x => x.Address)
               .NotEmpty()
               .WithMessage("Adres alanı boş geçilemez!")
               .MaximumLength(300)
               .WithMessage("Adres en fazla 300 karakter olabilir!");

            RuleFor(x => x.City)
              .NotEmpty()
              .WithMessage("Şehir alanı boş geçilemez!")
              .MaximumLength(15)
              .WithMessage("Adres en fazla 15 karakter olabilir!");

            RuleFor(x => x.PaymentTypeId)
              .NotEmpty()
              .WithMessage("Ödeme türü seçiniz!");



        }
    }
}
