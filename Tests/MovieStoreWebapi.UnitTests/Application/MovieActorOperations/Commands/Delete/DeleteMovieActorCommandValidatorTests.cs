using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieActorOperations.Commands.Delete
{
    public class DeleteMovieActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
        [InlineData(-1)]
        [InlineData(0)]
         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieActorId)
        {
            //arrange
            DeleteMovieActorCommand command = new DeleteMovieActorCommand(null);
            
            command.Id = movieActorId;

            //act
            DeleteMovieActorCommandValidator validator = new DeleteMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int movieActorId)
        {
            //arrange
            DeleteMovieActorCommand command = new DeleteMovieActorCommand(null);
            
            command.Id = movieActorId;

            //act
            DeleteMovieActorCommandValidator validator = new DeleteMovieActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}