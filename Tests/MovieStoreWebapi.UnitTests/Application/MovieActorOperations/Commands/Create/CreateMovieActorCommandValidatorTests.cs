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
    public class CreateActorMoviesCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieId, int actorId)
        {
            //arrange
            CreateMovieActorCommand command = new CreateMovieActorCommand(null,null);
            command.Model = new CreateMovieActorViewModel()
            {
                MovieId = movieId,
                ActorId = actorId
            };

            //act
            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int movieId, int actorId)
        {
            //arrange
            CreateMovieActorCommand command = new CreateMovieActorCommand(null,null);
            command.Model = new CreateMovieActorViewModel()
            {
                MovieId = movieId,
                ActorId = actorId
            };

            //act
            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}