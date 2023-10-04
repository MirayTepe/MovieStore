using System.Diagnostics.Contracts;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor
{
    public class UpdateMovieActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateMovieActorViewModel Model;
        public int Id;
        public UpdateMovieActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            Actor actor = _dbContext.Actors.SingleOrDefault(s => s.Id == Model.ActorId);
            Movie movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);
            MovieActor movieActor = _dbContext.MovieActors.SingleOrDefault(s => s.Id == Id);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı");
            else if (movies is null)
                throw new InvalidOperationException("Film bulunamadı");
            else if (movieActor is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı.");

            movieActor.ActorId = Model.ActorId == default ? movieActor.ActorId : Model.ActorId;
            movieActor.MovieId = Model.MovieId == default ? movieActor.MovieId : Model.MovieId;

            _dbContext.MovieActors.Update(movieActor);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateMovieActorViewModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}