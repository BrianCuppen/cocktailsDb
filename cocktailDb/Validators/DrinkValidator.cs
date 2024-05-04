using System;
using FluentValidation;

namespace CocktailDb.Validators;

public class DrinkValidator : AbstractValidator<Drink>
{
    public DrinkValidator()
    {
        //not Empty, drinkName not empty and less than 50 characters
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Drink name cannot be empty")
            .MaximumLength(50)
            .WithMessage("Drink name cannot be more than 50 characters").WithMessage("Your drinkname is invalid");
        //category not empty
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category cannot be empty");
    }
}
