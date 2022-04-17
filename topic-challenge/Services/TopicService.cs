using Microsoft.EntityFrameworkCore;
using topic_challenge.Models;
using topic_challenge.Repositories.Interfaces;
using topic_challenge.Services.Interfaces;

namespace topic_challenge.Services
{
    public class TopicService : ITopicService
    {
        private ITopicRepository _topicRepository;
        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public void Create(Topic topic)
        {
            _topicRepository.Create(topic);
        }

        public List<Topic> GetAll()
        {
            return _topicRepository.GetAll();
        }

        public void Update(Topic topic)
        {
            _topicRepository.Update(topic);
        }
    }
}
