using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenres
{
    public class GetFavoriteGenresQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetFavoriteGenresQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetFavoriteGenresViewModel> Handle()
        {
            var favoriteGenre = _dbContext.FavoritesGenres.Include(i=> i.
            Genre).Include(x=>x.Customer).OrderBy(x=> x.Id).ToList();
            List<GetFavoriteGenresViewModel> vm = _mapper.Map<List<GetFavoriteGenresViewModel>>(favoriteGenre);

            return vm;
        }
    }

    public class GetFavoriteGenresViewModel
    {
        public int Id{get;set;}
        public string Customer{get;set;}
        public string Genre { get; set; }
   
    }
}
