using FluentValidation;

namespace Api.Application.Commands.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);

        RuleFor(x => x.Description).MinimumLength(10);

        RuleFor(x => x.Price).GreaterThan(0);
    }
}
