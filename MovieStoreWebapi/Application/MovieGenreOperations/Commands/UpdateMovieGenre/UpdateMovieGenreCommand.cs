using System.Diagnostics.Contracts;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Commands.UpdateMovieGenre
{
    public class UpdateMovieGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateMovieGenreViewModel Model;
        public int Id;
        public UpdateMovieGenreCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            Genre genre = _dbContext.Genres.SingleOrDefault(s => s.Id == Model.GenreId);
            Movie movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            MovieGenre movieGenre = _dbContext.MovieGenres.SingleOrDefault(s => s.Id == Id);

            if (genre is null)
                throw new InvalidOperationException("Film türü bulunamadı!");
            else if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if (movieGenre is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı!");

            movieGenre.GenreId = Model.GenreId == default ? movieGenre.GenreId : Model.GenreId;
            movieGenre.MovieId = Model.MovieId == default ? movieGenre.MovieId : Model.MovieId;

            _dbContext.MovieGenres.Update(movieGenre);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateMovieGenreViewModel
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}