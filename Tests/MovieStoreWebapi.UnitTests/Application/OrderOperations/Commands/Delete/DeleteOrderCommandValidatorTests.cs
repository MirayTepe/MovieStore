using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.OrderOperations.Commands.DeleteOrder;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Commands.Delete
{
    public class DeleteOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       [Theory]
        [InlineData(-1)]
        [InlineData(0)]
         
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int orderId)
        {
            //arrange
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            
            command.OrderId = orderId;

            //act
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]         
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeError(int orderId)
        {
            //arrange
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            
            command.OrderId = orderId;

            //act
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}