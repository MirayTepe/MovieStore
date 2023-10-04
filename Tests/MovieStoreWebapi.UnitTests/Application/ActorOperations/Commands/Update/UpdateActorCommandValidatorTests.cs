using System.Runtime.InteropServices;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;


namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Commands.Update
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateActorCommandValidatorTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
        //[InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenActorIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateActorCommand command = new UpdateActorCommand(_context,_mapper);
            command.Id = id;
            command.Model = new UpdateActorViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Oyuncu bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int actorId = 1;
            UpdateActorCommand command = new UpdateActorCommand(_context,_mapper);
            UpdateActorViewModel model = new UpdateActorViewModel() { FirstName = "Test_WhenValidInputsAreGiven_Actor_ShouldBeUpdated",LastName = "Test_Surname",  };
            command.Model = model;
            command.Id = actorId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var actor = _context.Actors.SingleOrDefault(x => x.Id == actorId);

            actor.Should().NotBeNull();
            actor.FirstName.Should().Be(model.FirstName);
            actor.LastName.Should().Be(model.LastName);
         
        }

    }
}