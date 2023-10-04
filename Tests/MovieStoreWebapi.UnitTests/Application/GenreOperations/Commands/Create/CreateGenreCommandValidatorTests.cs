using FluentAssertions;
using MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Create
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]       
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model = new CreateGenreViewModel()
            {
                Name = name
            };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        

        
    }
}