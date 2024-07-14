using DiscussionZone.Application.Features.Command.Category.Create;
using FluentValidation;

namespace DiscussionZone.Application.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCategoryCommandRequest)} is not empty")
                .NotNull()
                .WithMessage($"{nameof(CreateCategoryCommandRequest)} is not null")
                .MinimumLength(2)
                .WithMessage("must be at least 2 characters");
        }
    }
}
