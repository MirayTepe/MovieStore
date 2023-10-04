using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenres
{
    public class GetMovieGenresQuery2
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieGenresQuery2(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetMovieGenresViewModel2> Handle()
        {
            var movieGenre2 = _dbContext.MovieGenres.Include(i=> i.
            Genre).Include(x=>x.Movie).OrderBy(x=> x.Id).ToList();
            List<GetMovieGenresViewModel2> vm = _mapper.Map<List<GetMovieGenresViewModel2>>(movieGenre2);

            return vm;
        }
    }

    public class GetMovieGenresViewModel2
    {
        public int Id{get;set;}
        public string Movie{get;set;}
        public string Genres { get; set; }
   
    }
}
