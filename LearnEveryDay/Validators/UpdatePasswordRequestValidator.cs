using FluentValidation;
using LearnEveryDay.Contracts.v1.Requests;

namespace LearnEveryDay.Validators
{
    public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordRequestValidator()
        {
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Equal(x => x.Password)
                .WithMessage("The passwords do not match.");
        }
    }
}