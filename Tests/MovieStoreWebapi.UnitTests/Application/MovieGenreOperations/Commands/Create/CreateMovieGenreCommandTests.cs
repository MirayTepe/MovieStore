using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.CreateMovieGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Commands.Create
{
    public class CreateMovieGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieGenreViewModel model = new CreateMovieGenreViewModel() { GenreId = 0, MovieId = 1};

            //act
            CreateMovieGenreCommand command = new CreateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieGenreViewModel model = new CreateMovieGenreViewModel() { GenreId = 1, MovieId = 0};

            //act
            CreateMovieGenreCommand command = new CreateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenAlreadyExistMovieAndGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieGenreViewModel model = new CreateMovieGenreViewModel() { GenreId = 1, MovieId = 1};

            //act
            CreateMovieGenreCommand command = new CreateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Favori film türü zaten eklenmiş!");
        }

        [Fact]
        public void WhenNotExistMovieAndGenreIdIsGiven_Create_ShouldBeCreateMovieGenre()
        {
            // arrange
            CreateMovieGenreViewModel model = new CreateMovieGenreViewModel() { GenreId = 3, MovieId = 4};
            CreateMovieGenreCommand command = new CreateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movieGenres = _dbContext.MovieGenres.SingleOrDefault(s => s.GenreId == model.GenreId && s.MovieId == model.MovieId);
            
            movieGenres.Should().NotBeNull();
            movieGenres.GenreId.Should().Be(model.GenreId);
            movieGenres.MovieId.Should().Be(model.MovieId);
        }
    }
}