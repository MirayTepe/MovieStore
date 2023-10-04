using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.DeleteMovieGenre
{
    public class DeleteMovieGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int Id;
        public DeleteMovieGenreCommand(IMovieStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            

            MovieGenre movieGenre = _dbContext.MovieGenres.SingleOrDefault(s => s.Id == Id);

            if (movieGenre is null)
                throw new InvalidOperationException("Film türü bulunamadı!");
            
            _dbContext.MovieGenres.Remove(movieGenre);
            _dbContext.SaveChanges();
        }
    }
}