using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Create
{
    public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("","test")]
        [InlineData("test","")]        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(string name, string surName)
        {
            //arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null,null);
            command.Model = new CreateDirectorViewModel()
            {
                FirstName = name,
                LastName = surName
            };

            //act
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        

        
    }
}