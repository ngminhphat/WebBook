using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models.DTO;
using WebBook.Repositories;

namespace WebBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBook _bookRepo;
        public BooksController(IBook bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks(
          [FromQuery] string? filterOn,
          [FromQuery] string? filterQuery,
          [FromQuery] string? sortBy,
          [FromQuery] bool isAscending = true,
          [FromQuery] int pageNumber = 1,
          [FromQuery] int pageSize = 100)
        {
            var books = _bookRepo.GetAllBooks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id:int}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var book = _bookRepo.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newBook = _bookRepo.AddBook(dto);
            return Ok(newBook);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-book-by-id/{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] AddBookRequestDTO dto)
        {
            var updated = _bookRepo.UpdateBookById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-book-by-id/{id:int}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            var deleted = _bookRepo.DeleteBookById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
