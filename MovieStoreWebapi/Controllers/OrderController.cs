using System.ComponentModel.Design;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.Commands.OrderOperations.UpdateOrder;
using MovieStoreWebapi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebapi.Application.OrderOperations.Commands.DeleteOrder;
using MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder;
using MovieStoreWebapi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebapi.Application.OrderOperations.Queries.GetOrders;
namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

         [HttpGet]
         public IActionResult GetOrders()
         {
             GetOrdersQuery query = new GetOrdersQuery(_dbContext, _mapper);
             var response = query.Handle();

             return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext, _mapper);
            query.OrderId = id;

            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var response = query.Handle();

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateOrderMovie([FromBody] CreateOrderViewModel model)
        {
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = model;

            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePurchasedMovie([FromBody] 
        UpdateOrderViewModel model, int Id)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            command.OrderId = Id;

            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int Id)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);        
            command.OrderId = Id;

            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}