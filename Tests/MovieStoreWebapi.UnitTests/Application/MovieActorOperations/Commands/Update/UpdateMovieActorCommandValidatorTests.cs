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
    public class UpdateMovieActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1,0,0)]
        [InlineData(1,1,0)]
        [InlineData(1,0,1)]
        [InlineData(1,null,1)]
        [InlineData(1,1,null)]
        [InlineData(0,1,1)]
        [InlineData(null,1,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int actorMovieId ,int movieId, int actorId)
        {
            //arrange
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(null,null);
            command.Model = new UpdateMovieActorViewModel()
            {
                MovieId = movieId,
                ActorId = actorId
            };
            command.Id = actorMovieId;

            //act
            UpdateMovieActorCommandValidator validator = new UpdateMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int actorMovieId ,int movieId, int actorId)
        {
            //arrange
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(null,null);
            command.Model = new UpdateMovieActorViewModel()
            {
                MovieId = movieId,
                ActorId = actorId
            };
            command.Id = actorMovieId;

            //act
            UpdateMovieActorCommandValidator validator = new UpdateMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}