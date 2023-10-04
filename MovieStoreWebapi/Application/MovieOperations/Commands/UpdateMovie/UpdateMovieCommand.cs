using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public UpdateMovieViewModel Model { get; set; }
        public int MovieId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public UpdateMovieCommand(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public void Handle() 
        {
            var movie = _movieStoreDbContext.Movies.SingleOrDefault(x => x.Id== MovieId);

            if (movie == null) 
            {
                throw new InvalidOperationException("Film bulunamadı!");
            }
             
            // movie.DirectorId=Model.DirectorId!=default? Model.DirectorId:movie.DirectorId;
            // movie.Price=Model.Price!=default?Model.Price:movie.Price;
            // movie.Year=Model.Year!=default?Model.Year:movie.Year;
            // movie.Title=Model.Title!=default?Model.Title:movie.Title;
            _mapper.Map(Model,movie);
          
            _movieStoreDbContext.SaveChanges();
        }

    }

    public class UpdateMovieViewModel
    {
        public string Title { get; set; }
        public int DirectorId { get; set; }   
        public string Year { get; set; }
        
        public decimal Price { get; set; }

    }
}