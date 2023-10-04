using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;
using Newtonsoft.Json;


namespace MovieStoreWebapi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetOrdersViewModel> Handle()
        {
            List<Order> orders = _context.Orders.Include(c=>c.Customer).Include(m=>m.Movie).OrderBy(x => x.Id).ToList();                   
        
            
            return _mapper.Map<List<GetOrdersViewModel>>(orders);
        }
    }

    public class GetOrdersViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

     
    }
}