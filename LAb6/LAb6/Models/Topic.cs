using System.ComponentModel.DataAnnotations;

namespace LAb6.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ParentForumId { get; set; }
    }
}
