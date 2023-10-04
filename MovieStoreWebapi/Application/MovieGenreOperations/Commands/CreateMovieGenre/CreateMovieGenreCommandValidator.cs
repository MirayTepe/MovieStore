using FluentValidation;

namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre
{
    public class CreateFavoriteGenreCommandValidator : AbstractValidator<CreateFavoriteGenreCommand>
    {
        public CreateFavoriteGenreCommandValidator()
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.CustomerId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}