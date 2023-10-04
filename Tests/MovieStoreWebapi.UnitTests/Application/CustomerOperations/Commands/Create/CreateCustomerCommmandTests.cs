using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Commands.Create
{
    public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
         [Fact]
        public void WhenAlreadyExistModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateCustomerViewModel model = new CreateCustomerViewModel() { FirstName = "testName", LastName = "testSurname" , Email = "test@gmail.com", Password = "test"};

            //act
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri zaten mevcut!");
        }
        
        [Fact]
        public void WhenCustomerModelIsGiven_Create_ShouldBeCreateCustomer()
        {
            // arrange
            CreateCustomerViewModel model = new CreateCustomerViewModel() { FirstName = "testName1", LastName = "testSurname1" , Email = "test1@gmail.com", Password = "test1"};
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var customer = _dbContext.Customers.SingleOrDefault(s => s.FirstName == model.FirstName && s.LastName == model.LastName);
            
            customer.Should().NotBeNull();
            customer.FirstName.Should().Be(model.FirstName);
            customer.LastName.Should().Be(model.LastName);
            customer.Email.Should().Be(model.Email);
            customer.Password.Should().Be(model.Password);
        }
    }
}