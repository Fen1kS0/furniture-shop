using FluentValidation;
using FurnitureShop.Backend.Common.DTOs.Buyers;

namespace FurnitureShop.Backend.BL.Validations.Buyers;

public class UpdateBuyerValidator : AbstractValidator<UpdateBuyerRequest>
{
    public UpdateBuyerValidator()
    {
        RuleFor(b => b.Name)
            .MinimumLength(3).WithMessage("Name length must be greater than or equal to 3")
            .MaximumLength(50).WithMessage("Name length must be less than or equal to 50");
        
        RuleFor(b => b.NumberPhone)
            .MinimumLength(10).WithMessage("Number phone length must be greater than or equal to 10")
            .MaximumLength(20).WithMessage("Number phone length must be less than or equal to 20");
        
        RuleFor(b => b.Address)
            .NotEmpty().WithMessage("Address must be not empty")
            .MaximumLength(250).WithMessage("Address length must be less than or equal to 250");
    }
}