using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.DeleteMovieGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieGenreOperations.Commands.Delete
{
    public class DeleteMovieGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
       
        private readonly IMapper _mapper;
        public DeleteMovieGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistMovieGenreIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteMovieGenreCommand command = new DeleteMovieGenreCommand(_dbContext);
            command.Id = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Film türü bulunamadı!");                    
        }

    }
}