using FluentValidation;


namespace MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail
{
    public class GetMovieActorDetailQueryValidator : AbstractValidator<GetMovieActorDetailQuery>
    {
        public GetMovieActorDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).NotEmpty().NotNull();
        }
    }
}