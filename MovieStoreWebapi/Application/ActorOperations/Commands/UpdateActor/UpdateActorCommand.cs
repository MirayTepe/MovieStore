using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext _context; 
        private readonly IMapper _mapper;
        public UpdateActorViewModel Model;
        public int Id;

        public UpdateActorCommand(IMovieStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x=> x.Id == Id);
            if(actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı!");
            
             // ViewModel'den Actor nesnesine veri aktarımını AutoMapper ile gerçekleştirin
            _mapper.Map(Model, actor);
            //  actor.FirstName = actor.FirstName == default ? actor.FirstName : Model.FirstName;
            //  actor.LastName = actor.LastName == default ? actor.LastName : Model.LastName;
           
            _context.SaveChanges();
        }
    }

    public class UpdateActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}