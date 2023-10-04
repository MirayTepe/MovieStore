using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderDetailQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
       [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenOrderIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
            query.OrderId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Sipariş bulunamadı!");

        }
    }
}