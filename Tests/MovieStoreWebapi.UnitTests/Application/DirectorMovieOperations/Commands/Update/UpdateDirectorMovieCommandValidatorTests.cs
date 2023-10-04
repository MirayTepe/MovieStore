using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Commands.Update
{
    public class UpdateDirectorMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int movieId, int directorId)
        {
            //arrange
            UpdateDirectorMovieCommand command = new UpdateDirectorMovieCommand(null,null);
            command.Model = new UpdateDirectorMovieViewModel()
            {
                MovieId = movieId,
                DirectorId = directorId
            };

            //act
            UpdateDirectorMovieCommandValidator validator = new UpdateDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int movieId, int directorId)
        {
            //arrange
            UpdateDirectorMovieCommand command = new UpdateDirectorMovieCommand(null,null);
            command.Model = new UpdateDirectorMovieViewModel()
            {
                MovieId = movieId,
                DirectorId = directorId
            };

            //act
            UpdateDirectorMovieCommandValidator validator = new UpdateDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}