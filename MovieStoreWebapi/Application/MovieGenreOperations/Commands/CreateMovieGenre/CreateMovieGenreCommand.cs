using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.CreateMovieGenre
{
    public class CreateMovieGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieGenreViewModel Model;
        public CreateMovieGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Actors.SingleOrDefault(s => s.Id == Model.GenreId);
            var movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            var actorMovies = _dbContext.MovieActors.SingleOrDefault(s => s.ActorId == Model.GenreId && s.MovieId == Model.MovieId);

            if (genre is null)
                throw new InvalidOperationException("Tür bulunamadı!");
            else if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if(actorMovies is not null)
                throw new InvalidOperationException("Favori film türü zaten eklenmiş!");

             MovieGenre result = _mapper.Map<MovieGenre>(Model);

            _dbContext.MovieGenres.Add(result);
            _dbContext.SaveChanges();
        }
    }

    public class CreateMovieGenreViewModel
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}