using Microsoft.EntityFrameworkCore;
using MovieStoreWebapi.Entities;
using Microsoft.AspNetCore.Identity;



namespace MovieStoreWebapi.DbOprations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                context.Directors.AddRange(
                    new Director { FirstName = "Chad", LastName = "Stahelski",   IsActive = true },
                    new Director { FirstName = "Kyle", LastName = "Balda",       IsActive = true },
                    new Director { FirstName = "Jonathan", LastName = "Del Val", IsActive = true }
                );
                
            

                context.Actors.AddRange(
                    new Actor { FirstName = "Keanu", LastName = "Reeves",     IsActive = true },
                    new Actor { FirstName = "Chad", LastName = "Stahelski",   IsActive = true },
                    new Actor { FirstName = "Bridget", LastName = "Moynahan", IsActive = true },
                    new Actor { FirstName = "lan", LastName = "McShane",      IsActive = true },
                    new Actor { FirstName = "Steve", LastName = "Carell",     IsActive = true },
                    new Actor { FirstName = "Alan", LastName = "Arkin",       IsActive = true }
                );

                context.MovieGenres.AddRange(
                    new MovieGenre { MovieId = 1, GenreId = 4 },
                    new MovieGenre { MovieId = 1, GenreId = 5 },
                    new MovieGenre { MovieId = 1, GenreId = 6 }
                );

                context.MovieActors.AddRange(
                    new MovieActor { MovieId = 1, ActorId = 1 },
                    new MovieActor { MovieId = 1, ActorId = 4 }
                );
                context.DirectorMovies.AddRange(
                    new DirectorMovie { MovieId = 1, DirectorId = 1 },
                    new DirectorMovie { MovieId = 2,  DirectorId = 4 }
                );
                                

                
               

                context.Movies.AddRange(

                    new Movie
                    {
                        // ID = 1,
                    
                        Title = "John Wick",
                        Year = "2014",
                        DirectorId=1,
                        Price = 50,
                        IsActive = true

                    },

                    new Movie
                    {
                        // ID = 2,
                    
                        Title = "Minyonlar 2: Gru'nun Yükselişi",
                        Year = "2022",
                        DirectorId=2,
                        Price = 45,
                        IsActive = true

                    }

                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Aksiyon "
                    },
                    new Genre
                    {
                        Name = "Bilimkurgu "
                    },
                    new Genre
                    {
                        Name = "Animasyon "
                    },
                    new Genre
                    {
                        Name = "Aksiyon "
                    },
                    new Genre
                    {
                        Name = "Suç "
                    },
                    new Genre
                    {
                        Name = "Gerilim "
                    },
                    new Genre
                    {
                        Name = "Komedi "
                    }
                );
            

                context.Customers.AddRange(
                    new Customer
                    {
                        FirstName = "Ayşe",
                        LastName = "Taş",
                        Email = "ayse95@gmail.com",
                        Password = "123456",
                        IsActive = true

                    },
                    new Customer
                    {
                        FirstName = "fatma",
                        LastName = "Kaya",
                        Email = "kaya@gmail.com",
                        Password = "123456",
                        IsActive = true

                    },
                    new Customer
                    {
                        FirstName = "Demir",
                        LastName = "Yıldırım",
                        Email = "demir@gmail.com",
                        Password = "123456",
                        IsActive = true

                    }
                );


              

                context.Orders.AddRange(
                    new Order { CustomerId = 1 , MovieId = 1, PurchaseDate = new DateTime(2022, 07, 06) , IsActive = true },
                    new Order { CustomerId = 2 , MovieId = 1, PurchaseDate = new DateTime(2022, 12, 05) , IsActive = true },
                    new Order { CustomerId = 3 , MovieId = 2, PurchaseDate = new DateTime(2022, 08, 25) , IsActive = true }
                );

                context.FavoritesGenres.AddRange(
                    new FavoriteGenre { CustomerId = 1 , GenreId = 1},
                    new FavoriteGenre { CustomerId = 1 , GenreId = 2},
                    new FavoriteGenre { CustomerId = 2 , GenreId = 3},
                    new FavoriteGenre { CustomerId = 2 , GenreId = 4},
                    new FavoriteGenre { CustomerId = 3 , GenreId = 5},
                    new FavoriteGenre { CustomerId = 2 , GenreId = 6}
                );

                context.SaveChanges();

            }
        }

    }
}