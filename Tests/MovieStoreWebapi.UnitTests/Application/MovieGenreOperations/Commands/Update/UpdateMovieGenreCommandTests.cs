using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.UpdateMovieGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Commands.Update
{
    public class UpdateMovieGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateMovieGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieGenreViewModel model = new UpdateMovieGenreViewModel() { GenreId = 0, MovieId = 1};

            //act
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieGenreViewModel model = new UpdateMovieGenreViewModel() { GenreId = 1, MovieId = 0};

            //act
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(_dbContext, _mapper);
            UpdateMovieGenreViewModel model = new UpdateMovieGenreViewModel() { GenreId = 1, MovieId = 1};
            command.Model = model;           
             
            //act
            command.Id = 0;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieAndGenreIdIsGiven_Update_ShouldBeUpdateMovieGenre()
        {
            // arrange
            UpdateMovieGenreViewModel model = new UpdateMovieGenreViewModel() { GenreId = 3, MovieId = 4};
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(_dbContext, _mapper);
            command.Model = model;
            command.Id = 1;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movieGenre = _dbContext.MovieGenres.SingleOrDefault(s => s.Id == 1);
            
            movieGenre.Should().NotBeNull();
            movieGenre.GenreId.Should().Be(model.GenreId);
            movieGenre.MovieId.Should().Be(model.MovieId);
        }

    }
}