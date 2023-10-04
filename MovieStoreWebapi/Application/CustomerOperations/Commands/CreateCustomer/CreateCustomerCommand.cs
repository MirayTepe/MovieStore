using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCustomerViewModel Model;

        public CreateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            Customer user = _context.Customers.Include(s=>s.Orders).Include(s=>s.FavoriteGenres).SingleOrDefault(x => x.Email.ToLower() == Model.Email.ToLower());
            if (user is not null)
                throw new InvalidOperationException("Müşteri zaten mevcut!");

            var result = _mapper.Map<Customer>(Model);
            
            _context.Customers.Add(result);
            _context.SaveChanges();
        }
    }

    public class CreateCustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
      
    }
}