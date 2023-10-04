using System.Diagnostics.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail
{
    public class GetFavoriteGenreDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetFavoriteGenreDetailViewModel Model;
        public int Id;
    
        public GetFavoriteGenreDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public GetFavoriteGenreDetailViewModel Handle()
        {
               FavoriteGenre favoriteGenre = _dbContext.FavoritesGenres.Include(i => i.Customer).Include(i=>i.Genre).SingleOrDefault(s => s.Id == Id);
               
                if (favoriteGenre is null)
                  throw new InvalidOperationException("Favori film türü bulunamadı!");

              GetFavoriteGenreDetailViewModel vm = _mapper.Map<GetFavoriteGenreDetailViewModel>(favoriteGenre);


         

            return vm;
        }
    }

    public class GetFavoriteGenreDetailViewModel
    {
        public int Id{get;set;}
        public string Genre { get; set; }

        public string Customer { get; set; }


    }
}