using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(1).NotNull().NotEmpty();
            RuleFor(command => command.Model.LastName).MinimumLength(1).NotNull().NotEmpty();
            RuleFor(command => command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}