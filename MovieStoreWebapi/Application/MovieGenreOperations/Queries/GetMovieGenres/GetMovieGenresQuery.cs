using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenres
{
    public class GetMovieGenresQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieGenresQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetMovieGenresViewModel> Handle()
        {
             List<Customer> movieGenre = _dbContext.Customers.Include(i => i.FavoriteGenres).OrderBy(x => x.Id).ToList();
            List<GetMovieGenresViewModel> vm = _mapper.Map<List<GetMovieGenresViewModel>>(movieGenre);

            return vm;
        }
    }

    public class GetMovieGenresViewModel
    {
        public int Id{get;set;}
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IReadOnlyList<string> Genres { get; set; }

        public IReadOnlyList<string> MovieGenreIds { get; set; }

    }
}
