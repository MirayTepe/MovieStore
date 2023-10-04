using FluentValidation;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.UpdateMovieGenre
{
    public class UpdateMovieGenreCommandValidator : AbstractValidator<UpdateMovieGenreCommand>
    {
        public UpdateMovieGenreCommandValidator()
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.MovieId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}