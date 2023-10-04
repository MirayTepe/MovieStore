using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryValidatorTests(CommonTestFixture testFixture)
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
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.Id = id;

            // Act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.Id = 4;

            // Act
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}