using WebBook.Data;
using WebBook.Models.DTO;
using WebBook.Models.Entities;

namespace WebBook.Repositories
{
    public class PublisherImpl : IPublisher
    {
        private readonly AppDbContext _dbContext;

        public PublisherImpl(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            var domains = _dbContext.Publishers.ToList();
            var dtos = new List<PublisherDTO>();
            foreach (var p in domains)
            {
                dtos.Add(new PublisherDTO { Id = p.Id, Name = p.Name });
            }
            return dtos;
        }

        public PublisherNoIdDTO? GetPublisherById(int id)
        {
            var domain = _dbContext.Publishers.FirstOrDefault(x => x.Id == id);
            if (domain != null)
            {
                return new PublisherNoIdDTO { Name = domain.Name };
            }
            return null;
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var entity = new Publisher { Name = addPublisherRequestDTO.Name };
            _dbContext.Publishers.Add(entity);
            _dbContext.SaveChanges();
            return addPublisherRequestDTO;
        }

        public PublisherNoIdDTO? UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var domain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (domain != null)
            {
                domain.Name = publisherNoIdDTO.Name;
                _dbContext.SaveChanges();
            }
            return null;
        }

        public Publisher? DeletePublisherById(int id)
        {
            var domain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (domain != null)
            {
                _dbContext.Publishers.Remove(domain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
