using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;


namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Delete
{
    public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieId)
        {
            //arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            
            command.MovieId = movieId;

            //act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int movieId)
        {
            //arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            
            command.MovieId = movieId;

            //act
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}