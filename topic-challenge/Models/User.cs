using System.ComponentModel.DataAnnotations;

namespace topic_challenge.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public ICollection<Topic> Topics { get; set; }
    }
}
