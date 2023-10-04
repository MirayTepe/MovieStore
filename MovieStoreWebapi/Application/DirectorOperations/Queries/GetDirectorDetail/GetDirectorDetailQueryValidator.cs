using FluentValidation;

namespace MovieStoreWebapi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
    {
        public GetDirectorDetailQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0).NotNull().NotEmpty();
        }
    }
}