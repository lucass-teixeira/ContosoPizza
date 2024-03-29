﻿using FluentValidation;

namespace ContosoPizza.Features.Toppings.Update
{
    public class UpdateToppingRequestValidator : AbstractValidator<UpdateToppingRequest>
    {
        public UpdateToppingRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
