using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieActorViewModel Model;
        public CreateMovieActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(s => s.Id == Model.ActorId);
            var movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            var actorMovies = _dbContext.MovieActors.SingleOrDefault(s => s.ActorId == Model.ActorId && s.MovieId == Model.MovieId);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı!");
            else if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if(actorMovies is not null)
                throw new InvalidOperationException("Oyuncunun bu filmi daha önceden eklenmiş!");

             MovieActor result = _mapper.Map<MovieActor>(Model);

            _dbContext.MovieActors.Add(result);
            _dbContext.SaveChanges();
        }
    }

    public class CreateMovieActorViewModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}