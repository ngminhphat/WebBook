using System.ComponentModel.DataAnnotations;

namespace WebBook.Models.DTO
{
    public class RegisterDTO
    {
        [Required, MinLength(4)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
