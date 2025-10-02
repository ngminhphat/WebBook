using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBook.Models.DTO;
using WebBook.Repositories;

namespace WebBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisher _publisherRepo;
        public PublishersController(IPublisher publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers()
        {
            var publishers = _publisherRepo.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("get-publisher-by-id/{id:int}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepo.GetPublisherById(id);
            if (publisher == null) return NotFound();
            return Ok(publisher);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newPublisher = _publisherRepo.AddPublisher(dto);
            return Ok(newPublisher);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-publisher-by-id/{id:int}")]
        public IActionResult UpdatePublisher(int id, [FromBody] PublisherNoIdDTO dto)
        {
            var updated = _publisherRepo.UpdatePublisherById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-publisher-by-id/{id:int}")]
        public IActionResult DeletePublisher(int id)
        {
            var deleted = _publisherRepo.DeletePublisherById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
