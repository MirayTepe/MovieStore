using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;



namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Create
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        
        [Fact]
        public void WhenMovieModelIsGiven_Create_ShouldBeCreateMovie()
        {
            // arrange
            CreateMovieViewModel model = new CreateMovieViewModel() { Title = "MovieTest3", DirectorId = 4,Price=20,Year="2023"};
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movie = _context.Movies.SingleOrDefault(s => s.DirectorId == model.DirectorId && s.Title == model.Title && s.Year == model.Year && s.Price == model.Price);
            
            movie.Should().NotBeNull();
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.Title.Should().Be(model.Title);
            movie.Year.Should().Be(model.Year);
            movie.Price.Should().Be(model.Price);
        }
    }
}

