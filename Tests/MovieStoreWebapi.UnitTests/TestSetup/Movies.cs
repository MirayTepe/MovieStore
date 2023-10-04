using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
            context.Movies.AddRange(

                new Movie{Title = "John Wick",Year = "2014",DirectorId=1,Price = 50,IsActive = true},

                new Movie{Title = "Minyonlar 2: Gru'nun Yükselişi",Year = "2022",DirectorId=2,Price = 45,
                IsActive = true},

                new Movie{Title = "MovieTest1",Year = "2010",DirectorId=4,Price = 45,IsActive = true},
                new Movie{Title = "MovieTest2",Year = "2010",DirectorId=5,Price = 45,IsActive = true}
            );
        }
    }
}