using FluentAssertions;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Update
{
    public class UpdateFavoriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int customerId, int genreId)
        {
            //arrange
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(null,null);
            command.Model = new UpdateFavoriteGenreViewModel()
            {
                CustomerId = customerId,
                GenreId = genreId
            };

            //act
            UpdateFavoriteGenreCommandValidator validator = new UpdateFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int customerId, int genreId)
        {
              //arrange
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(null,null);
            command.Model = new UpdateFavoriteGenreViewModel()
            {
                CustomerId = customerId,
                GenreId = genreId
            };

            //act
            UpdateFavoriteGenreCommandValidator validator = new UpdateFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}