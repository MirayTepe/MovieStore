using System.Runtime.InteropServices;
using AutoMapper;
using FluentAssertions;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;



namespace MovieStoreWebapi.UnitTests.Application.GenreOperations.Commands.Update
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
    
        [Theory]
       // [InlineData(2, "Underground Literature")]
        [InlineData(3, "")]
        [InlineData( 4,"S")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string genreName)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null,null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreViewModel()
            {
                Name= genreName,
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null,null);
            command.GenreId = 1;
            command.Model = new UpdateGenreViewModel()
            {
                Name = "Underground Literature",
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }

    }
}