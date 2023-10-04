using System.Diagnostics.Contracts;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre
{
    public class UpdateFavoriteGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateFavoriteGenreViewModel Model;
        public int Id;
        public UpdateFavoriteGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            Genre genre = _dbContext.Genres.SingleOrDefault(s => s.Id == Model.GenreId);
            Customer customer = _dbContext.Customers.SingleOrDefault(s => s.Id == Model.CustomerId);
            FavoriteGenre favoriteGenre = _dbContext.FavoritesGenres.SingleOrDefault(s => s.Id == Id);

            if (genre is null)
                throw new InvalidOperationException("Tür bulunamadı!");
            else if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");
            else if (favoriteGenre is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı!");

            favoriteGenre.GenreId = Model.GenreId == default ? favoriteGenre.GenreId : Model.GenreId;
            favoriteGenre.CustomerId = Model.CustomerId == default ? favoriteGenre.CustomerId : Model.CustomerId;

            _dbContext.FavoritesGenres.Update(favoriteGenre);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateFavoriteGenreViewModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
    }
}