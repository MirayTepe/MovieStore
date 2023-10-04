using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Create
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        
        [Fact]
        public void WhenDirectorModelIsGiven_Create_ShouldBeCreateDirector()
        {
            // arrange
            CreateDirectorViewModel model = new CreateDirectorViewModel() { FirstName = "testName", LastName = "testSurname"};
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen zaten kayıtlı!");
        
            // assert
            var director = _dbContext.Directors.SingleOrDefault(s => s.FirstName == model.FirstName && s.LastName == model.LastName);
            
            director.Should().NotBeNull();
            director.FirstName.Should().Be(model.FirstName);
            director.LastName.Should().Be(model.LastName);
        }
    }
}
