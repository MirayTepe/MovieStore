using MicrosoftWebApi.DbOprations;

namespace MovieStoreWebapi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;

        public DeleteGenreCommand(IMovieStoreDbContext movieStoreDbContext)
        {
            _movieStoreDbContext = movieStoreDbContext;
        }

        public void Handle()
        {
            var genre = _movieStoreDbContext.Genres.SingleOrDefault(m => m.Id == GenreId);

            if (genre == null)
                throw new InvalidOperationException("Film türü bulunamadı!");

            _movieStoreDbContext.Genres.Remove(genre);
            _movieStoreDbContext.SaveChanges();



        }

    }
}