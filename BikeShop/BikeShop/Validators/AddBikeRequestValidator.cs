using BikeShop.DTO.Requests;
using FluentValidation;

namespace BikeShop.Validators
{
    public class AddBikeRequestValidator : AbstractValidator<AddBikeRequest>
    {
        public AddBikeRequestValidator()
        {
            RuleFor(bike => bike.brand)
                .NotEmpty()
                .WithMessage("Brand is required.")
                .Length(2, 50)
                .WithMessage("Brand must be between 2 and 50 characters.")
                .Matches(@"^[0-9a-zA-Z ]+$")
                .WithMessage("Numbers and letters only please."); 

            RuleFor(bike => bike.model)
                .NotEmpty()
                .WithMessage("Model is required.")
                .Length(2, 50)
                .WithMessage("Model must be between 2 and 50 characters.")
                .Matches(@"^[0-9a-zA-Z ]+$")
                .WithMessage("Numbers and letters only please.");

            RuleFor(bike => bike.price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(10000)
                .WithMessage("Price must not exceed 10,000."); 

            RuleFor(bike => bike.availabilityInStore)
                .GreaterThan(0)
                .WithMessage("Availability in store must be 0 or greater.")
                .LessThanOrEqualTo(100)
                .WithMessage("Availability cannot exceed 100 units.");
        }
    }
}
