using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(1).NotNull().NotEmpty();
            RuleFor(command => command.Model.LastName).MinimumLength(1).NotNull().NotEmpty();
        }
    }
}