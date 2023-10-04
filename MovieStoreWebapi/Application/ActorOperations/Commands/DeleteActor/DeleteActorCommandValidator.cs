using FluentValidation;


namespace MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
             RuleFor(command => command.ActorId).GreaterThan(0);
        }
    }
}