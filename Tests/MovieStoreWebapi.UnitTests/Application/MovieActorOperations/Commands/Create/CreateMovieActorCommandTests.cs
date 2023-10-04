using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieActorOperations.Commands.Create
{
    public class CreateActorMoviesCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateActorMoviesCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistActorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieActorViewModel model = new CreateMovieActorViewModel() { ActorId = 0, MovieId = 1};

            //act
            CreateMovieActorCommand command = new CreateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Oyuncu bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieActorViewModel model = new CreateMovieActorViewModel() { ActorId = 1, MovieId = 0};

            //act
            CreateMovieActorCommand command = new CreateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenAlreadyExistMovieAndActorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateMovieActorViewModel model = new CreateMovieActorViewModel() { ActorId = 1, MovieId = 1};

            //act
            CreateMovieActorCommand command = new CreateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Oyuncunun bu filmi daha önceden eklenmiş!");
        }

        [Fact]
        public void WhenNotExistMovieAndActorIdIsGiven_Create_ShouldBeCreateMovieActor()
        {
            // arrange
            CreateMovieActorViewModel model = new CreateMovieActorViewModel() { ActorId = 3, MovieId = 4};
            CreateMovieActorCommand command = new CreateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movieActors = _dbContext.MovieActors.SingleOrDefault(s => s.ActorId == model.ActorId && s.MovieId == model.MovieId);
            
            movieActors.Should().NotBeNull();
            movieActors.ActorId.Should().Be(model.ActorId);
            movieActors.MovieId.Should().Be(model.MovieId);
        }
    }
}