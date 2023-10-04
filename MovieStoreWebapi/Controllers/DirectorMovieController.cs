using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.DeleteDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovies;


namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class DirectorMovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorMovieController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

         [HttpGet]
        public IActionResult GetDirectorMovies()
        {
            GetDirectorMoviesQuery query = new GetDirectorMoviesQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetDirectorMovieDetail(int id)
        {
            
            GetDirectorMovieDetailQuery query = new GetDirectorMovieDetailQuery(_context,_mapper);
            query.Id = id;

            GetDirectorMovieDetailQueryValidator validator = new GetDirectorMovieDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateDirectorMovie([FromBody]CreateDirectorMovieViewModel model)
        {
            CreateDirectorMovieCommand command = new CreateDirectorMovieCommand(_context,_mapper);
            command.Model= model;

            CreateDirectorMovieCommandValidator validator = new CreateDirectorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDirectorMovie([FromBody] UpdateDirectorMovieViewModel model, int id)
        {
            UpdateDirectorMovieCommand command = new UpdateDirectorMovieCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateDirectorMovieCommandValidator validator = new UpdateDirectorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirectorMovie(int id)
        {
            DeleteDirectorMovieCommand command = new DeleteDirectorMovieCommand(_context);
            command.Id = id;

            DeleteDirectorMovieCommandValidator validator = new DeleteDirectorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
}