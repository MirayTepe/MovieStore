using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}