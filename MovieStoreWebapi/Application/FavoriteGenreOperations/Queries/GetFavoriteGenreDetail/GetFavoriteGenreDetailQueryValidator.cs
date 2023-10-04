using FluentValidation;

namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail
{
    public class GetFavoriteGenreDetailQueryValidator : AbstractValidator<GetFavoriteGenreDetailQuery>
    {
        public GetFavoriteGenreDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}