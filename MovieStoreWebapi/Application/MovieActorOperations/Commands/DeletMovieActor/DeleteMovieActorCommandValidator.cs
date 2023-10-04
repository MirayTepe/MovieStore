using FluentValidation;


namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommandValidator : AbstractValidator<DeleteMovieActorCommand>
    {
        public DeleteMovieActorCommandValidator()
        {            
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}