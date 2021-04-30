using BerendBebe.DTO.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidatiors.ProductValidatons
{
    public class ProductAddValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddValidator()
        {
            RuleFor(x => x.ProductName)
              .NotEmpty()
              .WithMessage("Ürün adı boş geçilemez!");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Kategori seçiniz!");
        }
    }
}
