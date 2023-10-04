using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
           context.Actors.AddRange(
                    new Actor { FirstName = "Keanu", LastName = "Reeves",     IsActive = true },
                    new Actor { FirstName = "Chad", LastName = "Stahelski",   IsActive = true },
                    new Actor { FirstName = "Bridget", LastName = "Moynahan", IsActive = true },
                    new Actor { FirstName = "lan", LastName = "McShane",      IsActive = true },
                    new Actor { FirstName = "Steve", LastName = "Carell",     IsActive = true },
                    new Actor { FirstName = "Alan", LastName = "Arkin",       IsActive = true }
            );
        }
    }
}