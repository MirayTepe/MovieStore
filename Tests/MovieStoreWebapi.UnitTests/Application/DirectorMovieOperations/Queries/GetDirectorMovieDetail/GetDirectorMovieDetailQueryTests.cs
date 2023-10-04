using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail
{
    public class GetDirectorMovieDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenDirectorMovieIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetDirectorMovieDetailQuery query = new GetDirectorMovieDetailQuery(_context, _mapper);
            query.Id = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmenin film kaydı bulunamadı!");

        }
    }
}