﻿using FluentValidation;
using FluentValidation.Validators;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validators
{
    public class TicketTariffDtoValidator : AbstractValidator<TicketTariffDto>, ICustomValidator<TicketTariffDto>
    {
        public TicketTariffDtoValidator()
        {
            RuleFor(x => x.DefaultPrice)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .GreaterThan(0.0f).WithMessage("{PropertyName} should be greater than 0.")
                    .LessThanOrEqualTo(1000.0f).WithMessage("{PropertyName} should be less than 1000.");

            RuleFor(x => x.Description)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .MaximumLength(256);
        }
    }
}