using FluentAssertions;
using MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebapi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Delete
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            
            command.GenreId = genreId;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            
            command.GenreId = genreId;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}