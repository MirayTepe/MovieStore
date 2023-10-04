using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.DirectorMovieOperations.Commands.Create
{
    public class CreateDirectorMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
         [Fact]
        public void WhenAlreadyExistDirectorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateDirectorMovieViewModel model = new CreateDirectorMovieViewModel() {DirectorId=0,MovieId=1};

            //act
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı!");
        }
         [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateDirectorMovieViewModel model = new CreateDirectorMovieViewModel() { DirectorId = 1, MovieId = 0};

            //act
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }
         [Fact]
        public void WhenAlreadyExistMovieAndDirectorIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateDirectorMovieViewModel model = new CreateDirectorMovieViewModel() { DirectorId = 1, MovieId = 1};

            //act
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(_dbContext, _mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmenin bu filmi zaten eklenmiş!");
        }


        
        [Fact]
        public void WhenNotExistDirectorAndMovieIdIsGiven_Create_ShouldBeCreateDirectorMovie()
        {
            // arrange
            CreateDirectorMovieViewModel model = new CreateDirectorMovieViewModel() {DirectorId=4,MovieId=3};
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(_dbContext, _mapper);
            command.Model = model;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var directorMovie = _dbContext.DirectorMovies.SingleOrDefault(s => s.DirectorId == model.DirectorId && s.MovieId == model.MovieId);
            
            directorMovie.Should().NotBeNull();
            directorMovie.DirectorId.Should().Be(model.DirectorId);
            directorMovie.MovieId.Should().Be(model.MovieId);
           
        }
    }
}