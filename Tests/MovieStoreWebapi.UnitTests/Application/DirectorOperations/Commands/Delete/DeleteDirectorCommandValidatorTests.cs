using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Delete
{
    public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int directorId)
        {
            //arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            
            command.Id = directorId;

            //act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int directorId)
        {
            //arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            
            command.Id = directorId;

            //act
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}