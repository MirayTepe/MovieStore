using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Create
{
    public class CreateFavoriteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateFavoriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
         [Fact]
        public void WhenAlreadyExistGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            CreateFavoriteGenreViewModel model = new CreateFavoriteGenreViewModel() {GenreId=0,CustomerId=1};

            //act
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür bulunamadı!");
        }
         [Fact]
        public void WhenNotExistCustomerIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
           CreateFavoriteGenreViewModel model = new CreateFavoriteGenreViewModel() {GenreId=1,CustomerId=0};


            //act
             CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;
            
            

            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Müşteri bulunamadı!");
        }
         [Fact]
        public void WhenAlreadyExistCustomerAndGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
           CreateFavoriteGenreViewModel model = new CreateFavoriteGenreViewModel() {CustomerId=1,GenreId=1};


            //act
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;
            
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Favori film türü zaten eklenmiş!");
        }


        
        [Fact]
        public void WhenNotExistGenreAndCustomerIdIsGiven_Create_ShouldBeCreateFavoriteGenre()
        {
             //arrange
           CreateFavoriteGenreViewModel model = new CreateFavoriteGenreViewModel() {GenreId=5,CustomerId=3};


            //act
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;

            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            // assert
            var favoriteGenre = _dbContext.FavoritesGenres.SingleOrDefault(s => s.CustomerId == model.CustomerId && s.GenreId == model.GenreId);
            
            favoriteGenre.Should().NotBeNull();
            favoriteGenre.CustomerId.Should().Be(model.CustomerId);
            favoriteGenre.GenreId.Should().Be(model.GenreId);
           
        }
    }
}