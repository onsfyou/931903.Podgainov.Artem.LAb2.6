using System.ComponentModel.DataAnnotations;

namespace LAb6.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
