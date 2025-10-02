using System.ComponentModel.DataAnnotations;

namespace WebBook.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(4)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty; 
        public string Role { get; set; } = "User";
    }
}
