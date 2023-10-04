using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetActorsViewModel> Handle()
        {
            List<Actor> actors = _context.Actors.Include(s=>s.ActorMovies).ThenInclude(i=>i.Movie).OrderBy(x => x.Id).ToList();            
            List<GetActorsViewModel> vm = _mapper.Map<List<GetActorsViewModel>>(actors);

            return vm;
        }

    }

    public class GetActorsViewModel
    {
        public int Id{get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IReadOnlyList<string> ActorMovies { get; set; }
      
    }

}