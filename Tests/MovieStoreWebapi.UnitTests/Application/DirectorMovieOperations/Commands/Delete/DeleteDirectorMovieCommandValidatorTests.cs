using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.DeleteDirectorMovie;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Commands.Delete
{
    public class DeleteActorMoviesCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int diretorMovieId)
        {
            //arrange
            DeleteDirectorMovieCommand command = new DeleteDirectorMovieCommand(null);
            
            command.Id = diretorMovieId;

            //act
            DeleteDirectorMovieCommandValidator validator = new DeleteDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int diretorMovieId)
        {
            //arrange
            DeleteDirectorMovieCommand command = new DeleteDirectorMovieCommand(null);
            
            command.Id = diretorMovieId;

            //act
            DeleteDirectorMovieCommandValidator validator = new DeleteDirectorMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}