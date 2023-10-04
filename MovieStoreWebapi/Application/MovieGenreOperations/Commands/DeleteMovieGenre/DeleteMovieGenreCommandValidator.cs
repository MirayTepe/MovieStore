using FluentValidation;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.DeleteMovieGenre
{
    public class DeleteMovieGenreCommandValidator : AbstractValidator<DeleteMovieGenreCommand>
    {
        public DeleteMovieGenreCommandValidator()
        {            
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}