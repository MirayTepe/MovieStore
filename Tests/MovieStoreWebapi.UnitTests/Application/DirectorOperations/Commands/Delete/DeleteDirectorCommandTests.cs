using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Delete
{
    public class DeleteDirectorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistDirectorIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            command.Id = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen bulunamadı!");                    
        }

    }
}