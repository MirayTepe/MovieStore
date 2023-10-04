using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieDetailQueryValidatorTests(CommonTestFixture testFixture)
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
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = id;

            // Act
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = 4;

            // Act
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}