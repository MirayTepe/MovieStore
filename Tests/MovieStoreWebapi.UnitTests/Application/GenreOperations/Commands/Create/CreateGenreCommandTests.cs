using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;



namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Create
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var genre = new Genre() { Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreViewModel() { Name = genre.Name };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            CreateGenreViewModel model = new CreateGenreViewModel() { Name = "Underground Lliterature" };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            // var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            // genre.Should().NotBeNull();
            // genre.Name.Should().Be(model.Name);
        }
    }
}

