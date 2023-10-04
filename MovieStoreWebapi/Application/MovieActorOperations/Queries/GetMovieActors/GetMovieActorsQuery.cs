using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActor
{
    public class GetMovieActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieActorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetMovieActorsViewModel> Handle()
        {
            var actors = _dbContext.Actors.Include(i=> i.ActorMovies).ThenInclude(t=> t.Movie).OrderBy(x=> x.Id).ToList();
            List<GetMovieActorsViewModel> vm = _mapper.Map<List<GetMovieActorsViewModel>>(actors);

            return vm;
        }
    }

    public class GetMovieActorsViewModel
    {
        public int Id{get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IReadOnlyList<string> Movies { get; set; }

         public IReadOnlyList<int> MovieActorIds { get; set; }
    }
}
