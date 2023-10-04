using System.Runtime.InteropServices;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;


namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Update
{
    public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
        //[InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenMovieIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_context,_mapper);
            command.MovieId = id;
            command.Model = new UpdateMovieViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int movieId = 1;
            UpdateMovieCommand command = new UpdateMovieCommand(_context,_mapper);
            UpdateMovieViewModel model = new UpdateMovieViewModel() { Title = "Test_title",Year = "Test_Year",Price=4,DirectorId=3 };
            command.Model = model;
            command.MovieId = movieId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var movie = _context.Movies.SingleOrDefault(x => x.Id == movieId);

            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.Year.Should().Be(model.Year);
            movie.Price.Should().Be(model.Price);
            movie.DirectorId.Should().Be(model.DirectorId);
         
        }

    }
}