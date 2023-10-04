using FluentValidation;


namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor
{
    public class UpdateMovieActorCommandValidator : AbstractValidator<UpdateMovieActorCommand>
    {
        public UpdateMovieActorCommandValidator()
        {            
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}