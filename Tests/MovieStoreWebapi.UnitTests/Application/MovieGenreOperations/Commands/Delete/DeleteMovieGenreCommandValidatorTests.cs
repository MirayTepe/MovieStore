using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.DeleteMovieGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Commands.Delete
{
    public class DeleteMovieGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
        [InlineData(-1)]
        [InlineData(0)]
         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieGenreId)
        {
            //arrange
            DeleteMovieGenreCommand command = new DeleteMovieGenreCommand(null);
            
            command.Id = movieGenreId;

            //act
            DeleteMovieGenreCommandValidator validator = new DeleteMovieGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int movieGenreId)
        {
            //arrange
            DeleteMovieGenreCommand command = new DeleteMovieGenreCommand(null);
            
            command.Id = movieGenreId;

            //act
            DeleteMovieGenreCommandValidator validator = new DeleteMovieGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}