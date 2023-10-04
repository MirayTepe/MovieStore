using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder
{

    public class UpdateOrderCommand
    {
        public UpdateOrderViewModel Model { get; set; }
        public int OrderId;

   
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper = mapper;
        }

        public void Handle() 
        {
            var customer = _dbContext.Customers.SingleOrDefault(s => s.Id == Model.CustomerId);
            var movies = _dbContext.Movies.SingleOrDefault(s => s.Id == Model.MovieId);

          Order order = _dbContext.Orders.SingleOrDefault(s => s.Id == OrderId);

            if (customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");
            else if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");
            else if (order is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı!");
                
            _mapper.Map(Model, order);

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
        }

    }

    public class UpdateOrderViewModel 
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
   }
}