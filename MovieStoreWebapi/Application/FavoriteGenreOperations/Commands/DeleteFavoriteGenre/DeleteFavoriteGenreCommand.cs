using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre
{
    public class DeleteFavoriteGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int Id;
        public DeleteFavoriteGenreCommand(IMovieStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            

            FavoriteGenre favoriteGenre = _dbContext.FavoritesGenres.SingleOrDefault(s => s.Id == Id);

            if (favoriteGenre is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı.");
            
            _dbContext.FavoritesGenres.Remove(favoriteGenre);
            _dbContext.SaveChanges();
        }
    }
}