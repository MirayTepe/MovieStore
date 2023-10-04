using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActors;





namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ActorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetActorDetail(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
            query.Id = id;

            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorViewModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_context,_mapper);
            command.Model = model;

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActor([FromBody] UpdateActorViewModel model, int id)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
    
}