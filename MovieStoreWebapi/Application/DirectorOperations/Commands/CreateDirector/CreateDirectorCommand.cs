using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;



namespace MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorViewModel Model;

        public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(s => s.FirstName.Trim().ToLower() == Model.FirstName.Trim().ToLower() && s.LastName.Trim().ToLower() == Model.LastName.Trim().ToLower());
            if (director is not null)
                throw new InvalidOperationException("Yönetmen zaten kayıtlı!");
            
             director = _mapper.Map<Director>(Model);
        

            _dbContext.Directors.Add(director);
            _dbContext.SaveChanges();            
        }
    }

    public class CreateDirectorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}