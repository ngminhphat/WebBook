using System.ComponentModel.DataAnnotations;
using WebBook.Models.Entities;

namespace WebBook.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MinLength(10)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        public int? Rate { get; set; }   // 0..5

        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

     
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; } = null!;

  
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
