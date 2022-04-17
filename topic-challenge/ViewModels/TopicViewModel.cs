using System.ComponentModel.DataAnnotations;
using topic_challenge.Models;

namespace topic_challenge.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo título é obrigatório!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório!")]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public UserViewModel User { get; set; } = new UserViewModel();

    }
}