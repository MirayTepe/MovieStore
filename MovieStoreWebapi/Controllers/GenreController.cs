using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebapi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStoreWebapi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebapi.Application.GenreOperations.Queries;
using MovieStoreWebapi.Application.GenreOperations.Queries.GetDetail;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebapi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovies;



namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreViewModel model)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromBody] UpdateGenreViewModel model, int id)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.Model = model;
            command.GenreId = id;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
    
}