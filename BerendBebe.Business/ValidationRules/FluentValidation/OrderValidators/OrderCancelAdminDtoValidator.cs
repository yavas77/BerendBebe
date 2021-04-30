using BerendBebe.DTO.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.Business.ValidationRules.FluentValidation.OrderValidators
{
    public class OrderCancelAdminDtoValidator : AbstractValidator<OrderCancelAdminDto>
    {
        public OrderCancelAdminDtoValidator()
        {
            RuleFor(x => x.CancelDescription)
                .NotEmpty()
                .WithMessage("İptal nedeni boş geçilemez")
                .MaximumLength(500)
                .WithMessage("İptal nedeni en fazla 500 karakter olabilir");
        }
    }
}
