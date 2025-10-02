using System.ComponentModel.DataAnnotations;

namespace WebBook.Models.DTO
{
    // Dùng cho Post/Put
    public class AddBookRequestDTO
    {
        [Required, MinLength(10)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        [Range(0, 5)]
        public int? Rate { get; set; }

        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [Required]
        public int PublisherID { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; } = new();
    }
}
