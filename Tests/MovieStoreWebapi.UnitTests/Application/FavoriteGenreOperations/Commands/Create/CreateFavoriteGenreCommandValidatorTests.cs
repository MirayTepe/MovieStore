using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Create
{
    public class CreateFavoriteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        [InlineData(null,1)]
        [InlineData(1,null)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int genreId, int customerId)
        {
            //arrange
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(null,null);
            command.Model = new CreateFavoriteGenreViewModel()
            {
                GenreId = genreId,
                CustomerId = customerId
            };

            //act
            CreateFavoriteGenreCommandValidator validator = new CreateFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,1)]                                
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int genreId, int customerId)
        {
            //arrange
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(null,null);
            command.Model = new CreateFavoriteGenreViewModel()
            {
                GenreId = genreId,
                CustomerId = customerId
            };

            //act
            CreateFavoriteGenreCommandValidator validator = new CreateFavoriteGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

        
    }
}