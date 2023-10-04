using FluentValidation;

namespace MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty().NotNull();
            RuleFor(command => command.Model.LastName).NotEmpty().NotNull();
        }
    }
}