using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Commands.Delete
{
    public class DeleteActorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistActorMovieIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            command.ActorId = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Silinmek istenen oyuncu bulunamadı!");                    
        }

    }
}