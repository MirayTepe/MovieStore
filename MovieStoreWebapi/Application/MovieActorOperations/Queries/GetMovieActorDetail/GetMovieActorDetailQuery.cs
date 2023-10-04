using System.Diagnostics.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail
{
    public class GetMovieActorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieActorDetailViewModel Model;
        public int Id;
    
        public GetMovieActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public GetMovieActorDetailViewModel Handle()
        {
             Actor actors = _dbContext.Actors.Include(i=> i.ActorMovies).ThenInclude(t=> t.Movie).Where(x=> x.Id == Id).SingleOrDefault();

              
            if(actors is null)
                throw new InvalidOperationException("Oyuncu bulunamadı.");

            GetMovieActorDetailViewModel vm = _mapper.Map<GetMovieActorDetailViewModel>(actors);

            return vm;
        }
    }

    public class GetMovieActorDetailViewModel
    {
        public int Id{get; set;}
        public string FirstName { get; set; }
        public string LastName{get;set;}
        public IReadOnlyList<string> Movies { get; set; }
        public IReadOnlyList<int> MovieActorIds { get; set; }

    }
}