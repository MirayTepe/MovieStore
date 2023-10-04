using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Queries.GetDirectorMovieDetail
{
    public class GetFavoriteGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetFavoriteGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenFavoriteGenreIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetFavoriteGenreDetailQuery query = new GetFavoriteGenreDetailQuery(_context, _mapper);
            query.Id = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Favori film türü bulunamadı!");

        }
    }
}