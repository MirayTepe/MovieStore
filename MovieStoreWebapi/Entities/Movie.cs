using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Entities
{
    public class Movie
    {

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Year { get; set; }
            public decimal Price { get; set; }
            public bool IsActive { get; set; } = true; 
            // İlişkiler
            public int DirectorId { get; set; }
            public Director Director { get; set; }

            public virtual ICollection<MovieActor> MovieActors { get; set; }
            public virtual ICollection<MovieGenre> MovieGenres { get; set; }

          
      
           
        

            
    }
    
}