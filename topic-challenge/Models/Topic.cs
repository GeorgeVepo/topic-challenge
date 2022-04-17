using System.ComponentModel.DataAnnotations;

namespace topic_challenge.Models
{
    public class Topic
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public User User { get; set; } = new User();

    }
}
