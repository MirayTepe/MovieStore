using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Commands.Create
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("","test")]
        [InlineData("test","")]        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(string name, string surName)
        {
            //arrange
            CreateActorCommand command = new CreateActorCommand(null,null);
            command.Model = new CreateActorViewModel()
            {
                FirstName = name,
                LastName = surName
            };

            //act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        

        
    }
}