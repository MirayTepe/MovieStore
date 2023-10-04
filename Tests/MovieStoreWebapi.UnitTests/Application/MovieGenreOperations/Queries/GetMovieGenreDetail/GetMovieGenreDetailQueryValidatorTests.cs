using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Queries.GetMovieGenreDetail
{
    public class GetMovieGenreDetailQueryValidateTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieGenreDetailQueryValidateTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int id)
        {
            // Arrange
            GetMovieGenreDetailQuery query = new GetMovieGenreDetailQuery(null, null);
            query.Id = id;

            // Act
           GetMovieGenreDetailQueryValidator validator = new GetMovieGenreDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetMovieGenreDetailQuery query = new GetMovieGenreDetailQuery(null, null);
            query.Id = 4;

            // Act
            GetMovieGenreDetailQueryValidator validator = new GetMovieGenreDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}