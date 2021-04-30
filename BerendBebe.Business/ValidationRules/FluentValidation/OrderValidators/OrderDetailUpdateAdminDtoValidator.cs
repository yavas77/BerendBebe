using BerendBebe.DTO.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.OrderValidators
{
    public class OrderDetailUpdateAdminDtoValidator:AbstractValidator<OrderDetailUpdateAdminDto>
    {
        public OrderDetailUpdateAdminDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Miktar girilmelidir!")
                .GreaterThan(0)
                .WithMessage("Miktar Sıfırdan büyük olmalıdır!");
        }
    }
}
