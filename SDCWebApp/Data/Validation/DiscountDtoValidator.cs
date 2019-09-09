﻿using FluentValidation;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class DiscountDtoValidator : AbstractValidator<DiscountDto>, ICustomValidator<DiscountDto>
    {
        public DiscountDtoValidator()
        {

            RuleFor(d => d.GroupSizeForDiscount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Null().When(d => d.Type != Discount.DiscountType.ForGroup).WithMessage($"If '{nameof(Discount.Type)}' property is not set to '{Discount.DiscountType.ForGroup.ToString()}' " +
                   $"then '{{PropertyName}}' must be null.");

            RuleFor(x => x.GroupSizeForDiscount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().When(d => d.Type == Discount.DiscountType.ForGroup).WithMessage($"If '{nameof(Discount.Type)}' property is set to '{Discount.DiscountType.ForGroup.ToString()}' " +
                    $"then '{{PropertyName}}' is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");


            RuleFor(d => d.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MaximumLength(256).WithMessage("'{PropertyName}' length must be less than 257 characters.");

            RuleFor(d => d.DiscountValueInPercentage)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("'{PropertyName}' is required.")
                .InclusiveBetween(0, 100).WithMessage("'{PropertyName}' is in percentage so must be inclusive between 0 and 100.");
        }
    }
}