using FluentValidation;

namespace MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail
{
    public class GetMovieGenreDetailQueryValidator : AbstractValidator<GetMovieGenreDetailQuery>
    {
        public GetMovieGenreDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}