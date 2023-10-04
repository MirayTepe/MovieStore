using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStoreWebapi.Application.CustomerOperations.TokenOperations;
using MovieStoreWebapi.TokenOperations.Model;
using WebApi.Application.CustomerOperations.RefreshToken;
namespace MovieStoreWebapi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
            [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_context,_mapper);
            var response = query.Handle();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetActorDetail(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context,_mapper);
            query.Id = id;

            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();

            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerViewModel newUser){
            CreateCustomerCommand command=new CreateCustomerCommand(_context,_mapper);
            command.Model=newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody]CreateTokenModel login)
        {
            CreateTokenCommand command=new CreateTokenCommand(_context ,_mapper,_configuration);
            command.Model=login;
            var token=command.Handle();
            return token;

        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery]string token)
        {
            RefreshTokenCommand command=new RefreshTokenCommand(_context ,_configuration);
            command.RefreshToken=token;
            var resultToken=command.Handle();
            return resultToken;

        }

    }
}