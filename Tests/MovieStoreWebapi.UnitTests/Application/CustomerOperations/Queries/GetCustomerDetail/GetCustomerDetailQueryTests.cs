using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenCustomerIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.Id = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri kaydı bulunamadı!");

        }
    }
}