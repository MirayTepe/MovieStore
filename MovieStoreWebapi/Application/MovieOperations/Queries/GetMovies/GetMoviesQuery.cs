using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;
using Newtonsoft.Json;


namespace MovieStoreWebapi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetMoviesQueryViewModel> Handle()
        {
            List<Movie> movies = _context.Movies
           .Include(m => m.Director).OrderBy(x => x.Id).ToList();                   
        
            
            return _mapper.Map<List<GetMoviesQueryViewModel>>(movies);
        }
    }

    public class GetMoviesQueryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Director { get; set; }
        public decimal Price { get; set; }
     
    }
}