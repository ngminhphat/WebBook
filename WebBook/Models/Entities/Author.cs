using System.ComponentModel.DataAnnotations;

namespace WebBook.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required, MinLength(1)]
        public string FullName { get; set; } = string.Empty;

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
