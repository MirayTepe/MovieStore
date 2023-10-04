using FluentValidation;



namespace MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(m => m.Model.Price).NotEmpty().GreaterThan(0);
            RuleFor(m => m.Model.Year).NotEmpty().MinimumLength(4).MaximumLength(4);
            RuleFor(m => m.Model.Title).NotEmpty().MinimumLength(2);
     
        }
    }
}