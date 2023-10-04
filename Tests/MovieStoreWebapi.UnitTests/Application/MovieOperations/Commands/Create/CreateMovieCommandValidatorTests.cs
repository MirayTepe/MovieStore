using FluentAssertions;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebapi.UnitTests.TestSetup;
using Xunit;

namespace MovieStoreWebapi.UnitTests.Application.MovieOperations.Commands.Create
{
    public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"testTitle1","testyear1",1)]
        [InlineData(1,"","testyear2",2)]
        [InlineData(2,"testTitle3","",3)]
        [InlineData(1,"testTitle4","testyear4",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErorrs(int directorId,string title, string year,decimal price)
        {
            //arrange
            CreateMovieCommand command = new CreateMovieCommand(null,null);
            command.Model = new CreateMovieViewModel()
            {
                Title = title,
                Year = year,
                DirectorId=directorId,
                Price=price

            };

            //act
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var results = validator.Validate(command);

            //assert
            results.Errors.Count.Should().BeGreaterThan(0);
        }

        

        
    }
}