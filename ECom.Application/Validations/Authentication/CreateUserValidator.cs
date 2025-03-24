using ECom.Application.DTOs.IdentityDTO;
using FluentValidation;


namespace ECom.Application.Validations.Authentication
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password and ConfirmPassword do not match");
        }
    }
}
