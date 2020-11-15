using FluentValidation;
using LearnEveryDay.Contracts.v1.Requests;

namespace LearnEveryDay.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username or password is incorrect.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Username or password is incorrect.");
        }
    }
}