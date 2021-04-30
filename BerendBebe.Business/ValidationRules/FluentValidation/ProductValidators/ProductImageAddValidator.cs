using BerendBebe.DTO.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.ProductValidators
{
    public class ProductImageAddValidator : AbstractValidator<ProductImageAddDto>
    {
        public ProductImageAddValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Resim seçilmelidir!");
        }
    }
}
