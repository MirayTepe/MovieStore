using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;

namespace MovieStoreWebapi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {

        public int ActorId { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;


        public DeleteActorCommand(IMovieStoreDbContext movieStoreDbContext)
        {
            _movieStoreDbContext = movieStoreDbContext;

        }
        public void Handle()
        {
            var actor = _movieStoreDbContext.Actors.SingleOrDefault(m => m.Id == ActorId);

            if (actor == null)
                throw new InvalidOperationException("Silinmek istenen oyuncu bulunamadı!");

            _movieStoreDbContext.Actors.Remove(actor);
            _movieStoreDbContext.SaveChanges();



        }
    }
}