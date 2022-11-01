using FluentValidation;
using FurnitureShop.Backend.Common.DTOs.Furniture;

namespace FurnitureShop.Backend.BL.Validations.Furniture;

public class AddFurnitureValidator : AbstractValidator<AddFurnitureRequest>
{
    public AddFurnitureValidator()
    {
        RuleFor(f => f.Name)
            .MinimumLength(3).WithMessage("Name length must be greater than or equal to 3")
            .MaximumLength(50).WithMessage("Name length must be less than or equal to 50");
        
        RuleFor(f => f.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}