namespace WebBook.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }

    public class AuthorNoIdDTO
    {
        public string FullName { get; set; } = string.Empty;
    }

    public class AddAuthorRequestDTO
    {
        public string FullName { get; set; } = string.Empty;
    }
}
