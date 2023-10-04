using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebapi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStoreWebapi.Application.DirectorOperations.Queries.Get;
using MovieStoreWebapi.Application.DirectorOperations.Queries.GetDirectorDetail;
using static MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

         [HttpGet]
        public IActionResult GetDirectors()
        {
            GetDirectorsQuery query = new GetDirectorsQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetDirectorDetail(int id)
        {
            
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
            query.Id = id;

            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateDirector([FromBody]CreateDirectorViewModel model)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper);
            command.Model= model;

            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDirector([FromBody] UpdateDirectorViewModel model, int id)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.Id = id;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
}