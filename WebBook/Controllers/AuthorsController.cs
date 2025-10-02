using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models.DTO;
using WebBook.Repositories;

namespace WebBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthor _authorRepo;
        public AuthorsController(IAuthor authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorRepo.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("get-author-by-id/{id:int}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepo.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newAuthor = _authorRepo.AddAuthor(dto);
            return Ok(newAuthor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-author-by-id/{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorNoIdDTO dto)
        {
            var updated = _authorRepo.UpdateAuthorById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-author-by-id/{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var deleted = _authorRepo.DeleteAuthorById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
