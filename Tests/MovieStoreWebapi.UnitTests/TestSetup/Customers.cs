using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Customers
    {
        public static void AddCustomers(this MovieStoreDbContext context)
        {
            context.Customers.AddRange(
                new Customer{FirstName = "Ayşe",LastName = "Taş",Email = "ayse95@gmail.com",Password = "123456", 
                IsActive = true},
                new Customer{ FirstName = "fatma",LastName = "Kaya", Email = "kaya@gmail.com", Password = "123456",
                IsActive = true },
                new Customer{ FirstName = "Demir",LastName = "Yıldırım",Email = "demir@gmail.com", Password = "123456", IsActive = true},

                new Customer(){FirstName = "testName",LastName = "testSurname",Email = "test@gmail.com",Password = "test", IsActive=true}
            );
        }
    }
}