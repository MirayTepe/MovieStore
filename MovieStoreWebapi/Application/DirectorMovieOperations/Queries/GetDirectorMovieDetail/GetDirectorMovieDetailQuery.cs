using System.Diagnostics.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail
{
    public class GetDirectorMovieDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetDirectorMovieDetailViewModel Model;
        public int Id;
    
        public GetDirectorMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public GetDirectorMovieDetailViewModel Handle()
        {
               DirectorMovie directorMovie = _dbContext.DirectorMovies.Include(i => i.Director).Include(i=>i.Movie).SingleOrDefault(s => s.Id == Id);

                if (directorMovie is null)
                throw new InvalidOperationException("Yönetmenin film kaydı bulunamadı!");

              GetDirectorMovieDetailViewModel vm = _mapper.Map<GetDirectorMovieDetailViewModel>(directorMovie);


         

            return vm;
        }
    }

    public class GetDirectorMovieDetailViewModel
    {
        public int Id{get;set;}
        public string Director { get; set; }

        public string Movie { get; set; }


    }
}