using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Delete
{
    public class DeleteFavoriteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteFavoriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistFavoriteGenreIsGiven_InvalidOperationException_ShouldBeReturnErrors()
        {
            // Given
            DeleteFavoriteGenreCommand command = new DeleteFavoriteGenreCommand(_dbContext);
            command.Id = 0;
        
            // When // Then
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı.");                    
        }

    }
}