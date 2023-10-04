using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateDirectorViewModel Model;
        public int Id;

        public UpdateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(s => s.Id == Id);
            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");            
            
             _mapper.Map(Model, director);
            
            _dbContext.SaveChanges();         
        }

         public class UpdateDirectorViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

         
         
        }
    }
}