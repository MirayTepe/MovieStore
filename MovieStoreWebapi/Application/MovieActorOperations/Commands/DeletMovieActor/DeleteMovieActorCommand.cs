using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int Id;
        public DeleteMovieActorCommand(IMovieStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            

            MovieActor movieActor = _dbContext.MovieActors.SingleOrDefault(s => s.Id == Id);

            if (movieActor is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı!");
            
            _dbContext.MovieActors.Remove(movieActor);
            _dbContext.SaveChanges();
        }
    }
}