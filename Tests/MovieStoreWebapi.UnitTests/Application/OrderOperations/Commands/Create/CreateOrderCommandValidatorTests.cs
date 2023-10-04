using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebapi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;



namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Commands.Create
{
    public class CreateOrderCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, 1, "2022-07-06",20)]
        [InlineData(1, 0, "2021-07-08",20)]
        [InlineData(1, 5, "2021-07-08",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int customerId,int movieId, DateTime purchaseDate,decimal price)
        {
            //arrange
            CreateOrderCommand command = new CreateOrderCommand(null,null);
            command.Model = new CreateOrderViewModel()
            {
                CustomerId = customerId,
                MovieId = movieId,
                PurchaseDate=purchaseDate,
                Price=price,
          

            };

            //act
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}

