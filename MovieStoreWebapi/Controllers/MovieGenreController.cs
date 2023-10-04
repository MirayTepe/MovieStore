using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;

using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.CreateMovieGenre;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.DeleteMovieGenre;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.UpdateMovieGenre;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenres;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MovieGenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieGenreController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovieGenres()
        {
            GetMovieGenresQuery2 query = new GetMovieGenresQuery2(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetMoviGenreDetail(int id)
        {
            GetMovieGenreDetailQuery query = new GetMovieGenreDetailQuery(_context,_mapper);
            query.Id = id;

            GetMovieGenreDetailQueryValidator validator = new GetMovieGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }
    


        [HttpPost]
        public IActionResult CreateMovieActor([FromBody] CreateMovieGenreViewModel model)
        {
            CreateMovieGenreCommand command = new CreateMovieGenreCommand(_context,_mapper);
            command.Model = model;

            CreateMovieGenreCommandValidator validator = new CreateMovieGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromBody]UpdateMovieGenreViewModel model, int id)
        {
            UpdateMovieGenreCommand command = new UpdateMovieGenreCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateMovieGenreCommandValidator validator = new UpdateMovieGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteMovieGenreCommand command = new DeleteMovieGenreCommand(_context);
            command.Id = id;

            DeleteMovieGenreCommandValidator validator = new DeleteMovieGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
    
}