using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.DeleteDirectorMovie
{
    public class DeleteDirectorMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int Id;
        public DeleteDirectorMovieCommand(IMovieStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            

            DirectorMovie directorMovie = _dbContext.DirectorMovies.SingleOrDefault(s => s.Id == Id);

            if (directorMovie is null)
                throw new InvalidOperationException("İlgili kayda ait veri bulunamadı.");
            
            _dbContext.DirectorMovies.Remove(directorMovie);
            _dbContext.SaveChanges();
        }
    }
}