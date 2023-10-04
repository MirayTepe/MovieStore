using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int MovieId;

        public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetMovieDetailQueryViewModel Handle()
        {
            Movie movies = _context.Movies.Include(m => m.Director)
           .Where(x => x.Id == MovieId).SingleOrDefault();
            if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
        

            GetMovieDetailQueryViewModel vm = _mapper.Map<GetMovieDetailQueryViewModel>(movies);

            return vm;
        }
    }

    public class GetMovieDetailQueryViewModel
    {
        public string Title { get; set; }
        public string Year { get; set; }      
        public string Director { get; set; }
        public decimal Price { get; set; }
    }
}