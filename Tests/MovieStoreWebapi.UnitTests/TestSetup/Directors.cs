using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext context)
        {
            context.Directors.AddRange(
                new Director { FirstName = "Chad", LastName = "Stahelski",   IsActive = true },
                new Director { FirstName = "Kyle", LastName = "Balda",       IsActive = true },
                new Director { FirstName = "Jonathan",LastName = "Del Val", IsActive = true },
                new Director { FirstName = "TestDirectorName",LastName = "TestDirectorSurname", IsActive = true },
                new Director { FirstName = "TestDirectorName2",LastName = "TestDirectorSurname2", IsActive = true }
            );
                         

        }
    }
}