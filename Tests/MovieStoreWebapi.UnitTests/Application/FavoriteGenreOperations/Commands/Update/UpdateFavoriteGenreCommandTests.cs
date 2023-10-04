using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.FavoriteGenreOperations.Commands.Update
{
    public class UpdateFavoriteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateFavoriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
         [Fact]
        public void WhenAlreadyExistGenreIdInModelIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateFavoriteGenreViewModel model = new UpdateFavoriteGenreViewModel() {CustomerId=1,GenreId=0};

            //act
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(_dbContext, _mapper);
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
            UpdateFavoriteGenreViewModel model = new UpdateFavoriteGenreViewModel() {CustomerId=0,GenreId=1};

            //act
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(_dbContext, _mapper);
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
            UpdateFavoriteGenreViewModel model = new UpdateFavoriteGenreViewModel() {CustomerId=1,GenreId=1};

            //act
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;
            
            
            //assert
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("ilgili kayda ait veri bulunamadı!");
        }


        
        [Fact]
        public void WhenNotExistGenreAndCustomerIdIsGiven_Update_ShouldBeCreateFavoriteGenre()
        {
           //arrange
            UpdateFavoriteGenreViewModel model = new UpdateFavoriteGenreViewModel() {CustomerId=1,GenreId=5};

            //act
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(_dbContext, _mapper);
            command.Model = model;
            
            command.Model = model;
            command.Id = 1;

        
            // assert
            FluentActions
                .Invoking(()=> command.Handle()).Invoke();
        
            
            var favoriteGenre= _dbContext.FavoritesGenres.SingleOrDefault(s => s.CustomerId == model.CustomerId && s.GenreId == model.GenreId);
            
            favoriteGenre.Should().NotBeNull();
            favoriteGenre.CustomerId.Should().Be(model.CustomerId);
            favoriteGenre.GenreId.Should().Be(model.GenreId);
           
        }
    }
}