using Microsoft.EntityFrameworkCore;
using topic_challenge.Models;

namespace topic_challenge.Services.Interfaces
{
    public interface ITopicService
    {
        List<Topic> GetAll();
        void Create(Topic topic);
        void Update(Topic topic);
    }
}
