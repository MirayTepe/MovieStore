using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MicrosoftWebApi.DbOprations;
using MovieStoreWebapi.Entities;



namespace MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }

        private readonly IMovieStoreDbContext _movieStoreDbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IMovieStoreDbContext movieStoreDbContext, IMapper mapper)
        {
            _movieStoreDbContext = movieStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _movieStoreDbContext.Genres.SingleOrDefault(x => x.Name.Trim().ToLower() == Model.Name.Trim().ToLower());

            if (genre is not null)
                throw new InvalidOperationException("Film türü zaten mevcut!");

            genre = _mapper.Map<Genre>(Model);

            _movieStoreDbContext.Genres.Add(genre);
            _movieStoreDbContext.SaveChanges();
        }

    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
       
    }

}
