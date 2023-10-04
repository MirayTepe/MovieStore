using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebapi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Delete
{
    public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenGenreIdIsNotExist_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            // Arrange (preparation)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = genreId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü bulunamadı!");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted(int genreId)
        {
            // Arrange (preparation)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = genreId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            // Assert 
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == genreId);
            genre.Should().BeNull();
        }


    }
}