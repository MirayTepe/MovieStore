using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.Application.CustomerOperations.Commands.UpdateCustomer;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Commands.Update
{
    public class UpdateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistCustomerIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateCustomerViewModel model = new UpdateCustomerViewModel() { FirstName = "testName", LastName = "testSurname" , Email = "test@gmail.com", Password = "test"};

            //act
            UpdateCustomerCommand command = new UpdateCustomerCommand(_dbContext,_mapper);
            command.Model = model;
            command.CustomerId = 0;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri bulunamadı!");
        }

              
        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeUpdated()
        {
            // arrange
            int customerId=1;
            UpdateCustomerCommand command = new UpdateCustomerCommand(_dbContext,_mapper);
            UpdateCustomerViewModel model = new UpdateCustomerViewModel() { FirstName = "testName", LastName = "testSurname" , Email = "test@gmail.com", Password = "test"};
           
            command.Model = model;
            command.CustomerId = customerId;       
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var customer = _dbContext.Customers.SingleOrDefault(s => s.Id == customerId);
            
            customer.Should().NotBeNull();
            customer.FirstName.Should().Be(model.FirstName);
            customer.LastName.Should().Be(model.LastName);
            customer.Email.Should().Be(model.Email);
            customer.Password.Should().Be(model.Password);
        }

    }
}
