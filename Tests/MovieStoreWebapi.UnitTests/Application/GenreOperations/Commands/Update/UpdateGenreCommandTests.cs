using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Update
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateGenreViewModel model = new UpdateGenreViewModel() {Name = "test"};

            //act
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext,_mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film türü bulunamadı!");
        }

               
        [Fact]
        public void WhenAlreadyExistGenreIdAndModelAreGiven_Update_ShouldBeUpdateGenre()
        {
            // arrange
            UpdateGenreViewModel model = new UpdateGenreViewModel() { Name = "test"};
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext,_mapper);
            command.Model = model;
            command.GenreId = 1;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var genre = _dbContext.Genres.SingleOrDefault(s => s.Id == 1);
            
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
           
        }

    }
}