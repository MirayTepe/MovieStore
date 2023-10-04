using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Queries.GetFavoriteGenreDetail
{
    public class GetDirectorMovieDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorMovieDetailQueryValidatorTests(CommonTestFixture testFixture)
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
            GetFavoriteGenreDetailQuery query = new GetFavoriteGenreDetailQuery(null, null);
            query.Id = id;

            // Act
           GetFavoriteGenreDetailQueryValidator validator = new GetFavoriteGenreDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetFavoriteGenreDetailQuery query = new GetFavoriteGenreDetailQuery(null, null);
            query.Id = 4;

            // Act
            GetFavoriteGenreDetailQueryValidator validator = new GetFavoriteGenreDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}