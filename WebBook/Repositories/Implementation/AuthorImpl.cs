using WebBook.Data;
using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public class AuthorImpl : IAuthor
    {
        private readonly AppDbContext _dbContext;

        public AuthorImpl(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AuthorDTO> GetAllAuthors()
        {
            var domains = _dbContext.Authors.ToList();
            var dtos = new List<AuthorDTO>();
            foreach (var a in domains)
            {
                dtos.Add(new AuthorDTO { Id = a.Id, FullName = a.FullName });
            }
            return dtos;
        }

        public AuthorNoIdDTO? GetAuthorById(int id)
        {
            var domain = _dbContext.Authors.FirstOrDefault(x => x.Id == id);
            if (domain == null) return null;
            return new AuthorNoIdDTO { FullName = domain.FullName };
        }

        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var entity = new Models.Entities.Author { FullName = addAuthorRequestDTO.FullName };
            _dbContext.Authors.Add(entity);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }

        public AuthorNoIdDTO? UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var domain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (domain != null)
            {
                domain.FullName = authorNoIdDTO.FullName;
                _dbContext.SaveChanges();
            }
            return authorNoIdDTO;
        }

        public Author? DeleteAuthorById(int id)
        {
            var domain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if (domain != null)
            {
                _dbContext.Authors.Remove(domain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
