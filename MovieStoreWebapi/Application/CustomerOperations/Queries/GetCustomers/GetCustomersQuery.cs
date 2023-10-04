using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetCustomersViewModel> Handle()
        {
            List<Customer> actors = _context.Customers.Include(s=>s.Orders).ThenInclude(y=>y.Movie).Include(i=>i.FavoriteGenres).ThenInclude(z=>z.Genre).OrderBy(x => x.Id).ToList();            
            List<GetCustomersViewModel> vm = _mapper.Map<List<GetCustomersViewModel>>(actors);

            return vm;
        }

    }

    public class GetCustomersViewModel
    {
            public int Id{get;set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public IReadOnlyList<string> Orders { get; set; }
            public IReadOnlyList<string> FavoriteGenres { get; set; }
    }

}