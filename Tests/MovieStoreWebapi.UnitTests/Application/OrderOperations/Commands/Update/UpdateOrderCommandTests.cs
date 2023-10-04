using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Commands.Update
{
    public class UpdateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistCustomerIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateOrderViewModel model = new UpdateOrderViewModel() {CustomerId = 0,MovieId =3,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateOrderViewModel model = new UpdateOrderViewModel() {CustomerId = 3,MovieId =0,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenNotExistCustomerMovieIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
             //arrange
            UpdateOrderViewModel model = new UpdateOrderViewModel() {CustomerId = 3,MovieId =3,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;      
            command.OrderId = 0;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı!");
        }

        [Fact]
        public void WhenNotExistMovieAndCustomerIdIsGiven_Update_ShouldBeUpdateOrder()
        {
            //arrange
            UpdateOrderViewModel model = new UpdateOrderViewModel() {CustomerId = 3,MovieId =3,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;      
            command.OrderId = 1;
        
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var order = _dbContext.Orders.SingleOrDefault(s => s.Id == 1);
            
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(model.CustomerId);
            order.MovieId.Should().Be(model.MovieId);
            order.PurchaseDate.Should().Be(model.PurchaseDate);
            order.Price.Should().Be(model.Price);
        }

    }
}