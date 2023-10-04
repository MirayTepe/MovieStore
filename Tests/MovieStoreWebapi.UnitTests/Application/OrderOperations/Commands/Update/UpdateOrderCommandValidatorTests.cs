using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.Commands.OrderOperations.UpdateOrder;
using MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Commands.Update
{
    public class UpdateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1,0, 1, "2022-07-06",20)]
        [InlineData(2,1, 0, "2021-07-08",20)]
        [InlineData(3,1, 5, "2021-07-08",0)]
        [InlineData(0,1, 3, "2021-07-08",20)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int orderId, int customerId, int movieId, DateTime purchaseDate, decimal price)
        {
            //arrange
            UpdateOrderCommand command = new UpdateOrderCommand(null,null);
            command.Model = new UpdateOrderViewModel()
            {
                CustomerId = customerId,
                MovieId = movieId,
                PurchaseDate=purchaseDate,
                Price=price,
          

            };
            command.OrderId = orderId;

            //act
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(3,1, 5, "2021-07-08",10)]                               
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErorrs(int orderId,int customerId,int movieId, DateTime purchaseDate,decimal price)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(null,null);
            command.Model = new UpdateOrderViewModel()
            {
                CustomerId = customerId,
                MovieId = movieId,
                PurchaseDate=purchaseDate,
                Price=price,
          

            };
            command.OrderId = orderId;

            //act
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}