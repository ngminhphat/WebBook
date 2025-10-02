using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public interface IAuthor
    {
        List<AuthorDTO> GetAllAuthors();
        AuthorNoIdDTO? GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AuthorNoIdDTO? UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
        Author? DeleteAuthorById(int id);
    }
}
