using FluentAssertions;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Delete
{
    public class DeleteFavoriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorr(int favoriteGenreId)
        {
            //arrange
            DeleteFavoriteGenreCommand command = new DeleteFavoriteGenreCommand(null);
            
            command.Id = favoriteGenreId;

            //act
            DeleteFavoriteGenreCommandValidator validator = new DeleteFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int favoriteGenreId)
        {
            //arrange
            DeleteFavoriteGenreCommand command = new DeleteFavoriteGenreCommand(null);
            
            command.Id = favoriteGenreId;

            //act
            DeleteFavoriteGenreCommandValidator validator = new DeleteFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}