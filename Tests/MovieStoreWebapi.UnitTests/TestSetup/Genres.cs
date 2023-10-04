using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStoreWebapi.DbOprations;
using MovieStoreWebapi.Entities;


namespace MovieStoreWebapi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context)
        {
             context.Genres.AddRange(
                   new Genre
                   {
                       Name = "Aksiyon "
                   },
                   new Genre
                   {
                       Name = "Bilimkurgu "
                   },
                   new Genre
                   {
                       Name = "Animasyon "
                   },
                    new Genre
                   {
                       Name = "Aksiyon "
                   },
                    new Genre
                   {
                       Name = "Suç "
                   },
                    new Genre
                   {
                       Name = "Gerilim "
                   },
                   new Genre
                   {
                       Name = "Komedi "
                   }
               );
            
                         

        }
    }
}