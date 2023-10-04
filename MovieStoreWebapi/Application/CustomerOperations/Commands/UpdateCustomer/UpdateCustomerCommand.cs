using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        private readonly IMovieStoreDbContext _context; 
        private readonly IMapper _mapper;
        public UpdateCustomerViewModel Model;
        public int CustomerId;

        public UpdateCustomerCommand(IMovieStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x=> x.Id == CustomerId);
            if(customer is null)
                throw new InvalidOperationException("Müşteri bulunamadı!");
            
             // ViewModel'den Actor nesnesine veri aktarımını AutoMapper ile gerçekleştirin
            _mapper.Map(Model, customer);
            //  actor.FirstName = actor.FirstName == default ? actor.FirstName : Model.FirstName;
            //  actor.LastName = actor.LastName == default ? actor.LastName : Model.LastName;
           
            _context.SaveChanges();
        }
    }

    public class UpdateCustomerViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}