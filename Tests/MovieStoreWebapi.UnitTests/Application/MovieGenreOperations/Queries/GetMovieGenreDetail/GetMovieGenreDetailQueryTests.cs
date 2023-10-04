using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Queries.GetMovieGenreDetail
{
    public class GetMovieGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
       [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenMovieGenreIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetMovieGenreDetailQuery query = new GetMovieGenreDetailQuery(_dbContext, _mapper);
            query.Id = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri bulunamadı!");

        }
    }
}