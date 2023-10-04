using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class DirectorMovies
    {
        public static void AddDirectorMovies(this MovieStoreDbContext context)
        {
            context.DirectorMovies.AddRange(
                new DirectorMovie { MovieId = 1, DirectorId = 1 },
                new DirectorMovie { MovieId = 2,  DirectorId = 4 }
            

            );
        }
    }
}