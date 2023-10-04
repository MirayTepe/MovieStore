using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.DeleteFavoriGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenres;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class FavoriteGenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public FavoriteGenreController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFavoriteGenres()
        {
            GetFavoriteGenresQuery query = new GetFavoriteGenresQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetFavoriteGenreDetail(int id)
        {
            GetFavoriteGenreDetailQuery query = new GetFavoriteGenreDetailQuery(_context,_mapper);
            query.Id = id;

            GetFavoriteGenreDetailQueryValidator validator = new GetFavoriteGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateFavoriteGenre([FromBody] CreateFavoriteGenreViewModel model)
        {
            CreateFavoriteGenreCommand command = new CreateFavoriteGenreCommand(_context,_mapper);
            command.Model = model;

            CreateFavoriteGenreCommandValidator validator = new CreateFavoriteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFavoriteGenre([FromBody]UpdateFavoriteGenreViewModel model, int id)
        {
            UpdateFavoriteGenreCommand command = new UpdateFavoriteGenreCommand(_context,_mapper);
            command.Model = model;
            command.Id = id;

            UpdateFavoriteGenreCommandValidator validator = new UpdateFavoriteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFavoriteGenre(int id)
        {
            DeleteFavoriteGenreCommand command = new DeleteFavoriteGenreCommand(_context);
            command.Id = id;

            DeleteFavoriteGenreCommandValidator validator = new DeleteFavoriteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
    
}