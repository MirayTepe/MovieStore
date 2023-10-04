using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetActorDetailViewModel Handle()
        {
            var actor = _context.Actors.Include(s=>s.ActorMovies).ThenInclude(i=>i.Movie).Where(x => x.Id == Id).SingleOrDefault();
            if (actor is null)
                throw new InvalidOperationException("Oyuncu kaydı bulunamadı!");
            
            var actorDetail = _mapper.Map<GetActorDetailViewModel>(actor);

            return actorDetail;
        }
        public class GetActorDetailViewModel
        {
            public int Id{get;set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public IReadOnlyList<string> ActorMovies { get; set; }

           
        
        }
    }
}