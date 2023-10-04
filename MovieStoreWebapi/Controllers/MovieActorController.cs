using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MovieActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieActorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovieActors()
        {
            GetMovieActorsQuery2 query = new GetMovieActorsQuery2(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieActorDetail(int id)
        {
            GetMovieActorDetailQuery query = new GetMovieActorDetailQuery(_context,_mapper);
            query.Id = id;

            GetMovieActorDetailQueryValidator validator = new GetMovieActorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateMovieActor([FromBody] CreateMovieActorViewModel model)
        {
            CreateMovieActorCommand command = new CreateMovieActorCommand(_context,_mapper);
            command.Model = model;

            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovieActor([FromBody]UpdateMovieActorViewModel model, int id)
        {
            UpdateMovieActorCommand command = new UpdateMovieActorCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateMovieActorCommandValidator validator = new UpdateMovieActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovieActor(int id)
        {
            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.Id = id;

            DeleteMovieActorCommandValidator validator = new DeleteMovieActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
    
}