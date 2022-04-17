using Microsoft.EntityFrameworkCore;
using topic_challenge.Models;

namespace topic_challenge.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        List<Topic> GetAll();
        void Create(Topic topic);
        void Update(Topic topic);
    }
}
