using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;
using static MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;
namespace MovieStoreWebapi.UnitTests.Application.DirectorOperations.Commands.Update
{
    public class UpdateDirectorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistDirectorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateDirectorViewModel model = new UpdateDirectorViewModel() {FirstName = "test", LastName = "test"};

            //act
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext,_mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı!");
        }

               
        [Fact]
        public void WhenAlreadyExistDirectorIdAndModelAreGiven_Update_ShouldBeUpdateDirector()
        {
            // arrange
            UpdateDirectorViewModel model = new UpdateDirectorViewModel() { FirstName = "test", LastName = "test"};
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext,_mapper);
            command.Model = model;
            command.Id = 1;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var director = _dbContext.Directors.SingleOrDefault(s => s.Id == 1);
            
            director.Should().NotBeNull();
            director.FirstName.Should().Be(model.FirstName);
            director.LastName.Should().Be(model.LastName);
        }

    }
}