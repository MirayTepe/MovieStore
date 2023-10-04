using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.ActorOperations.Commands.Create
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        
        [Fact]
        public void WhenActorModelIsGiven_Create_ShouldBeCreateActor()
        {
            // arrange
            CreateActorViewModel model = new CreateActorViewModel() { FirstName = "testName", LastName = "testSurname"};
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var actor = _dbContext.Actors.SingleOrDefault(s => s.FirstName == model.FirstName && s.LastName == model.LastName);
            
            actor.Should().NotBeNull();
            actor.FirstName.Should().Be(model.FirstName);
            actor.LastName.Should().Be(model.LastName);
        }
    }
}
