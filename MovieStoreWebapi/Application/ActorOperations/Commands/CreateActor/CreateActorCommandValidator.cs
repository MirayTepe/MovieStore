using FluentValidation;


namespace MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);

        }
    }
}