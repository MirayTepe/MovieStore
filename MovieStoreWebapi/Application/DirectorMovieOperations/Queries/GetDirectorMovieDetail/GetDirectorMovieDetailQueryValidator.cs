using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail
{
    public class GetDirectorMovieDetailQueryValidator : AbstractValidator<GetDirectorMovieDetailQuery>
    {
        public GetDirectorMovieDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}