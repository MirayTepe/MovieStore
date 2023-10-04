using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class MovieActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context)
        {
           context.MovieActors.AddRange(
                    new MovieActor { MovieId = 1, ActorId = 1 },
                    new MovieActor { MovieId = 1, ActorId = 4 }
            );
                   
                         

        }
    }
}