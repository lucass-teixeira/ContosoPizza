﻿using ContosoPizza.Mediator.Commands.Requests;
using FluentValidation;

namespace ContosoPizza.Validators
{
    public class UpdateToppingRequestValidator : AbstractValidator<UpdateToppingRequest>
    {
        public UpdateToppingRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();

        }
    }
}