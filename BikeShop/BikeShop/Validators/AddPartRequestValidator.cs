using BikeShop.DTO.Requests;
using FluentValidation;

namespace BikeShop.Validators
{
    public class AddPartRequestValidator : AbstractValidator<AddPartRequest>
    {
        public AddPartRequestValidator()
        {
            RuleFor(part => part.partName)
                .NotEmpty()
                .WithMessage("Part name is required.")
                .Length(2, 50)
                .WithMessage("Part name must be between 2 and 50 characters.");

            RuleFor(part => part.partSpec)
                .NotEmpty()
                .WithMessage("Part specification is required.")
                .Length(1, 100)
                .WithMessage("Part specification must be between 1 and 100 characters.");
        }
    }
}
