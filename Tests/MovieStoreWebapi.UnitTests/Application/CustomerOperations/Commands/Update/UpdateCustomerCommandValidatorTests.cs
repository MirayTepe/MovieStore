using System.Runtime.InteropServices;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.UpdateCustomer;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Commands.Update
{
    public class UpdateActorMoviesCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "testName", "testSurname", "testEmail@gmail.com", "testPassword")]
        [InlineData(1, "", "testSurname", "testEmail@gmail.com", "testPassword")]
        [InlineData(1, "testName", "", "testEmail@gmail.com", "testPassword")]
        [InlineData(1, "testName", "testSurname", "", "testPassword")]
        [InlineData(1, "testName", "testSurname", "testEmail@gmail.com", "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, string name, string surName, string email, string password)
        {
            //arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null,null);
            command.Model = new UpdateCustomerViewModel()
            {
                FirstName = name,
                LastName = surName,
                Email = email,
                Password = password
            };
            command.CustomerId = customerId;

            //act
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1, "testName", "testSurname", "testEmail@gmail.com", "testPassword")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int customerId, string name, string surName, string email, string password)
        {
            //arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null,null);
            command.Model = new UpdateCustomerViewModel()
            {
                FirstName = name,
                LastName = surName,
                Email = email,
                Password = password
            };
            command.CustomerId = customerId;

            //act
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().Be(0);
        }
    }
}