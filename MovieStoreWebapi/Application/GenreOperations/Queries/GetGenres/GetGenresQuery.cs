using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.GenreOperations.Queries
{
    public class GetGenresQuery
    {
        public GetGenresViewModel Model { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

         public List<GetGenresViewModel> Handle()
        {
            var genres = _movieStoreDbContext.Genres.ToList();
            return _mapper.Map<List<GetGenresViewModel>>(genres);
        }


    }

    public class GetGenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}