using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Commands.Create
{
    public class CreateDirectorMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
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
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(null,null);
            command.Model = new CreateDirectorMovieViewModel()
            {
                MovieId = movieId,
                DirectorId = directorId
            };

            //act
            CreateDirectorMovieCommandValidator validator = new CreateDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int movieId, int directorId)
        {
            //arrange
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(null,null);
            command.Model = new CreateDirectorMovieViewModel()
            {
                MovieId = movieId,
                DirectorId = directorId
            };

            //act
            CreateDirectorMovieCommandValidator validator = new CreateDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}