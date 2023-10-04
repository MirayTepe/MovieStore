using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.OrderOperations.Commands.CreateOrder
{

    public class CreateOrderCommand
    {
        public CreateOrderViewModel Model { get; set; }
   
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper = mapper;
        }

        public void Handle() 
        {
            var customer = _dbContext.Customers.SingleOrDefault(s => s.Id == Model.CustomerId);
            var movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);

            if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");
            if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
           

            var result = _mapper.Map<Order>(Model);
            result.PurchaseDate = DateTime.Now;
            result.IsActive = true;

            _dbContext.Orders.Add(result);
            _dbContext.SaveChanges();
        }

    }

    public class CreateOrderViewModel 
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
   }
}