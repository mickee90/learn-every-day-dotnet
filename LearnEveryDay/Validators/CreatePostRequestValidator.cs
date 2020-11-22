using System;
using FluentValidation;
using LearnEveryDay.Contracts.v1.Requests;

namespace LearnEveryDay.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Ingress).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(255);
            RuleFor(x => x.PublishedDate).NotEmpty().Must(BeAValidDate).WithMessage("Published date is required");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}