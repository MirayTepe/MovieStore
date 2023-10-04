using System;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Delete
{
    public class DeleteMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenMovieIdIsNotExist_InvalidOperationException_ShouldBeReturn(int movieId)
        {
            // Arrange (preparation)
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId= movieId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted(int movieId)
        {
            // Arrange (preparation)
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId = movieId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            // Assert 
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == movieId);
            movie.Should().BeNull();
        }


    }
}