using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;


namespace MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActor
{
    public class GetMovieActorsQuery2
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieActorsQuery2(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<GetMovieActorsViewModel2> Handle()
        {
            var movieActor2 = _dbContext.MovieActors.Include(i=> i.Actor).Include(x=>x.Movie).OrderBy(x=> x.Id).ToList();
            List<GetMovieActorsViewModel2> vm = _mapper.Map<List<GetMovieActorsViewModel2>>(movieActor2);

            return vm;
        }
    }

    public class GetMovieActorsViewModel2
    {
        public int Id{get;set;}
        public string Movie{ get; set; }
        public string Actor { get; set; }
   
    }
}
