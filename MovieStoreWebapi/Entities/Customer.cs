using System.ComponentModel.DataAnnotations.Schema;
using MovieStoreWebapi.Entities;

namespace MovieStoreWebapi.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<FavoriteGenre> FavoriteGenres { get; set; }
        
    }
}