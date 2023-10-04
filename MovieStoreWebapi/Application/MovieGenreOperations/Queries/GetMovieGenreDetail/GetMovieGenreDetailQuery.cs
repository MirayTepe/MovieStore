using System.Diagnostics.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail
{
    public class GetMovieGenreDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieGenreDetailViewModel Model;
        public int Id;
    
        public GetMovieGenreDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public GetMovieGenreDetailViewModel Handle()
        {
               Customer customer = _dbContext.Customers.Include(i => i.FavoriteGenres).SingleOrDefault(s => s.Id == Id);
                if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");

              GetMovieGenreDetailViewModel vm = _mapper.Map<GetMovieGenreDetailViewModel>(customer);


         

            return vm;
        }
    }

    public class GetMovieGenreDetailViewModel
    {
        public int Id{get; set;}
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IReadOnlyList<string> Genres { get; set; }
        public IReadOnlyList<string> MovieGenreIds { get; set; }

    }
}