using FluentValidation;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre
{
    public class UpdateFavoriteGenreCommandValidator : AbstractValidator<UpdateFavoriteGenreCommand>
    {
        public UpdateFavoriteGenreCommandValidator()
        {
            RuleFor(command=> command.Model.GenreId).GreaterThan(0).NotNull().NotEmpty();
            RuleFor(command=> command.Model.CustomerId).GreaterThan(0).NotNull().NotEmpty();
      

        }
    }
}