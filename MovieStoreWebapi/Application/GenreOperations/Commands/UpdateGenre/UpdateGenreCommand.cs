using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {

        public UpdateGenreViewModel Model { get; set; }
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _movieStoreDbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

            if (genre == null)
            {
                throw new InvalidOperationException("Film türü bulunamadı!");
            }


            _mapper.Map(Model, genre);

           
            _movieStoreDbContext.SaveChanges();
        }
    }

    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
      
    }
}