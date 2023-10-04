using AutoMapper;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.GenreOperations.Queries
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
       
        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public GetGenreDetailViewModel Handle() 
        {
            var genre = _movieStoreDbContext.Genres.SingleOrDefault(g => g.Id == GenreId);

            if (genre == null)
            {
                throw new InvalidOperationException("Film Türü bulunamadı!");
            }

            GetGenreDetailViewModel model = _mapper.Map<GetGenreDetailViewModel>(genre);
            
            return model;
            


        }
      

    }

    public class GetGenreDetailViewModel
    {
         public string Name { get; set; }
       

    }
}