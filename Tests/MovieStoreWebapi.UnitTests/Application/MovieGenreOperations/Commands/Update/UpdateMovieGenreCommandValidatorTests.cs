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

namespace MovieStoreWebapi.UnitTests.Application.MovieActorOperations.Commands.Update
{
    public class UpdateMovieGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1,0,0)]
        [InlineData(1,1,0)]
        [InlineData(1,0,1)]
        [InlineData(1,null,1)]
        [InlineData(1,1,null)]
        [InlineData(0,1,1)]
        [InlineData(null,1,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieGenreId ,int movieId, int genreId)
        {
            //arrange
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(null,null);
            command.Model = new UpdateMovieGenreViewModel()
            {
                MovieId = movieId,
                GenreId = genreId
            };
            command.Id = movieGenreId;

            //act
            UpdateMovieGenreCommandValidator validator = new UpdateMovieGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int movieGenreId ,int movieId, int genreId)
        {
            //arrange
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(null,null);
            command.Model = new UpdateMovieGenreViewModel()
            {
                MovieId = movieId,
                GenreId = genreId
            };
            command.Id = movieGenreId;

            //act
            UpdateMovieGenreCommandValidator validator = new UpdateMovieGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}