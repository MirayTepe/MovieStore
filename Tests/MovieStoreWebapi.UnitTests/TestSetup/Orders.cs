using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Orders
    {
        public static void AddOrders(this MovieStoreDbContext context)
        {
           context.Orders.AddRange(
                new Order { CustomerId = 1 , MovieId = 1, PurchaseDate = new DateTime(2022, 07, 06) , Price=10, IsActive = true },
                new Order { CustomerId = 2 , MovieId = 1, PurchaseDate = new DateTime(2022, 12, 05) , Price=10,  IsActive = true },
                new Order { CustomerId = 3 , MovieId = 2, PurchaseDate = new DateTime(2022, 08, 25) , Price=10,  IsActive = true }
            );
        }
    }
}