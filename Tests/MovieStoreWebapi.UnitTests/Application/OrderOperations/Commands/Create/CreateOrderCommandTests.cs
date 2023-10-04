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
    public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistCustomerIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateOrderViewModel model = new CreateOrderViewModel() {CustomerId = 0,MovieId =3,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri bulunamadı!");
        }
        [Fact]
        public void WhenAlreadyExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateOrderViewModel model = new CreateOrderViewModel() {CustomerId = 4,MovieId =0,PurchaseDate= new DateTime(2021, 07, 08),Price=20};

            //act
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }
    

        
        [Fact]
        public void WhenOrderModelIsGiven_Create_ShouldBeCreateOrder()
        {
            // arrange
            CreateOrderViewModel model = new CreateOrderViewModel() { CustomerId = 3,MovieId =3,PurchaseDate= new DateTime(2021, 07, 08),Price=20};
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var order = _context.Orders.SingleOrDefault(s => s.CustomerId == model.CustomerId && s.MovieId == model.MovieId );
            
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(model.CustomerId);
            order.MovieId.Should().Be(model.MovieId);
           

        }
    }
}

