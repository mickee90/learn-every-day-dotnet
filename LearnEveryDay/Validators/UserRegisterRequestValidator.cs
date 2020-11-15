using FluentValidation;
using LearnEveryDay.Contracts.v1.Requests;

namespace LearnEveryDay.Validators
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Equal(x => x.Password)
                .WithMessage("The passwords do not match.");
        }
    }
}