using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {


        public CreateActorViewModel Model { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _movieStoreDbContext.Actors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);

            if (actor != null)
                throw new InvalidOperationException("Oyuncu zaten mevcut.");

            actor = _mapper.Map<Actor>(Model);


            _movieStoreDbContext.Actors.Add(actor);
            _movieStoreDbContext.SaveChanges();
        }

    }

    public class CreateActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        
        
    }

}