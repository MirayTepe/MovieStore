using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie
{
    public class CreateDirectorMovieCommandValidator : AbstractValidator<CreateDirectorMovieCommand>
    {
        public CreateDirectorMovieCommandValidator()
        {
            RuleFor(command=> command.Model.DirectorId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.MovieId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}