using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Commands.Delete
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int customerId)
        {
            //arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            
            command.CustomerId = customerId;

            //act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int customerId)
        {
            //arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            
            command.CustomerId= customerId;

            //act
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}