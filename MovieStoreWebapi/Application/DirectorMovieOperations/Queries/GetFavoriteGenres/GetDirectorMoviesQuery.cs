using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovies
{
    public class GetDirectorMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetDirectorMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetDirectorMoviesViewModel> Handle()
        {
            var directorMovies = _dbContext.DirectorMovies.Include(i=> i.
            Movie).Include(x=>x.Director).OrderBy(x=> x.Id).ToList();
            List<GetDirectorMoviesViewModel> vm = _mapper.Map<List<GetDirectorMoviesViewModel>>(directorMovies);

            return vm;
        }
    }

    public class GetDirectorMoviesViewModel
    {
        public int Id{get;set;}
        public string Director{get;set;}
        public string Movie { get; set; }
   
    }
}
