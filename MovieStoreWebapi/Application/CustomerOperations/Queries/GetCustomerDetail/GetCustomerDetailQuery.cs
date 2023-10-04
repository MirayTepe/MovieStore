using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public GetCustomerDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetCustomerDetailViewModel Handle()
        {
            var customer = _context.Customers.Include(s=>s.Orders).ThenInclude(y=>y.Movie).Include(i=>i.FavoriteGenres).ThenInclude(z=>z.Genre).Where(x => x.Id == Id).SingleOrDefault();
            if (customer is null)
                throw new InvalidOperationException("Müşteri kaydı bulunamadı!");
            
            var customerDetail = _mapper.Map<GetCustomerDetailViewModel>(customer);

            return customerDetail;
        }
        public class GetCustomerDetailViewModel
        {
            public int Id{get;set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public IReadOnlyList<string> Orders { get; set; }
            public IReadOnlyList<string> FavoriteGenres { get; set; }

           
        
        }
    }
}