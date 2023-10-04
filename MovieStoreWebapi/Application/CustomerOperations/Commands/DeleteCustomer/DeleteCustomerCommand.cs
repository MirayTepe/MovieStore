using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;

namespace MovieStoreWebapi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {

        public int CustomerId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;


        public DeleteCustomerCommand(IMovieStoreDbContext movieStoreDbContext)
        {
            _movieStoreDbContext = movieStoreDbContext;

        }
        public void Handle()
        {
            var customer = _movieStoreDbContext.Customers.SingleOrDefault(m => m.Id == CustomerId);

            if (customer == null)
                throw new InvalidOperationException("Silinmek istenen müşteri bulunamadı!");

            _movieStoreDbContext.Customers.Remove(customer);
            _movieStoreDbContext.SaveChanges();



        }
    }
}