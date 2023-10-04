using System.Runtime.InteropServices;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;
using static MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;


namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Update
{
    public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectorCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
        //[InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenDirectorIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context,_mapper);
            command.Id = id;
            command.Model = new UpdateDirectorViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int directorId = 1;
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context,_mapper);
            UpdateDirectorViewModel model = new UpdateDirectorViewModel() { FirstName = "Test_WhenValidInputsAreGiven_Director_ShouldBeUpdated",LastName = "Test_Surname",  };
            command.Model = model;
            command.Id = directorId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var director = _context.Directors.SingleOrDefault(x => x.Id == directorId);

            director.Should().NotBeNull();
            director.FirstName.Should().Be(model.FirstName);
            director.LastName.Should().Be(model.LastName);
         
        }

    }
}