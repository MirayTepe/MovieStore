using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie
{
    public class UpdateDirectorMovieCommandValidator : AbstractValidator<UpdateDirectorMovieCommand>
    {
        public UpdateDirectorMovieCommandValidator()
        {
            RuleFor(command=> command.Model.DirectorId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.MovieId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}