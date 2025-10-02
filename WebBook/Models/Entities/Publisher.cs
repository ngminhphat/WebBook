using System.ComponentModel.DataAnnotations;
using WebBook.Models.Entities;

namespace WebBook.Models.Entities
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required, MinLength(1)]
        public string Name { get; set; } = string.Empty;

        // 1-n: Một NXB có nhiều sách
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
