using Microsoft.EntityFrameworkCore;
using MovieStoreWebapi.Entities;

namespace MicrosoftWebApi.DbOprations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Actor> Actors { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<MovieActor> MovieActors { get; set; }
        DbSet<MovieGenre> MovieGenres { get; set; }
        DbSet<DirectorMovie> DirectorMovies { get; set; }
        DbSet<FavoriteGenre> FavoritesGenres { get; set; }
        DbSet<Genre> Genres { get; set; }

        int SaveChanges();
    }
}