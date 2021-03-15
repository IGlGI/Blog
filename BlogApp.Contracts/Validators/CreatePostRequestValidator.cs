using FluentValidation;

namespace BlogApp.Contracts.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<PostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Title).NotNull().MinimumLength(2);
            RuleFor(x => x.Text).NotNull().MinimumLength(10);
            RuleFor(x => x.AuthorName).NotNull().MinimumLength(2);
        }
    }
}