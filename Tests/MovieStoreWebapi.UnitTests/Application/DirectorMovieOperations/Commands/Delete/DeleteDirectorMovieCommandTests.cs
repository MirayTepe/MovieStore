using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.DeleteDirectorMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Commands.Delete
{
    public class DeleteDirectorMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteDirectorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistDirectorMovieIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteDirectorMovieCommand command = new DeleteDirectorMovieCommand(_dbContext);
            command.Id = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("İlgili kayda ait veri bulunamadı.");                    
        }

    }
}