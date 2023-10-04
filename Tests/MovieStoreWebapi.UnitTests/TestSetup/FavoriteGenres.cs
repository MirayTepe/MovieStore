using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class FavoriteGenres
    {
        public static void AddFavoriteGenres(this MovieStoreDbContext context)
        {
            context.FavoritesGenres.AddRange(
                new FavoriteGenre { CustomerId = 1 , GenreId = 1},
                new FavoriteGenre { CustomerId = 1 , GenreId = 2},
                new FavoriteGenre { CustomerId = 2 , GenreId = 3},
                new FavoriteGenre { CustomerId = 2 , GenreId = 4}

            );
                   
                         

        }
    }
}