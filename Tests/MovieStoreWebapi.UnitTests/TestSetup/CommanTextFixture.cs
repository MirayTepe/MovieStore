using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Common;
using MovieStoreWebapi.DbOprations;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDB").Options;
            Context = new MovieStoreDbContext(options);
               
            Context.Database.EnsureCreated();
            Context.AddActors();
            Context.AddDirectors();
            Context.AddGenres();
            Context.AddMovies();
            Context.AddCustomers();
            Context.AddMovieActors();
            Context.AddMovieGenres();
            Context.AddDirectorMovies();
            Context.AddFavoriteGenres();
            Context.AddOrders();
           
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }

}