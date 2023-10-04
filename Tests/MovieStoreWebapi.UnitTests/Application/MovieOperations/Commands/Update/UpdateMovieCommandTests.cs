using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;
namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Update
{
    public class UpdateMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistMovieIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateMovieViewModel model = new UpdateMovieViewModel() {Title = "test", Year = "2002",Price=5,DirectorId=2};

            //act
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext,_mapper);
            command.Model = model;
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film bulunamadı!");
        }

               
        [Fact]
        public void WhenAlreadyExistMovieIdAndModelAreGiven_Update_ShouldBeUpdateMovie()
        {
            // arrange
            UpdateMovieViewModel model = new UpdateMovieViewModel() { Title = "test", Year = "2018",DirectorId=2,Price=3};
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext,_mapper);
            command.Model = model;
            command.MovieId = 2;
        
            // act
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var movie = _dbContext.Movies.SingleOrDefault(s => s.Id == 2);
            
            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.Year.Should().Be(model.Year);
            movie.Price.Should().Be(model.Price);
            movie.DirectorId.Should().Be(model.DirectorId);
        }

    }
}