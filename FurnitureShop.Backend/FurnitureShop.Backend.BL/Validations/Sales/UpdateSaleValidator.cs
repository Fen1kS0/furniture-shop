using FluentValidation;
using FurnitureShop.Backend.Common.DTOs.Sales;

namespace FurnitureShop.Backend.BL.Validations.Sales;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleValidator()
    {
        RuleFor(s => s.Count)
            .GreaterThan(0).WithMessage("Count must be greater than zero");
    }
}