using FluentValidation;


namespace MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(m => m.Model.Title).NotEmpty().MinimumLength(2);
            RuleFor(m => m.Model.Price).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(m => m.Model.Year).NotEmpty().MinimumLength(4).MaximumLength(4);
          
        }
    }
}