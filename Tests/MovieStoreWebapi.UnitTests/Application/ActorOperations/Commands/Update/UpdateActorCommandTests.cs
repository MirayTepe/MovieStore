using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;
namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Commands.Update
{
    public class UpdateActorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistActorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateActorViewModel model = new UpdateActorViewModel() {FirstName = "test", LastName = "test"};

            //act
            UpdateActorCommand command = new UpdateActorCommand(_dbContext,_mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Oyuncu bulunamadı!");
        }

               
        [Fact]
        public void WhenAlreadyExistActorIdAndModelAreGiven_Update_ShouldBeUpdateActor()
        {
            // arrange
            UpdateActorViewModel model = new UpdateActorViewModel() { FirstName = "test", LastName = "test"};
            UpdateActorCommand command = new UpdateActorCommand(_dbContext,_mapper);
            command.Model = model;
            command.Id = 1;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var actor = _dbContext.Actors.SingleOrDefault(s => s.Id == 1);
            
            actor.Should().NotBeNull();
            actor.FirstName.Should().Be(model.FirstName);
            actor.LastName.Should().Be(model.LastName);
        }

    }
}