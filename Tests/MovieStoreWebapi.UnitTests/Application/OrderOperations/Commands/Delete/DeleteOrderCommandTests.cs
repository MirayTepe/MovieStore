using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.OrderOperations.Commands.Delete
{
    public class DeleteOrderCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
       
        private readonly IMapper _mapper;
        public DeleteOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistOrderIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_dbContext);
            command.Id = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı!");                    
        }

    }
}