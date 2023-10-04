using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieActorOperations.Commands.Update
{
    public class UpdateMovieActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateMovieActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistActorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieActorViewModel model = new UpdateMovieActorViewModel() { ActorId = 0, MovieId = 1};

            //act
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Oyuncu bulunamadı");
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieActorViewModel model = new UpdateMovieActorViewModel() { ActorId = 1, MovieId = 0};

            //act
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı");
        }

        [Fact]
        public void WhenNotExistMovieActorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(_dbContext, _mapper);
            UpdateMovieActorViewModel model = new UpdateMovieActorViewModel() { ActorId = 1, MovieId = 1};
            command.Model = model;           
             
            //act
            command.Id = 0;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı.");
        }

        [Fact]
        public void WhenNotExistMovieAndActorIdIsGiven_Update_ShouldBeUpdateMovieActor()
        {
            // arrange
            UpdateMovieActorViewModel model = new UpdateMovieActorViewModel() { ActorId = 3, MovieId = 4};
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(_dbContext, _mapper);
            command.Model = model;
            command.Id = 1;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movieActor = _dbContext.MovieActors.SingleOrDefault(s => s.Id == 1);
            
            movieActor.Should().NotBeNull();
            movieActor.ActorId.Should().Be(model.ActorId);
            movieActor.MovieId.Should().Be(model.MovieId);
        }

    }
}