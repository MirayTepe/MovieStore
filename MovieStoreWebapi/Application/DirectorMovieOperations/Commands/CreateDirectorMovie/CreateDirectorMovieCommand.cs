using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie
{
    public class CreateDirectorMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorMovieViewModel Model;
        public CreateDirectorMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(s => s.Id == Model.DirectorId);
            var movie = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            var directorMovie = _dbContext.DirectorMovies.SingleOrDefault(s => s.DirectorId == Model.DirectorId && s.MovieId == Model.MovieId);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            else if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if(directorMovie is not null)
                throw new InvalidOperationException("Yönetmenin bu filmi zaten eklenmiş!");

             DirectorMovie result = _mapper.Map<DirectorMovie>(Model);

            _dbContext.DirectorMovies.Add(result);
            _dbContext.SaveChanges();
        }
    }

    public class CreateDirectorMovieViewModel
    {
        public int DirectorId { get; set; }
        public int MovieId { get; set; }
    }
}