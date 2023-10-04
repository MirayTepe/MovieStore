using FluentValidation;  

namespace MovieStoreWebapi.Application.GenreOperations.Queries.GetDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}