using FluentValidation;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovieDetail;

namespace MicrosoftWebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}