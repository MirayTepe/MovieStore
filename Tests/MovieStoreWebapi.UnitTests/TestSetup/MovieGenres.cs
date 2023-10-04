using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class MovieGenres
    {
        public static void AddMovieGenres(this MovieStoreDbContext context)
        {
            context.MovieGenres.AddRange(
                    new MovieGenre { MovieId = 1, GenreId = 4 },
                    new MovieGenre { MovieId = 1, GenreId = 5 },
                    new MovieGenre { MovieId = 1, GenreId = 6 }
            );
                   
                         

        }
    }
}