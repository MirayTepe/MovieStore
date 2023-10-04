using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.OrderOperations.Queries.GetOrderDetail
{

    public class GetOrderDetailQuery
    {
        public GetOrderDetailQueryViewModel Model { get; set; }
        public int OrderId;

   
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper = mapper;
        }

        public GetOrderDetailQueryViewModel Handle() 
        {
            Order order = _dbContext.Orders.Include(i=>i.Customer).Include(x=>x.Movie).Where(s => s.Id == OrderId).SingleOrDefault();

            if (order is null)
                throw new InvalidOperationException("Sipariş bulunamadı!");
             GetOrderDetailQueryViewModel vm = _mapper.Map<GetOrderDetailQueryViewModel>(order);

            return vm;
        }

    }

    public class GetOrderDetailQueryViewModel 
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

    
   }
}