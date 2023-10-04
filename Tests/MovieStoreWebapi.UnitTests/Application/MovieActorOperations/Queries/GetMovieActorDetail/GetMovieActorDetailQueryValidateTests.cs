using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieActorOperations.Queries.GetMovieActorDetail
{
    public class GetMovieActorDetailQueryValidateTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieActorDetailQueryValidateTests(CommonTestFixture testFixture)
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
            GetMovieActorDetailQuery query = new GetMovieActorDetailQuery(null, null);
            query.Id = id;

            // Act
           GetMovieActorDetailQueryValidator validator = new GetMovieActorDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetMovieActorDetailQuery query = new GetMovieActorDetailQuery(null, null);
            query.Id = 4;

            // Act
            GetMovieActorDetailQueryValidator validator = new GetMovieActorDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}