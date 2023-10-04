using AutoMapper;
using MicrosoftWebApi.DbOprations;

namespace MovieStoreWebapi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        

        public DeleteMovieCommand(IMovieStoreDbContext movieStoreDbContext)
        {
            _movieStoreDbContext = movieStoreDbContext;
           
        }
        public void Handle() 
        {
            var movie = _movieStoreDbContext.Movies.SingleOrDefault(m => m.Id == MovieId);

            if (movie == null)
                throw new InvalidOperationException("Film bulunamadı!");

            _movieStoreDbContext.Movies.Remove(movie);
            _movieStoreDbContext.SaveChanges();



        }
    }
}