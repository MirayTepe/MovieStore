using FluentValidation;

namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandValidator : AbstractValidator<CreateMovieActorCommand>
    {
        public CreateMovieActorCommandValidator()
        {
            RuleFor(command=> command.Model.ActorId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.MovieId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}