using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Commands.Create
{
    public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("", "test", "test", "test")]
        [InlineData("test", "", "test", "test")]
        [InlineData("test", "test", "", "test")]
        [InlineData("test", "test", "test", "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(string name, string surName, string email, string password)
        {
            //arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerViewModel()
            {
                FirstName = name,
                LastName = surName,
                Email = email,
                Password = password
            };

            //act
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }




    }
}