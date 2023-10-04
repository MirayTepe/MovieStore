using FluentValidation;


namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.DeleteDirectorMovie
{
    public class DeleteDirectorMovieCommandValidator : AbstractValidator<DeleteDirectorMovieCommand>
    {
        public DeleteDirectorMovieCommandValidator()
        {            
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}