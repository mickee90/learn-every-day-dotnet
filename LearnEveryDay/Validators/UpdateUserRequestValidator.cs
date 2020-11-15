using FluentValidation;
using LearnEveryDay.Contracts.v1.Requests;

namespace LearnEveryDay.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().EmailAddress();
            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255);
            
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(255);
            
            RuleFor(x => x.Address).MaximumLength(255);
            
            RuleFor(x => x.ZipCode).MaximumLength(255);
            
            RuleFor(x => x.City).MaximumLength(255);
            
            // @todo add phone validation
            RuleFor(x => x.Phone).MaximumLength(255);
        }
    }
}