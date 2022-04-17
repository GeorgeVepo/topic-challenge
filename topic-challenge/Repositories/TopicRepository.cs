using Microsoft.EntityFrameworkCore;
using topic_challenge.Data;
using topic_challenge.Models;
using topic_challenge.Repositories.Interfaces;

namespace topic_challenge.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private SQLServerContext _sqlServerContext;
        public TopicRepository(SQLServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Topic topic)
        {
            _sqlServerContext.Add(topic);
            _sqlServerContext.SaveChanges();
        }

        public List<Topic> GetAll()
        {
            return _sqlServerContext.Topics.Include(t=> t.User).ToList();
        }

        public void Update(Topic topic)
        {
            _sqlServerContext.Update(topic);
            _sqlServerContext.SaveChanges();
        }
    }
}
