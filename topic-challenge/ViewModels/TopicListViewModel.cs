using topic_challenge.Models;

namespace topic_challenge.ViewModels
{
    public class TopicListViewModel
    {
        public List<TopicViewModel> Topics { get; set; }
        public UserViewModel CurrentUser { get; set; } = new UserViewModel();

    }
}
