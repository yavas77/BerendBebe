using BerendBebe.DTO.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.OrderValidators
{
    public class OrderCargoAdminAddValidator : AbstractValidator<OrderCargoAdminAddDto>
    {
        public OrderCargoAdminAddValidator()
        {
            RuleFor(x => x.CargoName)
               .NotEmpty()
               .WithMessage("Kargo Firma alanı boş geçilemez!")
               .MaximumLength(100)
               .WithMessage("Kargo Firma alanı en fazla 100 karakter olabilir!");

            RuleFor(x => x.CargoNo)
              .NotEmpty()
              .WithMessage("Kargo numarası alanı boş geçilemez!")
              .MaximumLength(100)
              .WithMessage("Kargo numarası alanı en fazla 100 karakter olabilir!");
        }
    }
}
