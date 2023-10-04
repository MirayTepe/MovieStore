using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Application.DirectorOperations.Queries.Get;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int Id;

        public GetDirectorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public GetDirectorDetailViewModel Handle()
        {
            var director = _dbContext.Directors.SingleOrDefault(s => s.Id == Id);
            if (director is null)
              throw new InvalidOperationException("Yönetmen kaydı bulunamadı!");

            GetDirectorDetailViewModel vm = _mapper.Map<GetDirectorDetailViewModel>(director);

            return vm;
        }

        public class GetDirectorDetailViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        
        }
    }
}