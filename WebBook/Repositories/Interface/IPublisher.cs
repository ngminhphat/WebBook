using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public interface IPublisher
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO? GetPublisherById(int id);
        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO? UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        Publisher? DeletePublisherById(int id);
    }
}
