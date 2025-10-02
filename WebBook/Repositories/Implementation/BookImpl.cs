using Microsoft.EntityFrameworkCore;
using WebBook.Data;
using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public class BookImpl : IBook
    {
        private readonly AppDbContext _dbContext;
        public BookImpl(AppDbContext dbContext) => _dbContext = dbContext;

        public List<BookWithAuthorAndPublisherDTO> GetAllBooks(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 100)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            // Project -> DTO trước để có PublisherName/AuthorNames
            var query = _dbContext.Books
                .Select(b => new BookWithAuthorAndPublisherDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    IsRead = b.IsRead,
                    DateRead = b.IsRead ? b.DateRead : null,
                    Rate = b.IsRead ? b.Rate : null,
                    Genre = b.Genre,
                    CoverUrl = b.CoverUrl,
                    PublisherName = b.Publisher.Name,
                    AuthorNames = b.BookAuthors.Select(x => x.Author.FullName).ToList()
                })
                .AsQueryable();

            // ----- Filter (7.1) -----
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                switch (filterOn.ToLower())
                {
                    case "title":
                        query = query.Where(x => x.Title.Contains(filterQuery));
                        break;
                    case "publisher":
                        query = query.Where(x => x.PublisherName.Contains(filterQuery));
                        break;
                    case "genre":
                        query = query.Where(x => x.Genre != null && x.Genre.Contains(filterQuery));
                        break;
                }
            }

            // ----- Sort (7.2) -----
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "title":
                        query = isAscending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                        break;
                    case "publisher":
                        query = isAscending ? query.OrderBy(x => x.PublisherName) : query.OrderByDescending(x => x.PublisherName);
                        break;
                    case "dateadded":
                        // không có field trực tiếp trong DTO — bỏ qua hoặc thêm vào DTO nếu cần
                        break;
                }
            }
            else
            {
                // default sort
                query = query.OrderBy(x => x.Id);
            }

            // ----- Pagination (7.3) -----
            var skip = (pageNumber - 1) * pageSize;
            return query.Skip(skip).Take(pageSize).ToList();
        }

        // các method còn lại giữ nguyên (GetById/Add/Update/Delete)...
        public BookWithAuthorAndPublisherDTO? GetBookById(int id)
        {
            return _dbContext.Books
                .Where(n => n.Id == id)
                .Select(book => new BookWithAuthorAndPublisherDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.DateRead,
                    Rate = book.Rate,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    PublisherName = book.Publisher.Name,
                    AuthorNames = book.BookAuthors.Select(n => n.Author.FullName).ToList()
                }).FirstOrDefault();
        }

        public AddBookRequestDTO AddBook(AddBookRequestDTO dto)
        {
            var entity = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                IsRead = dto.IsRead,
                DateRead = dto.DateRead,
                Rate = dto.Rate,
                Genre = dto.Genre,
                CoverUrl = dto.CoverUrl,
                DateAdded = dto.DateAdded,
                PublisherID = dto.PublisherID
            };
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();

            foreach (var id in dto.AuthorIds)
            {
                _dbContext.Books_Authors.Add(new BookAuthor { BookId = entity.Id, AuthorId = id });
                _dbContext.SaveChanges();
            }
            return dto;
        }

        public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO dto)
        {
            var book = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (book != null)
            {
                book.Title = dto.Title;
                book.Description = dto.Description;
                book.IsRead = dto.IsRead;
                book.DateRead = dto.DateRead;
                book.Rate = dto.Rate;
                book.Genre = dto.Genre;
                book.CoverUrl = dto.CoverUrl;
                book.DateAdded = dto.DateAdded;
                book.PublisherID = dto.PublisherID;
                _dbContext.SaveChanges();
            }

            var links = _dbContext.Books_Authors.Where(a => a.BookId == id).ToList();
            if (links.Count > 0)
            {
                _dbContext.Books_Authors.RemoveRange(links);
                _dbContext.SaveChanges();
            }
            foreach (var aid in dto.AuthorIds)
            {
                _dbContext.Books_Authors.Add(new BookAuthor { BookId = id, AuthorId = aid });
                _dbContext.SaveChanges();
            }
            return dto;
        }

        public Book? DeleteBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
            return book;
        }
    }
}
