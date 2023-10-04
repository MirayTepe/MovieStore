using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre
{
    public class CreateFavoriteGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateFavoriteGenreViewModel Model;
        public CreateFavoriteGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == Model.GenreId);
            var customer = _dbContext.Customers.SingleOrDefault(s => s.Id == Model.CustomerId);
            var favoriteGenre = _dbContext.FavoritesGenres.SingleOrDefault(s => s.GenreId == Model.GenreId && s.CustomerId == Model.CustomerId);

            if (genre is null)
                throw new InvalidOperationException("Tür bulunamadı!");
            else if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");
            else if(favoriteGenre is not null)
                throw new InvalidOperationException("Favori film türü zaten eklenmiş!");

             FavoriteGenre result = _mapper.Map<FavoriteGenre>(Model);

            _dbContext.FavoritesGenres.Add(result);
            _dbContext.SaveChanges();
        }
    }

    public class CreateFavoriteGenreViewModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
    }
}