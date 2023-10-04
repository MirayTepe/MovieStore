using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie{

    public class CreateMovieCommand
    {
        public CreateMovieViewModel Model { get; set; }
   
        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public void Handle() 
        {
            var movie = _movieStoreDbContext.Movies.SingleOrDefault(x => x.Title.Trim().ToLower() == Model.Title.Trim().ToLower());

            if (movie is not null)
                throw new InvalidOperationException("Film zaten mevcut!");

            movie = _mapper.Map<Movie>(Model);

            _movieStoreDbContext.Movies.Add(movie);
            _movieStoreDbContext.SaveChanges();
        }

    }

    public class CreateMovieViewModel 
    {
        public string Title { get; set; }
        public string Year { get; set; }
    
        public int DirectorId { get; set; }    
        public decimal Price { get; set; }
    }
}