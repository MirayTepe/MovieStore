using FluentValidation;

namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.CreateMovieGenre
{
    public class CreateMovieGenreCommandValidator : AbstractValidator<CreateMovieGenreCommand>
    {
        public CreateMovieGenreCommandValidator()
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.MovieId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}