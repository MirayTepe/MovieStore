using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.OrderOperations.Commands.DeleteOrder
{

    public class DeleteOrderCommand
    {
     
        private readonly IMovieStoreDbContext _dbContext;
        public int OrderId;

        public DeleteOrderCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext= dbContext;
          
        }

        public void Handle() 
        {
         var order = _dbContext.Orders.SingleOrDefault(s => s.Id == OrderId);

            if (order is null)
                throw new InvalidOperationException("ilgili kayda ait veri bulunamadı!");

            order.IsActive = false;

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
        }

    }

}