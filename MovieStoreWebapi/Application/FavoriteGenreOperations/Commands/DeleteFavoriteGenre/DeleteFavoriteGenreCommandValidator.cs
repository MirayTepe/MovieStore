using FluentValidation;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre
{
    public class DeleteFavoriteGenreCommandValidator : AbstractValidator<DeleteFavoriteGenreCommand>
    {
        public DeleteFavoriteGenreCommandValidator()
        {            
            RuleFor(command=> command.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}