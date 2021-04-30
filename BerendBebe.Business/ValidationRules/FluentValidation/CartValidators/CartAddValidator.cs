using BerendBebe.DTO.CartDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.CartValidators
{
    public class CartAddValidator : AbstractValidator<CartAddDto>
    {
        public CartAddValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Ürün seçilmemiş. Lütfen ürün seçiniz!");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Miktar giriniz!");
        }
    }
}
