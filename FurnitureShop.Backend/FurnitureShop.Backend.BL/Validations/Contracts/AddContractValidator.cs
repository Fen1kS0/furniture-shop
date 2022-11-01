using FluentValidation;
using FurnitureShop.Backend.Common.DTOs.Contracts;

namespace FurnitureShop.Backend.BL.Validations.Contracts;

public class AddContractValidator : AbstractValidator<AddContractRequest>
{
    public AddContractValidator()
    {
        RuleFor(c => c.DueDate)
            .GreaterThan(c => c.IssueDate).WithMessage("The due date must be later than the issue date");
    }
}