using System.Diagnostics.Contracts;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie
{
    public class UpdateDirectorMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateDirectorMovieViewModel Model;
        public int Id;
        public UpdateDirectorMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            Director director = _dbContext.Directors.SingleOrDefault(s => s.Id == Model.DirectorId);
            Movie movie = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            DirectorMovie directorMovie = _dbContext.DirectorMovies.SingleOrDefault(s => s.Id == Id);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            else if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if (directorMovie is null)
                throw new InvalidOperationException("Yönetmenin bu filmi zaten eklenmiş!");

            directorMovie.DirectorId = Model.DirectorId == default ? directorMovie.DirectorId : Model.DirectorId;
            directorMovie.MovieId = Model.MovieId == default ? directorMovie.MovieId : Model.MovieId;

            _dbContext.DirectorMovies.Update(directorMovie);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateDirectorMovieViewModel
    {
        public int DirectorId { get; set; }
        public int MovieId { get; set; }
    }
}